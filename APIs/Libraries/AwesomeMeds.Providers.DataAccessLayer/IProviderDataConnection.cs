using AwesomeMeds.Providers.DataContracts;
using AwesomeMeds.Scheduling.DataContracts;

namespace AwesomeMeds.Providers.DataAccessLayer
{
    public interface IProviderDataConnection
    {

        /// <summary>
        /// Gets the provider by ID, or NULL if the ID is invalid.
        /// </summary>
        /// <param name="providerID"></param>
        /// <returns></returns>
        Provider GetProviderByGuid(Guid providerID);

        /// <summary>
        /// Gets the existing appt slots for the given provider.
        /// </summary>
        /// <param name="providerID"></param>
        /// <returns></returns>
        List<AppointmentSlot> GetProviderAppointmentSlots(Guid providerID);
        
        /// <summary>
        /// Adds appointment slots for the provider.
        /// </summary>
        /// <param name="providerId"></param>
        /// <param name="nextAppointmentSlots"></param>
        void AddAppointmentSlots(Guid providerId, List<AppointmentSlot> nextAppointmentSlots);
    
    
    }
}