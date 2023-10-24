using AwesomeMeds.Clients.BusinessLayer.Providers;
using AwesomeMeds.Clients.DataAccessLayer;
using AwesomeMeds.Scheduling.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Clients.BusinessLayer.Workflows
{
    public class ReserveAvailableAppointmentSlotWorkflow : IReserveAvailableAppointmentSlotWorkflow
    {

        public const string ClientNotFoundErrorMessage = "Client not found.";
        public const string AppointmentSlotNotAvailableErrorMessage = "Appointment slot not available.";

        private IClientDataConnection _clientDataConnection;
        private IAvailableAppointmentSlotProvider _availableAppointmentSlotProvider;
        public ReserveAvailableAppointmentSlotWorkflow(IClientDataConnection clientDataConnection, IAvailableAppointmentSlotProvider availableAppointmentSlotProvider)
        {
            _clientDataConnection = clientDataConnection;
            _availableAppointmentSlotProvider = availableAppointmentSlotProvider;
        }

        public void ReserveAvailableAppointmentSlot(Guid clientID, AppointmentSlot appointmentSlot)
        {
            // TODO: unit test we have a valid client, and if not throw an exception
            Clients.DataContracts.Client client = _clientDataConnection.GetClientByClientID(clientID);
            if (client == null)
            {
                throw new ArgumentException($"{ClientNotFoundErrorMessage} Client with ID '{clientID}' is not valid.");
            }

            // TODO: unit test to make sure the time is still available, and if not throw an exception
            // Note that we are doing this here in business logic and there could be a race condition here if we dont do it in the database.
            // For me writing this code in C# is faster to implement, but for a production system I would take the time to write the SQL to make
            // sure the appt is claimed in an atomic manner.

            // IMO its much easier to confirm proper operation of code with unit tests than with SQL databases, so when possible I try to keep business logic
            // in the application code above the SQL database.  Also note that with proper Primary Keys its most likely the database can still prohibit
            // the downside affects of the race condition and the client would need to start the workflow over.

            List<AppointmentSlot> availableApptSlots = _availableAppointmentSlotProvider.GetAvailableAppointmentSlots();
            if (!availableApptSlots.Contains(appointmentSlot))
            {
                throw new ArgumentException(AppointmentSlotNotAvailableErrorMessage);
            }

            _clientDataConnection.ReserveAppointmentSlot(clientID, appointmentSlot);
        }



    }
}
