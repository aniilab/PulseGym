using Mapster;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Facades
{
    public class GroupClassFacade : IGroupClassFacade
    {
        private readonly IGroupClassRepository _groupClassRepository;

        public GroupClassFacade(IGroupClassRepository groupClassRepository)
        {
            _groupClassRepository = groupClassRepository;
        }

        public async Task<ICollection<GroupClassViewDTO>> GetGroupClassesAsync()
        {
            var groupClassesList = await _groupClassRepository.GetAllAsync();

            return groupClassesList.Adapt<List<GroupClassViewDTO>>();
        }

        public async Task<GroupClassViewDTO> GetGroupClassAsync(Guid id)
        {
            var groupClass = await _groupClassRepository.GetByIdAsync(id);

            return groupClass.Adapt<GroupClassViewDTO>();
        }

        public async Task CreateGroupClassAsync(GroupClassInDTO groupClassInDTO)
        {
            var entity = groupClassInDTO.Adapt<GroupClass>();

            await _groupClassRepository.CreateAsync(entity);
        }

        public async Task DeleteGroupClassAsync(Guid id)
        {
            await _groupClassRepository.DeleteAsync(id);
        }

        public async Task UpdateGroupClassAsync(Guid id, GroupClassInDTO groupClassInDTO)
        {
            var entity = groupClassInDTO.Adapt<GroupClass>();

            await _groupClassRepository.UpdateAsync(id, entity);
        }
    }
}
