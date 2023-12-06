using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IWorkoutRepository
    {
        Task CreateAsync(Workout newWorkout);
        Task<ICollection<Workout>> GetAllAsync();
    }
}
