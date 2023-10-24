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

        void ReserveAvailableAppointmentSlot(Guid clientID, AppointmentSlot appointment);

    }
}
