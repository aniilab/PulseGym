using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IMembershipProgramFacade
    {
        Task CreateProgramAsync(MembershipProgramInDTO membershipProgram);

        Task<ICollection<MembershipProgramViewDTO>> GetMembershipProgramsAsync();
    }
}
