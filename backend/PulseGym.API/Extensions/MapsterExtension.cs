using Mapster;

using PulseGym.DAL.Models;
using PulseGym.Entities.Enums;
using PulseGym.Logic.DTO;

namespace PulseGym.API.Extensions
{
    public static class MapsterExtension
    {
        public static void AddMapsterConfiguration()
        {
            // Client
            TypeAdapterConfig<Client, ClientViewDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.UserId)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName);

            // Trainer
            TypeAdapterConfig<Trainer, TrainerViewDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.UserId)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName);

            // MembershipProgram
            TypeAdapterConfig<ClientMembershipProgram, ClientMembershipProgramViewDTO>.NewConfig()
                .Map(dest => dest.Name, src => src.MembershipProgram!.Name)
                .Map(dest => dest.WorkoutType, src => src.MembershipProgram!.WorkoutType);

            // Workout
            TypeAdapterConfig<WorkoutInDTO, Workout>.NewConfig()
                .Map(dest => dest.Status, src => WorkoutStatus.Planned)
                .Map(dest => dest.Clients, src => new List<Client>());

            // WorkoutRequest
            TypeAdapterConfig<WorkoutRequestInDTO, WorkoutRequest>.NewConfig()
                .Map(dest => dest.Status, src => WorkoutRequestStatus.New);

            TypeAdapterConfig<WorkoutRequest, Workout>.NewConfig()
                .Map(dest => dest.WorkoutRequestId, src => src.Id)
                .Map(dest => dest.Status, src => WorkoutStatus.Planned)
                .Map(dest => dest.WorkoutType, src => WorkoutType.Personal)
                .Map(dest => dest.Clients, src => new List<Client>() { src.Client });
        }
    }
}
