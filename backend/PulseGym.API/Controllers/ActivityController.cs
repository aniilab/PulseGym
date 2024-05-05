using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.Infrastructure;
using PulseGym.Logic.DTO;
using PulseGym.Logic.Facades;

namespace PulseGym.API.Controllers;

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

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityViewDTO>> GetById(Guid id)
    {
        var activity = await _activityFacade.GetActivityAsync(id);

        return Ok(activity);
    }

    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<ActionResult> Create(ActivityInDTO activityDTO)
    {
        await _activityFacade.CreateActivityAsync(activityDTO);

        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<ActionResult> Update(Guid id, ActivityInDTO activityDTO)
    {
        await _activityFacade.UpdateActivityAsync(id, activityDTO);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _activityFacade.DeleteActivityAsync(id);

        return Ok();
    }
}
