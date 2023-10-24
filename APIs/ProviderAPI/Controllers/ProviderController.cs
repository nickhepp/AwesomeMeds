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

        [HttpPut("provider/appointment-slots")]
        public StatusCodeResult AddApointmentTimeWindows([FromBody] ProviderAvailableTimeWindow timeWindow)
        {
            return Ok();
        }


        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}