using PulseGym.Entities.Enums;

namespace PulseGym.Logic.DTO
{
    public class WorkoutViewDTO
    {
        public Guid Id { get; set; }

        public DateTime WorkoutDateTime { get; set; }

        public string? Notes { get; set; }

        public required WorkoutStatus Status { get; set; }

        public required WorkoutType WorkoutType { get; set; }

        public TrainerViewDTO? Trainer { get; set; }

        public GroupClassViewDTO? GroupClass { get; set; }

        public ICollection<ClientViewDTO>? Clients { get; set; }
    }
}
