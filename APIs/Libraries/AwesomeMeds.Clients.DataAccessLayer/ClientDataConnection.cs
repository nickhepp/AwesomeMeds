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
    }
}