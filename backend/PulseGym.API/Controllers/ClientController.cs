using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Logic.DTO;

using PulseGym.Logic.Facades;

namespace PulseGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        IClientFacade _clientFacade;

        public ClientController(IClientFacade clientFacade)
        {
            _clientFacade = clientFacade;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(ClientCreateDTO newClient)
        {
            var isCreated = await _clientFacade.CreateClientAsync(newClient);

            if (isCreated)
            {
                return Ok("Created successfully!");
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ClientViewDTO>>> Get()
        {
            var clientList = await _clientFacade.GetClientsAsync();

            return Ok(clientList);
        }

        [HttpGet("OccupiedTime/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ICollection<DateTime>>> GetClientOccupiedTime(Guid id)
        {
            var dateTimeList = await _clientFacade.GetOccupiedDateTimeAsync(id);

            return Ok(dateTimeList);
        }
    }
}
