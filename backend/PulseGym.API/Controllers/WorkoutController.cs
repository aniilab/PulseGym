using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.Enums;
using PulseGym.Entities.Infrastructure;
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
                || (requestRole != RoleNames.Admin && requestUserId != userId))
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
                || (requestRole == RoleNames.Trainer && workout.TrainerId != requestUserId)
                || (requestRole == RoleNames.Client && ((WorkoutType)workout.WorkoutType != WorkoutType.Solo
                    || !workout.ClientIds.Any(id => id == requestUserId))))
            {
                return Unauthorized();
            }

            await _workoutFacade.CreateWorkoutAsync(workout);

            return Ok();
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

            return Ok();
        }

        [HttpDelete("{workoutId}/Client/{clientId}")]
        [Authorize(Roles = $"{RoleNames.Client}, {RoleNames.Admin}")]
        public async Task<ActionResult> RemoveUser(Guid workoutId, Guid clientId)
        {
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId) || requestRole == null
                || (requestRole == RoleNames.Client && userId != clientId))
            {
                return Unauthorized();
            }

            await _workoutFacade.RemoveClientFromWorkoutAsync(workoutId, clientId);

            return Ok();
        }

        [HttpPut("{workoutId}")]
        [Authorize(Roles = $"{RoleNames.Trainer}, {RoleNames.Admin}")]
        public async Task<ActionResult> UpdateWorkout(Guid workoutId, WorkoutUpdateDTO workout)
        {
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId) || requestRole == null
                || (requestRole == RoleNames.Trainer && userId != workout.TrainerId))
            {
                return Unauthorized();
            }

            await _workoutFacade.UpdateWorkoutAsync(workoutId, workout);

            return Ok();
        }

        [HttpPut("Status/{workoutId}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult> UpdateWorkoutStatus(Guid workoutId)
        {
            await _workoutFacade.UpdateWorkoutStatusAsync(workoutId);

            return Ok();
        }

        [HttpGet("Requests/{role}/{userId}")]
        public async Task<ActionResult<ICollection<WorkoutRequestViewDTO>>> GetWorkoutRequestsByUserId(string role, Guid userId)
        {
            var id = HttpContext.User.FindFirstValue("Id");
            var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (!Guid.TryParse(id, out Guid requestUserId) || requestRole == null
                || (requestRole != RoleNames.Admin && requestUserId != userId))
            {
                return Unauthorized();
            }

            var workoutRequestList = await _workoutFacade.GetUserWorkoutRequestsAsync(role, userId);

            return Ok(workoutRequestList);
        }

        [HttpPost("Request")]
        [Authorize(Roles = RoleNames.Client)]
        public async Task<ActionResult> CreateWorkoutRequest(WorkoutRequestInDTO request)
        {
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId) || userId != request.ClientId)
            {
                return Unauthorized();
            }

            await _workoutFacade.CreateWorkoutRequestAsync(request);

            return Ok();
        }

        [HttpPut("AcceptRequest/{workoutRequestId}")]
        [Authorize(Roles = RoleNames.Trainer)]
        public async Task<ActionResult> AcceptWorkoutRequest(Guid workoutRequestId)
        {
            var id = HttpContext.User.FindFirstValue("Id");

            if (!Guid.TryParse(id, out Guid userId))
            {
                return Unauthorized();
            }

            await _workoutFacade.AcceptWorkoutRequestAsync(userId, workoutRequestId);

            return Ok();
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

            return Ok();
        }
    }
}
