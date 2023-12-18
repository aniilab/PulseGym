using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IGroupClassFacade
    {
        Task<GroupClassViewDTO> GetGroupClassAsync(Guid id);
        Task<ICollection<GroupClassViewDTO>> GetGroupClassesAsync();

        Task CreateGroupClassAsync(GroupClassInDTO groupClassInDTO);

        Task DeleteGroupClassAsync(Guid id);

        Task UpdateGroupClassAsync(Guid id, GroupClassInDTO groupClassInDTO);
    }
}
