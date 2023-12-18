using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IActivityRepository
    {
        Task<ICollection<Activity>> GetAllAsync();

        Task<Activity> GetByIdAsync(Guid id);

        Task CreateAsync(Activity activity);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(Guid id, Activity activity);
    }
}
