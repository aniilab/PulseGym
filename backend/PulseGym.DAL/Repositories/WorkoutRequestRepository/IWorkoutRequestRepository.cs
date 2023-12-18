using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IWorkoutRequestRepository
    {
        Task CreateAsync(WorkoutRequest entity);

        Task<ICollection<WorkoutRequest>> GetAllAsync();

        Task<WorkoutRequest> GetByIdAsync(Guid id);

        Task UpdateAsync(Guid id, WorkoutRequest request);
    }
}
