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
            throw new NotImplementedException();
        }
    }
}