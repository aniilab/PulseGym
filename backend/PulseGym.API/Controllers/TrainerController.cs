﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.DTO.TrainerDTO;
using PulseGym.Logic.Facades.TrainerFacade;

namespace PulseGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        ITrainerFacade _trainerFacade;

        public TrainerController(ITrainerFacade trainerFacade)
        {
            _trainerFacade = trainerFacade;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddTrainerAsync(TrainerCreate newTrainer)
        {
            var isCreated = await _trainerFacade.CreateTrainerAsync(newTrainer);

            if (isCreated)
            {
                return Ok("Created successfully!");
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetTrainersAsync()
        {
            var trainerList = await _trainerFacade.GetTrainersAsync();

            return Ok(trainerList);
        }

    }
}
