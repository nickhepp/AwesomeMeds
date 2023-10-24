using AwesomeMeds.Providers.DataContracts;

namespace AwesomeMeds.Providers.BusinessLayer.Workflows
{
    public interface IAddProviderAvailableTimeWindowWorkflow
    {
        void AddProviderAvailableTimeWindow(ProviderAvailableTimeWindow timeWindow);
    }
}