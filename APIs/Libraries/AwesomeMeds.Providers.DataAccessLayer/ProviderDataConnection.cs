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
        private readonly string _providerConnectionString = Environment.GetEnvironmentVariable("AWESOME_MEDS_DB_CONNECTION");


        public Provider GetProviderByGuid(Guid providerID)
        {
            Provider provider = null;
            using (IDbConnection dbConnection = new SqlConnection(_providerConnectionString))
            {
                // Execute the stored procedure and map the result to the Provider class
                provider = dbConnection.QueryFirstOrDefault<Provider>(
                    "[Provider].[GetProviderByProviderID]",
                    new { ProviderID = providerID},
                    commandType: CommandType.StoredProcedure
                );

            }
            return provider;
        }

        public List<AppointmentSlot> GetProviderAppointmentSlots(Guid providerID)
        {
            List<AppointmentSlot> apptSlots = null;
            using (IDbConnection dbConnection = new SqlConnection(_providerConnectionString))
            {
                apptSlots = dbConnection.Query<AppointmentSlot>(
                        "[Provider].[GetAppointmentSlotsByProviderID]",
                        new { ProviderID = providerID},
                        commandType: CommandType.StoredProcedure
                        ).AsList();

            }
            return apptSlots;
        }

        public void AddAppointmentSlots(Guid providerId, List<AppointmentSlot> nextAppointmentSlots)
        {
            // TODO: in the future we do not want to loop over the database like this for atomicity of update (we dont want a partial update).
            // The choice to do this was for expediency.  To make this better we would create a SQL User Defined Table Type
            // and create the equivalent data table in C#.
            using (IDbConnection dbConnection = new SqlConnection(_providerConnectionString))
            {
                foreach (AppointmentSlot nextAppointmentSlot in nextAppointmentSlots)
                {
                    dbConnection.Execute(
                        "[Provider].[InsertAppointmentSlot]", 
                        new
                        {
                            ProviderID = providerId,
                            nextAppointmentSlot.Year,
                            nextAppointmentSlot.Month,
                            nextAppointmentSlot.Day,
                            nextAppointmentSlot.Hour,
                            nextAppointmentSlot.QuarterHourSegment
                        }, 
                        commandType: CommandType.StoredProcedure);
                }
            }
        }
    }
}