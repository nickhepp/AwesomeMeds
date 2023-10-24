using AwesomeMeds.Providers.DataAccessLayer;
using AwesomeMeds.Providers.DataContracts;
using AwesomeMeds.Scheduling.DataContracts;

namespace AwesomeMeds.Providers.BusinessLayer.Workflows
{
    public class AddProviderAvailableTimeWindowWorkflow : IAddProviderAvailableTimeWindowWorkflow
    {

        public const string ProviderNotFoundErrorMessage = "Provider not found.";
        public const string InvalidProviderDatesErrorMessage = "Invalid provider dates.";
        public const string ConflictingProviderAppointmentSlotsErrorMessage = "Conflicting provider appointment slots.";
        public const string MaxTimeWindowInHoursExceededErrorMessage = "Max time window of hours exceeded.";
        public const double MaxTimeWindowInHours = 12.0;

        private IProviderDataConnection _providerDataConnection;
        public AddProviderAvailableTimeWindowWorkflow(IProviderDataConnection providerDataConnection)
        {
            _providerDataConnection = providerDataConnection;
        }

        public void AddProviderAvailableTimeWindow(ProviderAvailableTimeWindow timeWindow)
        {

            // test we have a valid provider
            Provider provider = _providerDataConnection.GetProviderByGuid(timeWindow.ProviderId);
            if (provider == null)
            {
                throw new ArgumentException($"{ProviderNotFoundErrorMessage} Provider with ID '{timeWindow.ProviderId}' is not valid.");
            }

            // make sure the hours are sensible
            double totalHours = (timeWindow.EndAppointmentSlot.GetDateTimeUTC() - timeWindow.StartAppointmentSlot.GetDateTimeUTC()).TotalHours;
            if (totalHours <= 0.0)
            {
                throw new ArgumentException($"{InvalidProviderDatesErrorMessage} Provider with ID '{timeWindow.ProviderId}' has invalid dates.");
            }
            else if (totalHours > MaxTimeWindowInHours)
            {
                throw new ArgumentException($"{MaxTimeWindowInHoursExceededErrorMessage} Provider with ID '{timeWindow.ProviderId}' has invalid dates.");
            }


            // make sure the time window doesnt interfere with an existing time window
            List<AppointmentSlot> nextAppointmentSlots = new List<AppointmentSlot>();
            AppointmentSlot currentApptSlot = timeWindow.StartAppointmentSlot;
            while (currentApptSlot.GetDateTimeUTC() <= timeWindow.EndAppointmentSlot.GetDateTimeUTC())
            {
                nextAppointmentSlots.Add(currentApptSlot);
                currentApptSlot = new AppointmentSlot(currentApptSlot.GetDateTimeUTC().AddMinutes(15));
            }

            List<AppointmentSlot> appointmentSlots = _providerDataConnection.GetProviderAppointmentSlots(timeWindow.ProviderId);
            if (appointmentSlots.Intersect(nextAppointmentSlots).Any())
            {
                throw new ArgumentException($"{ConflictingProviderAppointmentSlotsErrorMessage} Provider with ID '{timeWindow.ProviderId}' has invalid dates.");
            }

            _providerDataConnection.AddAppointmentSlots(timeWindow.ProviderId, nextAppointmentSlots);
        }

    }
}