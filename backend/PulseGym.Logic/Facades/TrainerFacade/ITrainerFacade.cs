using PulseGym.Entities.DTO.TrainerDTO;

namespace PulseGym.Logic.Facades.TrainerFacade
{
    public interface ITrainerFacade
    {
        Task<bool> CreateTrainerAsync(TrainerCreate newTrainer);

        Task<ICollection<TrainerListItem>> GetTrainersAsync();
    }
}
