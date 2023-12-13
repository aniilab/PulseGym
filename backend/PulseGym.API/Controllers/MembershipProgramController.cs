using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(MembershipProgramInDTO membershipProgram)
        {
            await _membershipProgramFacade.CreateProgramAsync(membershipProgram);

            return Ok("Created successfully!");
        }

        [HttpDelete("{programId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(Guid programId)
        {
            await _membershipProgramFacade.DeleteProgramAsync(programId);

            return Ok("Deleted successfully!");
        }

        [HttpGet("Client/{clientId}")]
        [Authorize]
        public async Task<ActionResult<ICollection<ClientMembershipProgramViewDTO>>> GetClientPrograms(Guid clientId)
        {
            var programList = await _membershipProgramFacade.GetClientProgramsAsync(clientId);

            return Ok(programList);
        }

        [HttpPost("Client/{clientId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddProgramToClient(Guid clientId, Guid programId)
        {
            await _membershipProgramFacade.AddClientProgramAsync(clientId, programId);

            return Ok("Added successfully");
        }
    }
}
