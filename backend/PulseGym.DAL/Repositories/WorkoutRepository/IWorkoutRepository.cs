using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IWorkoutRepository
    {
        Task<Workout> CreateAsync(Workout workout);

        Task<ICollection<Workout>> GetAllAsync();

        Task<Workout> GetByIdAsync(Guid workoutId);

        Task UpdateAsync(Guid workoutId, Workout workout);
    }
}
