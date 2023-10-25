using AwesomeMeds.Providers.DataContracts;

namespace AwesomeMeds.Providers.BusinessLayer.Workflows
{
    public interface IAddProviderAvailableTimeWindowWorkflow
    {
    
        /// <summary>
        /// Adds a timewindow for the provider.
        /// </summary>
        /// <param name="timeWindow"></param>
        void AddProviderAvailableTimeWindow(ProviderAvailableTimeWindow timeWindow);
    
    }
}