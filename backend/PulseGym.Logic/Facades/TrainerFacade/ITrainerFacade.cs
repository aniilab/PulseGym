using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public interface ITrainerFacade
    {
        Task<bool> CreateTrainerAsync(TrainerCreateDTO newTrainer);

        Task<ICollection<TrainerViewDTO>> GetTrainersAsync();
    }
}
