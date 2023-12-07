using Mapster;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public class MembershipProgramFacade : IMembershipProgramFacade
    {
        private readonly IMembershipProgramRepository _membershipProgramRepository;

        public MembershipProgramFacade(IMembershipProgramRepository membershipProgramRepository)
        {
            _membershipProgramRepository = membershipProgramRepository;
        }

        public async Task<ICollection<MembershipProgramViewDTO>> GetMembershipProgramsAsync()
        {
            var programs = await _membershipProgramRepository.GetAllAsync();

            return programs.Adapt<List<MembershipProgramViewDTO>>();
        }

        public async Task CreateProgramAsync(MembershipProgramInDTO membershipProgram)
        {
            var entity = membershipProgram.Adapt<MembershipProgram>();

            await _membershipProgramRepository.CreateAsync(entity);
        }
    }
}
