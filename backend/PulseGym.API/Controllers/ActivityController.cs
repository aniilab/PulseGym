using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.DTO;
using PulseGym.Logic.Facades;

namespace PulseGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityFacade _activityFacade;

        public ActivityController(IActivityFacade activityFacade)
        {
            _activityFacade = activityFacade;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ActivityViewDTO>>> Get()
        {
            var activityList = await _activityFacade.GetActivitiesAsync();

            return Ok(activityList);
        }
    }
}
