using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IActivityRepository
    {
        Task<ICollection<Activity>> GetAllAsync();
    }
}
