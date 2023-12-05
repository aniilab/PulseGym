using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IMembershipProgramFacade
    {
        Task<ICollection<MembershipProgramViewDTO>> GetMembershipProgramsAsync();
    }
}
