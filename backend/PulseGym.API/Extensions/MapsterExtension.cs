using Mapster;

using PulseGym.DAL.Models;
using PulseGym.Entities.DTO;
using PulseGym.Entities.Enums;

namespace PulseGym.API.Extensions
{
    public static class MapsterExtension
    {
        public static void AddMapsterConfiguration()
        {
            TypeAdapterConfig<Trainer, TrainerListItemDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.UserId)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.Category, src => ((TrainerCategory)src.Category).ToString());

            TypeAdapterConfig<Client, ClientListItemDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.UserId)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.MembershipProgram, src => src.MembershipProgram.Name);

        }
    }
}
