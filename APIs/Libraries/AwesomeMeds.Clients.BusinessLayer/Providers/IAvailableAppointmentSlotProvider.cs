using AwesomeMeds.Scheduling.DataContracts;

namespace AwesomeMeds.Clients.BusinessLayer.Providers
{
    public interface IAvailableAppointmentSlotProvider
    {
        List<AppointmentSlot> GetAvailableAppointmentSlots();
    }
}