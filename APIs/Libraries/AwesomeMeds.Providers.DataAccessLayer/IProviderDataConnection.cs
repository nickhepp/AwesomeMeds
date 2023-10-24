using AwesomeMeds.Providers.DataContracts;
using AwesomeMeds.Scheduling.DataContracts;

namespace AwesomeMeds.Providers.DataAccessLayer
{
    public interface IProviderDataConnection
    {
        Provider GetProviderByGuid(Guid providerID);

        List<AppointmentSlot> GetProviderAppointmentSlots(Guid providerID);
        void AddAppointmentSlots(Guid providerId, List<AppointmentSlot> nextAppointmentSlots);
    }
}