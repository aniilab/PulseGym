﻿using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.DTO;
using PulseGym.Logic.Facades;

namespace PulseGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutFacade _workoutFacade;

        public WorkoutController(IWorkoutFacade workoutFacade)
        {
            _workoutFacade = workoutFacade;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<WorkoutViewDTO>>> GetWorkouts()
        {
            var workoutList = await _workoutFacade.GetWorkoutsAsync();

            return Ok(workoutList);
        }

        [HttpGet("Requests")]
        public async Task<ActionResult<ICollection<WorkoutRequestViewDTO>>> GetWorkoutRequests()
        {
            var workoutRequestList = await _workoutFacade.GetWorkoutRequestsAsync();

            return Ok(workoutRequestList);
        }

        [HttpPost("Request")]
        [Authorize(Roles = "client")]
        public async Task<ActionResult> CreateWorkoutRequest(WorkoutRequestInDTO request)
        {
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId) || userId != request.ClientId)
            {
                return Unauthorized();
            }

            await _workoutFacade.CreateWorkoutRequestAsync(request);

            return Ok("Created successfully!");
        }

        [HttpPost("Create")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateWorkout(WorkoutInDTO workout)
        {
            await _workoutFacade.CreateWorkoutAsync(workout);

            return Ok("Created successfully!");
        }

        [HttpPut("AcceptRequest/{workoutRequestId}")]
        [Authorize(Roles = "trainer")]
        public async Task<ActionResult> AcceptWorkoutRequest(Guid workoutRequestId)
        {
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId))
            {
                return Unauthorized();
            }

            await _workoutFacade.AcceptWorkoutRequestAsync(userId, workoutRequestId);

            return Ok("Accepted successfully!");
        }

        [HttpDelete("DeclineRequest/{workoutRequestId}")]
        public async Task<ActionResult> DeclineWorkoutRequest(Guid workoutRequestId)
        {
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId))
            {
                return Unauthorized();
            }

            await _workoutFacade.DeclineWorkoutRequestAsync(userId, workoutRequestId);

            return Ok("Declined successfully!");
        }

        [HttpPut("Update/{workoutId}")]
        [Authorize(Roles = "trainer")]
        public async Task<ActionResult> UpdateWorkout(Guid workoutId, WorkoutUpdateDTO workout)
        {
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId))
            {
                return Unauthorized();
            }

            await _workoutFacade.UpdateWorkoutAsync(workoutId, workout, userId);

            return Ok("Updated successfully!");
        }

        [HttpDelete("Cancel/{id}")]
        public async Task<ActionResult> CancelWorkout(Guid workoutId)
        {
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId))
            {
                return Unauthorized();
            }

            await _workoutFacade.CancelWorkoutAsync(userId, workoutId);

            return Ok("Cancelled successfully!");
        }
    }
}
