using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.DAL.Enums;
using PulseGym.Logic.DTO;
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

        [HttpGet("Group")]
        public async Task<ActionResult<ICollection<WorkoutViewDTO>>> GetGroupWorkouts(DateTime dateFrom, DateTime dateTo)
        {
            var workoutList = await _workoutFacade.GetGroupWorkoutsAsync(dateFrom, dateTo);

            return Ok(workoutList);
        }

        [HttpGet("{role}/{userId}")]
        public async Task<ActionResult<ICollection<WorkoutViewDTO>>> GetByUserId(DateTime dateFrom, DateTime dateTo, string role, Guid userId)
        {
            var id = HttpContext.User.FindFirstValue("Id");
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (!Guid.TryParse(id, out Guid requestUserId) || requestRole == null
                || (requestRole != "admin" && requestUserId != userId))
            {
                return Unauthorized();
            }

            var workoutList = await _workoutFacade.GetUserWorkoutsAsync(dateFrom, dateTo, role, userId);

            return Ok(workoutList);
        }

        [HttpPost]
        public async Task<ActionResult> CreateWorkout(WorkoutInDTO workout)
        {
            var id = HttpContext.User.FindFirstValue("Id");
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (!Guid.TryParse(id, out Guid requestUserId) || requestRole == null
                || (requestRole == "trainer" && workout.TrainerId != requestUserId)
                || (requestRole == "client" && ((WorkoutType)workout.WorkoutType != WorkoutType.Solo || !workout.ClientIds.Any(id => id == requestUserId))))
            {
                return Unauthorized();
            }

            await _workoutFacade.CreateWorkoutAsync(workout);

            return Ok("Created successfully!");
        }

        [HttpDelete("{workoutId}")]
        public async Task<ActionResult> CancelWorkout(Guid workoutId)
        {
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId) || requestRole == null)
            {
                return Unauthorized();
            }

            await _workoutFacade.CancelWorkoutAsync(userId, requestRole, workoutId);

            return Ok("Cancelled successfully!");
        }

        [HttpDelete("{workoutId}/Client/{clientId}")]
        [Authorize(Roles = "client, admin")]
        public async Task<ActionResult> RemoveUser(Guid workoutId, Guid clientId)
        {
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId) || requestRole == null
                || (requestRole == "client" && userId != clientId))
            {
                return Unauthorized();
            }

            await _workoutFacade.RemoveUserFromWorkoutAsync(workoutId, clientId);

            return Ok("Removed successfully!");
        }

        [HttpPut("{workoutId}")]
        [Authorize(Roles = "trainer, admin")]
        public async Task<ActionResult> UpdateWorkout(Guid workoutId, WorkoutUpdateDTO workout)
        {
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId) || requestRole == null
                || (requestRole == "trainer" && userId != workout.TrainerId))
            {
                return Unauthorized();
            }

            await _workoutFacade.UpdateWorkoutAsync(workoutId, workout);

            return Ok("Updated successfully!");
        }


        [HttpGet("Requests/{role}/{userId}")]
        public async Task<ActionResult<ICollection<WorkoutRequestViewDTO>>> GetWorkoutRequestsByUserId(string role, Guid userId)
        {
            var id = HttpContext.User.FindFirstValue("Id");
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (!Guid.TryParse(id, out Guid requestUserId) || requestRole == null
                || (requestRole != "admin" && requestUserId != userId))
            {
                return Unauthorized();
            }

            var workoutRequestList = await _workoutFacade.GetUserWorkoutRequestsAsync(role, userId);

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
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (!Guid.TryParse(id, out Guid userId) || requestRole == null)
            {
                return Unauthorized();
            }

            await _workoutFacade.DeclineWorkoutRequestAsync(userId, requestRole, workoutRequestId);

            return Ok("Declined successfully!");
        }


    }
}
