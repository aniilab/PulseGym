using Mapster;

using PulseGym.Entities.Enums;
using PulseGym.DAL.Models;
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
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.Category, src => src.Category.ToString());

            // GroupClass
            TypeAdapterConfig<GroupClass, GroupClassViewDTO>.NewConfig()
                .Map(dest => dest.Level, src => src.Level.ToString());

            TypeAdapterConfig<GroupClassInDTO, GroupClass>.NewConfig()
                .Map(dest => dest.Level, src => (ClassLevel)src.Level);

            // MembershipProgram
            TypeAdapterConfig<MembershipProgram, MembershipProgramViewDTO>.NewConfig()
                .Map(dest => dest.WorkoutType, src => src.WorkoutType.ToString());

            TypeAdapterConfig<MembershipProgramInDTO, MembershipProgram>.NewConfig()
                .Map(dest => dest.WorkoutType, src => (WorkoutType)src.WorkoutType);

            TypeAdapterConfig<ClientMembershipProgram, ClientMembershipProgramViewDTO>.NewConfig()
                .Map(dest => dest.Name, src => src.MembershipProgram.Name)
                .Map(dest => dest.WorkoutType, src => src.MembershipProgram.WorkoutType.ToString());

            // Workout
            TypeAdapterConfig<Workout, WorkoutViewDTO>.NewConfig()
                .Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.WorkoutType, src => src.WorkoutType.ToString());

            TypeAdapterConfig<WorkoutInDTO, Workout>.NewConfig()
                .Map(dest => dest.Status, src => WorkoutStatus.Planned)
                .Map(dest => dest.WorkoutType, src => (WorkoutType)src.WorkoutType)
                .Map(dest => dest.Clients, src => new List<Client>());

            // WorkoutRequest
            TypeAdapterConfig<WorkoutRequest, WorkoutRequestViewDTO>.NewConfig()
                .Map(dest => dest.Status, src => src.Status.ToString());

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
