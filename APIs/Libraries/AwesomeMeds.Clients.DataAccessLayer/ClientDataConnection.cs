using AwesomeMeds.Clients.DataContracts;
using AwesomeMeds.Scheduling.DataContracts;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace AwesomeMeds.Clients.DataAccessLayer
{
    public class ClientDataConnection : IClientDataConnection
    {

        // TODO: Get the connection string from a encrypted data source or secure key vault
        private readonly string _providerConnectionString = Environment.GetEnvironmentVariable("AWESOME_MEDS_DB_CONNECTION");

        public AwesomeMeds.Clients.DataContracts.Client GetClientByClientID(Guid clientID)
        {
            AwesomeMeds.Clients.DataContracts.Client client = null;
            using (IDbConnection dbConnection = new SqlConnection(_providerConnectionString))
            {
                // Call the stored procedure using Dapper
                client = dbConnection.QuerySingleOrDefault<AwesomeMeds.Clients.DataContracts.Client>(
                    "[Client].[GetClientByClientID]",
                    new { ClientID = clientID },
                    commandType: CommandType.StoredProcedure
                );

            }
            return client;
        }



        public void DeleteUnconfirmedPendingReservations()
        {
            using (IDbConnection dbConnection = new SqlConnection(_providerConnectionString))
            {
                // Call the stored procedure using Dapper
                dbConnection.Execute("[Client].[DeleteUnconfirmedPendingReservations]", commandType: CommandType.StoredProcedure);
            }
        }

        public List<AppointmentSlot> GetUnreservedAppointmentSlots()
        {
            List<AppointmentSlot> unreservedApptSlots = null;
            using (IDbConnection dbConnection = new SqlConnection(_providerConnectionString))
            {
                // Call the stored procedure using Dapper
                unreservedApptSlots = dbConnection.Query<AppointmentSlot>("[Client].[GetUnreservedAppointmentSlots]", commandType: CommandType.StoredProcedure).AsList();
            }
            return unreservedApptSlots;
        }

        public void ReserveAppointmentSlot(Guid clientID, AppointmentSlot appointmentSlot)
        {
            using (IDbConnection dbConnection = new SqlConnection(_providerConnectionString))
            {
                // Define parameters for the stored procedure
                DateTime dt = DateTime.UtcNow;
                var parameters = new
                {
                    Year = appointmentSlot.Year, // Replace with your desired values
                    Month = appointmentSlot.Month,
                    Day = appointmentSlot.Day,
                    Hour = appointmentSlot.Hour,
                    QuarterHourSegment = appointmentSlot.QuarterHourSegment,
                    ClientID = clientID,
                    ReservationCreatedUTC = dt,
                    ReservationConfirmedByUTC = dt.AddMinutes(ClientConstants.MaxTimeToConfirmReservationInMinutes)
                };

                dbConnection.Execute("[Client].[InsertPendingReservationWithFirstAvailableProvider]", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public bool ConfirmUnreservedAppointmentSlot(Guid clientID, AppointmentSlot appointmentSlot, DateTime dateTime)
        {
            bool confirmed = false;
            using (IDbConnection dbConnection = new SqlConnection(_providerConnectionString))
            {
                var parameters = new
                {
                    ClientID = clientID,
                    ConfirmationDateTime = dateTime, 
                    Year = appointmentSlot.Year,
                    Month = appointmentSlot.Month,
                    Day = appointmentSlot.Day,
                    Hour = appointmentSlot.Hour,
                    QuarterHourSegment = appointmentSlot.QuarterHourSegment
                };
                int rowAddedVal = dbConnection.QuerySingle<int>("[Client].[ConfirmPendingReservationAppointmentSlot]", parameters, commandType: CommandType.StoredProcedure);
                confirmed = (rowAddedVal == 1);
            }
            return confirmed;
        }

    }
}