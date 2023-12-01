using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.DTO;

using PulseGym.Logic.Facades;

namespace PulseGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        IClientFacade _clientFacade;

        public ClientController(IClientFacade clientFacade)
        {
            _clientFacade = clientFacade;
        }

        [HttpPost("Create")]
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
        public async Task<ActionResult<ICollection<ClientListItemDTO>>> Get()
        {
            var clientList = await _clientFacade.GetClientsAsync();

            return Ok(clientList);
        }
    }
}
