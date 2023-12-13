using Mapster;

using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Logic.DTO;

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

        public async Task<ActivityViewDTO> GetActivityAsync(Guid id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);

            return activity.Adapt<ActivityViewDTO>();
        }

        public async Task CreateActivityAsync(ActivityInDTO activityDTO)
        {
            var entity = activityDTO.Adapt<Activity>();

            await _activityRepository.CreateAsync(entity);
        }

        public async Task DeleteActivityAsync(Guid id)
        {
            await _activityRepository.DeleteAsync(id);
        }

        public async Task UpdateActivityAsync(Guid id, ActivityInDTO activityDTO)
        {
            var entity = activityDTO.Adapt<Activity>();

            await _activityRepository.UpdateAsync(id, entity);
        }
    }
}
