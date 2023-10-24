using AwesomeMeds.Providers.BusinessLayer.Workflows;
using AwesomeMeds.Providers.DataAccessLayer;
using AwesomeMeds.Providers.DataContracts;
using AwesomeMeds.Scheduling.DataContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Clients.BusinessLayer.Test.Workflows
{ 

    [TestClass]
    public class AddProviderAvailableTimeWindowWorkflowTest
    {
        private Mock<IProviderDataConnection> _mockProviderDataConnection;
        private IAddProviderAvailableTimeWindowWorkflow _testObject;
        private Guid _validProviderID = new Guid(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
        private Guid _invalidProviderID = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        [TestInitialize]
        public void InitializeTest()
        {
            _mockProviderDataConnection = new Mock<IProviderDataConnection>();

            // we have 1 known provider that we accept as good
            _mockProviderDataConnection.Setup(providerDataConn => providerDataConn.GetProviderByGuid(_validProviderID)).Returns(new Provider { ProviderID = _validProviderID });

            // start off with returning an empty list of existing times
            _mockProviderDataConnection.Setup(providerDataConn => providerDataConn.GetProviderAppointmentSlots(_validProviderID)).Returns(new List<AppointmentSlot>());

            _testObject = new AddProviderAvailableTimeWindowWorkflow(_mockProviderDataConnection.Object);
        }

        private void AssertExceptionContainingMessage<TException>(Action action, string exceptionMessageStartsWith, string errorMessage)
            where TException : Exception
        {
            bool exceptionCaught = false;
            try
            {
                action();
            }
            catch (TException exception)
            {
                exceptionCaught = exception.Message.StartsWith(exceptionMessageStartsWith);
            }

            Assert.IsTrue(exceptionCaught, errorMessage);
        }


        [TestMethod]
        public void AddProviderAvailableTimeWindow_TimeWindowTooLong_ExceptionThrownWithProperErrorMessage()
        {
            // arrange
            AppointmentSlot validStartAppointmentSlot = new AppointmentSlot { Day = 1, Month = 1, Year = 2020, Hour = 0, QuarterHourSegment = 0 };
            AppointmentSlot invalidEndAppointmentSlot = new AppointmentSlot(validStartAppointmentSlot.GetDateTimeUTC().AddHours(AddProviderAvailableTimeWindowWorkflow.MaxTimeWindowInHours + 1));
            ProviderAvailableTimeWindow timeWindow = new ProviderAvailableTimeWindow
            {
                StartAppointmentSlot = validStartAppointmentSlot,
                EndAppointmentSlot = invalidEndAppointmentSlot,
                ProviderId = _validProviderID,
            };

            // act, assert
            AssertExceptionContainingMessage<ArgumentException>(() => _testObject.AddProviderAvailableTimeWindow(timeWindow),
                AddProviderAvailableTimeWindowWorkflow.MaxTimeWindowInHoursExceededErrorMessage,
                "An exception should be thrown if the time window is too long for appointments.");
        }

        [TestMethod]
        public void AddProviderAvailableTimeWindow_InvalidProvider_ExceptionThrownWithProperErrorMessage()
        {
            // arrange
            AppointmentSlot validStartAppointmentSlot = new AppointmentSlot { Day = 1, Month = 1, Year = 2020, Hour = 0, QuarterHourSegment = 0 };
            AppointmentSlot validEndAppointmentSlot = new AppointmentSlot(validStartAppointmentSlot.GetDateTimeUTC().AddHours(2));
            ProviderAvailableTimeWindow timeWindow = new ProviderAvailableTimeWindow
            {
                StartAppointmentSlot = validStartAppointmentSlot,
                EndAppointmentSlot = validEndAppointmentSlot,
                ProviderId = _invalidProviderID,
            };

            // act, assert
            AssertExceptionContainingMessage<ArgumentException>(() => _testObject.AddProviderAvailableTimeWindow(timeWindow),
                AddProviderAvailableTimeWindowWorkflow.ProviderNotFoundErrorMessage,
                "An exception should be thrown if provider ID is not found.");
        }

        [TestMethod]
        public void AddProviderAvailableTimeWindow_InvalidDates_ExceptionThrownWithProperErrorMessage()
        {
            // arrange
            AppointmentSlot validStartAppointmentSlot = new AppointmentSlot { Day = 1, Month = 1, Year = 2020, Hour = 0, QuarterHourSegment = 0 };
            AppointmentSlot invalidEndAppointmentSlot = new AppointmentSlot(validStartAppointmentSlot.GetDateTimeUTC().AddDays(-1));
            ProviderAvailableTimeWindow timeWindow = new ProviderAvailableTimeWindow
            {
                StartAppointmentSlot = validStartAppointmentSlot,
                EndAppointmentSlot = invalidEndAppointmentSlot,
                ProviderId = _validProviderID,
            };

            // act, assert
            AssertExceptionContainingMessage<ArgumentException>(() => _testObject.AddProviderAvailableTimeWindow(timeWindow),
                AddProviderAvailableTimeWindowWorkflow.InvalidProviderDatesErrorMessage,
                "An exception should be thrown if the end time is equal or less than the start time.");
        }

        [TestMethod]
        public void AddProviderAvailableTimeWindow_AppointmentSlotsConflict_ExceptionThrownWithProperErrorMessage()
        {
            // arrange
            AppointmentSlot validStartAppointmentSlot = new AppointmentSlot { Day = 1, Month = 1, Year = 2020, Hour = 0, QuarterHourSegment = 0 };
            AppointmentSlot validEndAppointmentSlot = new AppointmentSlot(validStartAppointmentSlot.GetDateTimeUTC().AddHours(4));
            ProviderAvailableTimeWindow timeWindow = new ProviderAvailableTimeWindow
            {
                StartAppointmentSlot = validStartAppointmentSlot,
                EndAppointmentSlot = validEndAppointmentSlot,
                ProviderId = _validProviderID,
            };

            List<AppointmentSlot> priorAppointmentSlots = new List<AppointmentSlot> { new AppointmentSlot(validStartAppointmentSlot.GetDateTimeUTC().AddMinutes(30)) };
            _mockProviderDataConnection.Setup(providerDataConn => providerDataConn.GetProviderAppointmentSlots(_validProviderID)).Returns(priorAppointmentSlots);

            // act, assert
            AssertExceptionContainingMessage<ArgumentException>(() => _testObject.AddProviderAvailableTimeWindow(timeWindow),
                AddProviderAvailableTimeWindowWorkflow.ConflictingProviderAppointmentSlotsErrorMessage,
                "An exception should be thrown if the end time is equal or less than the start time.");
        }

    }
}
