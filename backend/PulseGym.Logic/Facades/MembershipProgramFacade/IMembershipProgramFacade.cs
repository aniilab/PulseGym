using PulseGym.Entities.Enums;
using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IMembershipProgramFacade
    {
        Task CreateProgramAsync(MembershipProgramInDTO membershipProgram);

        Task<ICollection<MembershipProgramViewDTO>> GetMembershipProgramsAsync();

        Task UpdateProgramAsync(Guid id, MembershipProgramInDTO membershipProgramInDTO);

        Task DeleteProgramAsync(Guid id);

        Task<ICollection<ClientMembershipProgramViewDTO>> GetClientProgramsAsync(Guid clientId);

        Task AddClientProgramAsync(Guid clientId, Guid programId);

        Task ChangeWorkoutRemainderCount(Guid clientId, WorkoutType workoutType, bool isUsed);
    }
}
