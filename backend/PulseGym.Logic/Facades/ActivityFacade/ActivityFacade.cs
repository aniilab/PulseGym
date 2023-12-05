using Mapster;

using PulseGym.DAL.Repositories;
using PulseGym.Entities.DTO;

namespace PulseGym.Logic.Facades
{
    public class ActivityFacade : IActivityFacade
    {
        IActivityRepository _activityRepository;

        public ActivityFacade(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        public async Task<ICollection<ActivityViewDTO>> GetActivitiesAsync()
        {
            var activities = await _activityRepository.GetAllAsync();

            return activities.Adapt<List<ActivityViewDTO>>();
        }
    }
}
