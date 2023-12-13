using Mapster;

using PulseGym.DAL.Enums;
using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Facades
{
    public class MembershipProgramFacade : IMembershipProgramFacade
    {
        private readonly IMembershipProgramRepository _membershipProgramRepository;
        private readonly IClientMembershipProgramRepository _clientMembershipProgramRepository;

        public MembershipProgramFacade(IMembershipProgramRepository membershipProgramRepository, IClientMembershipProgramRepository clientMembershipProgramRepository)
        {
            _membershipProgramRepository = membershipProgramRepository;
            _clientMembershipProgramRepository = clientMembershipProgramRepository;
        }

        public async Task<ICollection<MembershipProgramViewDTO>> GetMembershipProgramsAsync()
        {
            var programs = await _membershipProgramRepository.GetAllAsync();

            return programs.Adapt<List<MembershipProgramViewDTO>>();
        }

        public async Task CreateProgramAsync(MembershipProgramInDTO membershipProgramInDTO)
        {
            var entity = membershipProgramInDTO.Adapt<MembershipProgram>();

            await _membershipProgramRepository.CreateAsync(entity);
        }

        public async Task DeleteProgramAsync(Guid id)
        {
            await _membershipProgramRepository.DeleteAsync(id);
        }

        public async Task UpdateProgramAsync(Guid id, MembershipProgramInDTO membershipProgramInDTO)
        {
            var entity = membershipProgramInDTO.Adapt<MembershipProgram>();

            await _membershipProgramRepository.UpdateAsync(id, entity);
        }

        public async Task<ICollection<ClientMembershipProgramViewDTO>> GetClientProgramsAsync(Guid clientId)
        {
            var programList = await _clientMembershipProgramRepository.GetByClientIdAsync(clientId);

            return programList.Adapt<List<ClientMembershipProgramViewDTO>>();
        }

        public async Task AddClientProgramAsync(Guid clientId, Guid programId)
        {
            var program = await _membershipProgramRepository.GetByIdAsync(programId);

            var entity = new ClientMembershipProgram()
            {
                ClientId = clientId,
                MembershipProgramId = programId,
                WorkoutRemainder = program.WorkoutNumber,
                ExpirationDate = DateTime.UtcNow.AddDays(program.Duration)
            };

            await _clientMembershipProgramRepository.CreateAsync(entity);
        }

        public async Task ChangeWorkoutRemainderCount(Guid clientId, WorkoutType workoutType, bool isUsed)
        {
            var clientPrograms = await _clientMembershipProgramRepository.GetByClientIdAsync(clientId);

            var clientProgram = clientPrograms.Where(cmp => cmp.MembershipProgram!.WorkoutType == workoutType).FirstOrDefault()
                ?? throw new Exception($"Client with Id {clientId} does not have program with Workout Type {workoutType}");

            clientProgram.WorkoutRemainder = isUsed ? clientProgram.WorkoutRemainder-- : clientProgram.WorkoutRemainder++;

            await _clientMembershipProgramRepository.UpdateAsync(clientProgram.Id, clientProgram);
        }
    }
}
