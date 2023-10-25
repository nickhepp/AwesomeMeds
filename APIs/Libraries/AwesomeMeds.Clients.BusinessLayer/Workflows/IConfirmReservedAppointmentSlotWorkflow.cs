using AwesomeMeds.Scheduling.DataContracts;

namespace AwesomeMeds.Clients.BusinessLayer.Workflows
{
    public interface IConfirmReservedAppointmentSlotWorkflow
    {

        /// <summary>
        /// Confirms the reserved appointment slot for the given client.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="appointmentSlot"></param>
        void ConfirmReservedAppointmentSlot(Guid clientID, AppointmentSlot appointmentSlot);
    }
}