using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Entities.DTO;
using PulseGym.Logic.Facades;

namespace PulseGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
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
        public async Task<ActionResult> Create(MembershipProgramInDTO membershipProgram)
        {
            await _membershipProgramFacade.CreateProgramAsync(membershipProgram);

            return Ok("Created successfully!");
        }
    }
}
