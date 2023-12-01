﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.DTO;
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
        public async Task<ActionResult<ICollection<TrainerListItemDTO>>> Get()
        {
            var trainerList = await _trainerFacade.GetTrainersAsync();

            return Ok(trainerList);
        }

    }
}
