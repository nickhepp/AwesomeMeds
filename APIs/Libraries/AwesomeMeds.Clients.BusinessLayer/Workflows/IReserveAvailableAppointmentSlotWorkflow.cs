using AwesomeMeds.Scheduling.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Clients.BusinessLayer.Workflows
{
    public interface IReserveAvailableAppointmentSlotWorkflow
    {
        /// <summary>
        /// Reserves the given appt slot for the client.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="appointment"></param>
        void ReserveAvailableAppointmentSlot(Guid clientID, AppointmentSlot appointment);

    }
}
