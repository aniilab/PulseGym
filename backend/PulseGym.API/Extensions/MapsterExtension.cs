using Mapster;

using PulseGym.DAL.Models;
using PulseGym.Entities.DTO;

namespace PulseGym.API.Extensions
{
    public static class MapsterExtension
    {
        public static void AddMapsterConfiguration()
        {
            TypeAdapterConfig<Trainer, TrainerViewDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.UserId)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.Category, src => (src.Category).ToString());

            TypeAdapterConfig<Client, ClientViewDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.UserId)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.MembershipProgram, src => src.MembershipProgram.Name);

            TypeAdapterConfig<Activity, ActivityViewDTO>.NewConfig()
             .Map(dest => dest.Category, src => (src.Category).ToString());

            TypeAdapterConfig<Workout, WorkoutViewDTO>.NewConfig()
            .Map(dest => dest.Status, src => (src.Status).ToString());

            TypeAdapterConfig<WorkoutRequest, WorkoutRequestViewDTO>.NewConfig()
           .Map(dest => dest.Status, src => (src.Status).ToString());
        }
    }
}
