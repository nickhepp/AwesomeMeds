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

        /// <summary>
        /// Gets a client by ID, or NULL if the ID is invalid.
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        AwesomeMeds.Clients.DataContracts.Client GetClientByClientID(Guid clientID);

        /// <summary>
        /// Flushes unconfirmed pending reservations that are too old.
        /// </summary>
        void DeleteUnconfirmedPendingReservations();

        /// <summary>
        /// Gets a list of unconfirmed and available appt slots.
        /// </summary>
        /// <returns></returns>
        List<AppointmentSlot> GetUnreservedAppointmentSlots();

        /// <summary>
        /// Reserves and appt slot for the client.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="appointmentSlot"></param>
        void ReserveAppointmentSlot(Guid clientID, AppointmentSlot appointmentSlot);
        
        /// <summary>
        /// Confirms a reserved appt slot for the client.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="appointmentSlot"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        bool ConfirmUnreservedAppointmentSlot(Guid clientID, AppointmentSlot appointmentSlot, DateTime dateTime);
    
    }
}
