using AwesomeMeds.Scheduling.DataContracts;

namespace AwesomeMeds.Clients.BusinessLayer.Workflows
{
    public interface IConfirmReservedAppointmentSlotWorkflow
    {
        void ConfirmReservedAppointmentSlot(Guid clientID, AppointmentSlot appointmentSlot);
    }
}