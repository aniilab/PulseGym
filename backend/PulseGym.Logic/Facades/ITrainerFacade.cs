using PulseGym.Entities.DTO.Trainer;

namespace PulseGym.Logic.Facades
{
    public interface ITrainerFacade
    {
        Task<bool> CreateTrainerAsync(TrainerCreate newTrainer);

        Task<ICollection<TrainerListItem>> GetTrainersAsync();
    }
}
