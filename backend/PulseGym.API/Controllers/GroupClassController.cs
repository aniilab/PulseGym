using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PulseGym.Logic.DTO;
using PulseGym.Logic.Facades;

namespace PulseGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupClassController : ControllerBase
    {
        private readonly IGroupClassFacade _groupClassFacade;

        public GroupClassController(IGroupClassFacade groupClassFacade)
        {
            _groupClassFacade = groupClassFacade;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<GroupClassViewDTO>>> Get()
        {
            var groupClassList = await _groupClassFacade.GetGroupClassesAsync();

            return Ok(groupClassList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupClassViewDTO>> GetById(Guid id)
        {
            var groupClass = await _groupClassFacade.GetGroupClassAsync(id);

            return Ok(groupClass);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(GroupClassInDTO groupClass)
        {
            await _groupClassFacade.CreateGroupClassAsync(groupClass);

            return Ok("Created successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _groupClassFacade.DeleteGroupClassAsync(id);

            return Ok("Deleted successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Update(Guid id, GroupClassInDTO groupClass)
        {
            await _groupClassFacade.UpdateGroupClassAsync(id, groupClass);

            return Ok("Updated successfully");
        }
    }
}
