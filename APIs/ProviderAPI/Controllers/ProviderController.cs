using AwesomeMeds.Providers.BusinessLayer.Workflows;
using AwesomeMeds.Providers.DataAccessLayer;
using AwesomeMeds.Providers.DataContracts;
using Microsoft.AspNetCore.Mvc;

namespace ProviderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProviderController : ControllerBase
    {

        private readonly IAddProviderAvailableTimeWindowWorkflow _addProviderAvailableTimeWindowWorkflow;
        private readonly ILogger<ProviderController> _logger;

        public ProviderController(ILogger<ProviderController> logger)
        {
            _logger = logger;

            // TODO: make these provided via dependency injection
            _addProviderAvailableTimeWindowWorkflow = new AddProviderAvailableTimeWindowWorkflow(new ProviderDataConnection());
        }

        // TODO: Authentication

        [HttpPut("appointment-slots")]
        public StatusCodeResult AddApointmentTimeWindows([FromBody] ProviderAvailableTimeWindow timeWindow)
        {
            try
            {
                _addProviderAvailableTimeWindowWorkflow.AddProviderAvailableTimeWindow(timeWindow);
                return Ok();
            }
            catch (Exception ex)
            {
                // TODO: logging
                return BadRequest();
            }
        }
    }
}