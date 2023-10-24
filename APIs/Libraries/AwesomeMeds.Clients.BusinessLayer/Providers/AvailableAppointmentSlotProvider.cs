using AwesomeMeds.Clients.DataAccessLayer;
using AwesomeMeds.Scheduling.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Clients.BusinessLayer.Providers
{
    public class AvailableAppointmentSlotProvider
    {

        private IClientDataConnection _clientDataConnection;

        public AvailableAppointmentSlotProvider(IClientDataConnection clientDataConnection)
        {
            _clientDataConnection = clientDataConnection;
        } 

        public List<AppointmentSlot> GetAvailableAppointmentSlots()
        {
            _clientDataConnection.DeleteUnconfirmedPendingReservations();
            return _clientDataConnection.GetUnreservedAppointmentSlots();
        }



    }
}
