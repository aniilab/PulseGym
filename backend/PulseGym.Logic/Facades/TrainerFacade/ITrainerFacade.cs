using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public interface ITrainerFacade
    {
        Task<bool> CreateTrainerAsync(TrainerCreateDTO newTrainer);

        Task<bool> ExistsAsync(Guid userId);

        Task<ICollection<TrainerViewDTO>> GetTrainersAsync();

        Task<bool> CheckTrainerAvailabilityAsync(Guid userId, DateTime dateTime);

        Task<ICollection<DateTime>> GetOccupiedDateTimeAsync(Guid userId);
    }
}
