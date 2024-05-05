using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.Infrastructure;
using PulseGym.Logic.Facades;
using PulseGym.Logic.Services;

namespace PulseGym.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OpenAIController : ControllerBase
{
    private readonly IOpenAIService _openAIService;
    private readonly IClientFacade _clientFacade;

    public OpenAIController(IOpenAIService openAIService, IClientFacade clientFacade)
    {
        _openAIService = openAIService;
        _clientFacade = clientFacade;
    }

    [HttpGet("{scheduleType}")]
    public async Task<ActionResult<string>> GetTrainerOccupiedTime(string scheduleType)
    {
        var id = HttpContext.User.FindFirstValue("Id");
        var requestRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

        if (!Guid.TryParse(id, out Guid requestUserId) || requestRole == null || requestRole != RoleNames.Client)
        {
            return Unauthorized();
        }

        var client = await _clientFacade.GetClientAsync(requestUserId);

        var response = await _openAIService.GenerateSchedule(scheduleType, client);

        return Ok(response);
    }
}
