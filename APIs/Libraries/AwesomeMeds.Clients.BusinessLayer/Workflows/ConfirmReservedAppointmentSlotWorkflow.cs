using AwesomeMeds.Clients.DataAccessLayer;
using AwesomeMeds.Scheduling.Business;
using AwesomeMeds.Scheduling.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMeds.Clients.BusinessLayer.Workflows
{
    public class ConfirmReservedAppointmentSlotWorkflow : IConfirmReservedAppointmentSlotWorkflow
    {

        public const string ClientNotFoundErrorMessage = "Client not found.";
        public const string CouldNotConfirmReservationErrorMessage = "Could not confirm reserveration.";

        private IDateTimeProvider _dateTimeProvider;
        private IClientDataConnection _clientDataConnection;
        public ConfirmReservedAppointmentSlotWorkflow(IClientDataConnection clientDataConnection, IDateTimeProvider dateTimeProvider)
        {
            _clientDataConnection = clientDataConnection;
            _dateTimeProvider = dateTimeProvider;
        }


        public void ConfirmReservedAppointmentSlot(Guid clientID, AppointmentSlot appointmentSlot)
        {
            // TODO: unit test we have a valid client, and if not throw an exception
            Clients.DataContracts.Client client = _clientDataConnection.GetClientByClientID(clientID);
            if (client == null)
            {
                throw new ArgumentException($"{ClientNotFoundErrorMessage} Client with ID '{clientID}' is not valid.");
            }

            // TODO: unit test exception thrown when not confirmed
            DateTime dateTime = _dateTimeProvider.GetCurrentDateTimeUtc();
            bool confirmed = _clientDataConnection.ConfirmUnreservedAppointmentSlot(clientID, appointmentSlot, dateTime);
            if (!confirmed)
            {
                throw new ArgumentException(CouldNotConfirmReservationErrorMessage);
            }

        }
    }
}
