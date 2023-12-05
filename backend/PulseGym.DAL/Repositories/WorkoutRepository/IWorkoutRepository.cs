using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IWorkoutRepository
    {
        Task<ICollection<Workout>> GetAllAsync();
    }
}
