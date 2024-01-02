using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.Infrastructure;
using PulseGym.Logic.DTO;
using PulseGym.Logic.Facades;

namespace PulseGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipProgramController : ControllerBase
    {
        private readonly IMembershipProgramFacade _membershipProgramFacade;

        public MembershipProgramController(IMembershipProgramFacade membershipProgramFacade)
        {
            _membershipProgramFacade = membershipProgramFacade;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<MembershipProgramViewDTO>>> Get()
        {
            var programList = await _membershipProgramFacade.GetMembershipProgramsAsync();

            return Ok(programList);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult> Create(MembershipProgramInDTO membershipProgram)
        {
            await _membershipProgramFacade.CreateProgramAsync(membershipProgram);

            return Ok();
        }

        [HttpDelete("{programId}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult> Delete(Guid programId)
        {
            await _membershipProgramFacade.DeleteProgramAsync(programId);

            return Ok();
        }

        [HttpGet("Client/{clientId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<ClientMembershipProgramViewDTO>>> GetClientPrograms(Guid clientId)
        {
            var programList = await _membershipProgramFacade.GetClientProgramsAsync(clientId);

            return Ok(programList);
        }

        [HttpPost("Client/{clientId}")]
        [Authorize(Roles = RoleNames.Admin)]
        public async Task<ActionResult> AddProgramToClient(Guid clientId, Guid programId)
        {
            await _membershipProgramFacade.AddClientProgramAsync(clientId, programId);

            return Ok();
        }
    }
}
