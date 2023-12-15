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
    public class TrainerController : ControllerBase
    {
        ITrainerFacade _trainerFacade;

        public TrainerController(ITrainerFacade trainerFacade)
        {
            _trainerFacade = trainerFacade;
        }

        [HttpPost("Create")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult> Create(TrainerCreateDTO newTrainer)
        {
            var isCreated = await _trainerFacade.CreateTrainerAsync(newTrainer);

            if (isCreated)
            {
                return Ok("Created successfully!");
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult<ICollection<TrainerViewDTO>>> Get()
        {
            var trainerList = await _trainerFacade.GetTrainersAsync();

            return Ok(trainerList);
        }

        [HttpGet("OccupiedTime/{id}")]
        public async Task<ActionResult<ICollection<DateTime>>> GetTrainerOccupiedTime(Guid id)
        {
            var dateTimeList = await _trainerFacade.GetOccupiedDateTimeAsync(id);

            return Ok(dateTimeList);
        }
    }
}
