using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public interface IActivityFacade
    {
        Task<ICollection<ActivityViewDTO>> GetActivitiesAsync();
    }
}
