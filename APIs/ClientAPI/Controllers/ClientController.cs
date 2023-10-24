using AwesomeMeds.Clients.BusinessLayer.Providers;
using AwesomeMeds.Clients.DataAccessLayer;
using AwesomeMeds.Scheduling.Business;
using AwesomeMeds.Scheduling.DataContracts;
using AwesomeMeds.Scheduling.DataContracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {

        private readonly ILogger<ClientController> _logger;
        private readonly IAvailableAppointmentSlotProvider _availableAppointmentSlotProvider;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
            // TODO: inject this with DI
            _availableAppointmentSlotProvider = new AvailableAppointmentSlotProvider(new ClientDataConnection(), new DateTimeProvider());
        }

        // TODO: Authentication

        [HttpGet(Name = "appointment-slots")]
        public IActionResult GetAvailableAppointmentSlots()
        {
            try
            {
                return new OkObjectResult(_availableAppointmentSlotProvider.GetAvailableAppointmentSlots());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost(Name = "appointment-slots/reserve")]
        public IActionResult ReserveAvailableAppointmentSlots([FromBody] ReserveAppointmentSlotRequest reserveRequest)
        {
            try
            {
                return new OkObjectResult(_availableAppointmentSlotProvider.GetAvailableAppointmentSlots());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


    }
}