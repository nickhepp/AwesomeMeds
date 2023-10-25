using AwesomeMeds.Clients.BusinessLayer.Providers;
using AwesomeMeds.Clients.BusinessLayer.Workflows;
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
        private readonly IReserveAvailableAppointmentSlotWorkflow _reserveAvailableAppointmentSlotWorkflow;
        private readonly IConfirmReservedAppointmentSlotWorkflow _confirmReservedAppointmentSlotWorkflow;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
            // TODO: inject these classes with DI
            IClientDataConnection clientDataConnection = new ClientDataConnection();
            IDateTimeProvider dateTimeProvider = new DateTimeProvider();
            _availableAppointmentSlotProvider = new AvailableAppointmentSlotProvider(clientDataConnection, dateTimeProvider);
            _reserveAvailableAppointmentSlotWorkflow = new ReserveAvailableAppointmentSlotWorkflow(new ClientDataConnection(), _availableAppointmentSlotProvider);
            _confirmReservedAppointmentSlotWorkflow = new ConfirmReservedAppointmentSlotWorkflow(clientDataConnection, dateTimeProvider);
        }

        // TODO: Authentication

        [HttpGet("appointment-slots")]
        public IActionResult GetAvailableAppointmentSlots()
        {
            try
            {
                return new OkObjectResult(_availableAppointmentSlotProvider.GetAvailableAppointmentSlots());
            }
            catch (Exception ex)
            {
                // TODO: log exception
                return BadRequest();
            }
        }

        [HttpPost("appointment-slots/reserve")]
        public IActionResult ReserveAvailableAppointmentSlot([FromBody] AppointmentSlotRequest reserveRequest)
        {
            try
            {
                _reserveAvailableAppointmentSlotWorkflow.ReserveAvailableAppointmentSlot(reserveRequest.ClientID, reserveRequest.AppointmentSlot);
                return Ok();
            }
            catch (Exception ex)
            {
                // TODO: log exception
                return BadRequest();
            }
        }

        [HttpPost("appointment-slots/confirm")]
        public IActionResult ConfirmReservedAvailableAppointment([FromBody] AppointmentSlotRequest reserveRequest)
        {
            try
            {
                _confirmReservedAppointmentSlotWorkflow.ConfirmReservedAppointmentSlot(reserveRequest.ClientID, reserveRequest.AppointmentSlot);
                return Ok();
            }
            catch (Exception ex)
            {
                // TODO: log exception
                return BadRequest();
            }
        }



    }
}