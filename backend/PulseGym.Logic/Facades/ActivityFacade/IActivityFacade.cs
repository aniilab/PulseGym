using PulseGym.Logic.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IActivityFacade
    {
        Task<ICollection<ActivityViewDTO>> GetActivitiesAsync();

        Task<ActivityViewDTO> GetActivityAsync(Guid id);

        Task UpdateActivityAsync(Guid id, ActivityInDTO activityDTO);

        Task DeleteActivityAsync(Guid id);

        Task CreateActivityAsync(ActivityInDTO activityDTO);
    }
}
