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

        AwesomeMeds.Clients.DataContracts.Client GetClientByClientID(Guid clientID);

        void DeleteUnconfirmedPendingReservations();

        List<AppointmentSlot> GetUnreservedAppointmentSlots();
        void ReserveAppointmentSlot(Guid clientID, AppointmentSlot appointmentSlot);
        bool ConfirmUnreservedAppointmentSlot(Guid clientID, AppointmentSlot appointmentSlot, DateTime dateTime);
    }
}
