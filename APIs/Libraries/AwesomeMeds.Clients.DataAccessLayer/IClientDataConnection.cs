using AwesomeMeds.Scheduling.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Clients.DataAccessLayer
{
    public interface IClientDataConnection
    {

        void DeleteUnconfirmedPendingReservations();

        List<AppointmentSlot> GetUnreservedAppointmentSlots();

    }
}
