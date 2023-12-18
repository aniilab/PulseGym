using PulseGym.Entities.Enums;

namespace PulseGym.Logic.DTO
{
    public class WorkoutInDTO
    {
        public DateTime WorkoutDateTime { get; set; }

        public string? Notes { get; set; }

        public WorkoutType WorkoutType { get; set; }

        public Guid? TrainerId { get; set; }

        public Guid? GroupClassId { get; set; }

        public required ICollection<Guid> ClientIds { get; set; }
    }
}
