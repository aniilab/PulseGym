using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface ITrainerRepository
    {
        Task CreateAsync(Guid id, Trainer trainer);

        Task<ICollection<Trainer>> GetAllAsync();

        Task<Trainer> GetByIdAsync(Guid userId);

        Task DeleteAsync(Guid trainerId);
    }
}
