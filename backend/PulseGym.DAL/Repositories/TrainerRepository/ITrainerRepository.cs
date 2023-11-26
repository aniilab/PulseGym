using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface ITrainerRepository
    {
        Task<bool> CreateAsync(Guid id, Trainer trainer);
        Task<ICollection<Trainer>> GetAllAsync();
    }
}
