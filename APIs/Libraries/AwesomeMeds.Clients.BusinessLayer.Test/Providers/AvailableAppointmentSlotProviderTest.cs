using AwesomeMeds.Clients.BusinessLayer.Providers;
using AwesomeMeds.Clients.DataAccessLayer;
using AwesomeMeds.Scheduling.Business;
using AwesomeMeds.Scheduling.DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Clients.BusinessLayer.Test.Providers
{

    [TestClass]
    public class AvailableAppointmentSlotProviderTest
    {
        private Mock<IClientDataConnection> _mockClientDataConnection;
        private Mock<IDateTimeProvider> _mockDateTimeProvider;
        private IAvailableAppointmentSlotProvider _testObject;

        /// <summary>
        /// These appt slots are too recent to get
        /// </summary>
        private List<AppointmentSlot> _tooRecentApptSlots;

        /// <summary>
        /// These appt slots are far enough in the future to get
        /// </summary>
        private List<AppointmentSlot> _futureApptSlots;

        /// <summary>
        /// These are all the appt slots returned from the database. 
        /// </summary>
        private List<AppointmentSlot> _allApptSlots;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockClientDataConnection = new Mock<IClientDataConnection>();

            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            DateTime dateTime = DateTime.Now.ToUniversalTime();
            dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, minute: 15, second: 0);
            DateTime futureTime = dateTime.AddHours(AvailableAppointmentSlotProvider.MinHoursForAppointmentSlotFromNow + 1);
            _mockDateTimeProvider.Setup(dateTimeProvider => dateTimeProvider.GetCurrentDateTimeUtc()).Returns(dateTime);

            _tooRecentApptSlots = new List<AppointmentSlot>
            {
                new AppointmentSlot (dateTime),
                new AppointmentSlot (dateTime),
                new AppointmentSlot (dateTime),
            };

            _futureApptSlots = new List<AppointmentSlot>
            {
                new AppointmentSlot(futureTime),
                new AppointmentSlot(futureTime),
            };

            _allApptSlots = new List<AppointmentSlot>(_tooRecentApptSlots);
            _allApptSlots.AddRange(_futureApptSlots);

            _testObject = new AvailableAppointmentSlotProvider(_mockClientDataConnection.Object, _mockDateTimeProvider.Object);
        }


        [TestMethod]
        public void GetAvailableAppointmentSlots_IClientDataConnectionDeleteUnconfirmedPendingReservations_CalledOnce()
        {
            // arrange
            Expression<Action<IClientDataConnection>> expression = clientDataConn => clientDataConn.GetUnreservedAppointmentSlots();
            _mockClientDataConnection.Setup(expression);
            _mockClientDataConnection.Setup(clientDataConn => clientDataConn.GetUnreservedAppointmentSlots()).Returns(_futureApptSlots);

            // act
            _testObject.GetAvailableAppointmentSlots();

            // assert
            _mockClientDataConnection.Verify(expression, Times.Once, "Before getting unreserved appointments, unconfirmed appointments that have passed their window should be retired.");
        }

        [TestMethod]
        public void GetAvailableAppointmentSlots_AllApointmentsAreBeyond24Hours_AllReturned()
        {
            // arrange
            _mockClientDataConnection.Setup(clientDataConn => clientDataConn.GetUnreservedAppointmentSlots()).Returns(_futureApptSlots);

            // act
            List<AppointmentSlot> results = _testObject.GetAvailableAppointmentSlots();

            // assert
            Assert.AreEqual(expected: _futureApptSlots.Count, actual: results.Count, "Should not have any data filtered out by time.");
        }

        [TestMethod]
        public void GetAvailableAppointmentSlots_SomeAppointmentsBeyond24Hours_AllAppointmentsBeyond24HoursReturned()
        {
            // arrange
            _mockClientDataConnection.Setup(clientDataConn => clientDataConn.GetUnreservedAppointmentSlots()).Returns(_allApptSlots);

            // act
            List<AppointmentSlot> results = _testObject.GetAvailableAppointmentSlots();

            // assert
            Assert.AreEqual(expected: _futureApptSlots.Count, actual: results.Count, "Should have filtered data by time.");
        }
    }
}
