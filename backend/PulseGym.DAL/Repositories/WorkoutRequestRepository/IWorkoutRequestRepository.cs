using PulseGym.DAL.Models;

namespace PulseGym.DAL.Repositories
{
    public interface IWorkoutRequestRepository
    {
        Task<ICollection<WorkoutRequest>> GetAllAsync();
    }
}
