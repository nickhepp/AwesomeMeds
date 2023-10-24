using AwesomeMeds.Providers.DataContracts;
using AwesomeMeds.Scheduling.DataContracts;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace AwesomeMeds.Providers.DataAccessLayer
{
    public class ProviderDataConnection : IProviderDataConnection
    {
        // TODO: Get the connection string from a encrypted data source or secure key vault
        private const string ProviderConnectionString = "";


        public Provider GetProviderByGuid(Guid providerID)
        {
            Provider provider = null;
            using (IDbConnection dbConnection = new SqlConnection(ProviderConnectionString))
            {
                // Execute the stored procedure and map the result to the Provider class
                provider = dbConnection.QueryFirstOrDefault<Provider>(
                    "GetProviderByProviderID",
                    new { ProviderID = providerID},
                    commandType: CommandType.StoredProcedure
                );

            }
            return provider;
        }
        public List<AppointmentSlot> GetProviderAppointmentSlots(Guid providerID)
        {
            throw new NotImplementedException();
        }

        public void AddAppointmentSlots(Guid providerId, List<AppointmentSlot> nextAppointmentSlots)
        {
            throw new NotImplementedException();
        }
    }
}