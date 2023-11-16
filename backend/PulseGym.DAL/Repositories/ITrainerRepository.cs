using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface ITrainerRepository
    {
        Task<bool> CreateTrainerAsync(Guid id, Trainer trainer);
        Task<ICollection<Trainer>> GetTrainersAsync();
    }
}
