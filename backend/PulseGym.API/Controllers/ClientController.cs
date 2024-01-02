using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.Infrastructure;
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
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult> Create(ClientCreateDTO newClient)
        {
            var isCreated = await _clientFacade.CreateClientAsync(newClient);

            if (isCreated)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ClientViewDTO>>> Get()
        {
            var clientList = await _clientFacade.GetClientsAsync();

            return Ok(clientList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientViewDTO>> GetById(Guid id)
        {
            var client = await _clientFacade.GetClientAsync(id);

            return Ok(client);
        }

        [HttpGet("OccupiedTime/{clientId}")]
        [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Client}")]
        public async Task<ActionResult<ICollection<DateTime>>> GetClientOccupiedTime(Guid clientId)
        {
            var dateTimeList = await _clientFacade.GetOccupiedDateTimeAsync(clientId);

            return Ok(dateTimeList);
        }

        [HttpPost("{clientId}")]
        [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Client}")]
        public async Task<ActionResult> UpdateClient(Guid clientId, ClientUpdateDTO client)
        {
            var id = HttpContext.User.FindFirstValue("Id");
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (!Guid.TryParse(id, out Guid requestUserId) || requestRole == null
            || (requestRole == RoleNames.Client && clientId != requestUserId))
            {
                return Unauthorized();
            }

            await _clientFacade.UpdateClientAsync(clientId, client);

            return Ok();
        }

        [HttpDelete("{clientId}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult> DeleteClient(Guid clientId)
        {
            await _clientFacade.DeleteClientAsync(clientId);

            return Ok();
        }

        [HttpPut("{clientId}/AddTrainer/{trainerId}")]
        [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.Trainer}")]
        public async Task<ActionResult> AddPersonalTrainer(Guid clientId, Guid trainerId)
        {
            var id = HttpContext.User.FindFirstValue("Id");
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (!Guid.TryParse(id, out Guid requestUserId) || requestRole == null
            || (requestRole == RoleNames.Trainer && trainerId != requestUserId))
            {
                return Unauthorized();
            }

            await _clientFacade.AddPersonalTrainer(clientId, trainerId);

            return Ok();
        }
    }
}
