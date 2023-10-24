using AwesomeMeds.Clients.DataAccessLayer;
using AwesomeMeds.Scheduling.Business;
using AwesomeMeds.Scheduling.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Clients.BusinessLayer.Providers
{
    public class AvailableAppointmentSlotProvider : IAvailableAppointmentSlotProvider
    {
        private IDateTimeProvider _dateTimeProvider;
        private IClientDataConnection _clientDataConnection;

        public const int MinHoursForAppointmentSlotFromNow = 24;

        public AvailableAppointmentSlotProvider(IClientDataConnection clientDataConnection, IDateTimeProvider dateTimeProvider)
        {
            _clientDataConnection = clientDataConnection;
            _dateTimeProvider = dateTimeProvider;
        }

        public List<AppointmentSlot> GetAvailableAppointmentSlots()
        {
            // this cancels unconfirmed appts
            _clientDataConnection.DeleteUnconfirmedPendingReservations();

            // As a Client, I can view available appointment slots within a time window that is at least 24 hours into the future.
            // An alternative implementation of this would push the filtering into the database.  As this data set grows larger, developers
            // would want to put lower and upper bounds on the time from which appts were fetched.
            DateTime twentyfourHoursInFuture = _dateTimeProvider.GetCurrentDateTimeUtc().AddHours(MinHoursForAppointmentSlotFromNow);
            return _clientDataConnection.GetUnreservedAppointmentSlots().Where(unreservedAppointmentSlot => unreservedAppointmentSlot.GetDateTimeUTC() > twentyfourHoursInFuture).ToList();
        }

    }
}
