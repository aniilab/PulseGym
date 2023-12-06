using PulseGym.Entities.Enums;

namespace PulseGym.DAL.Models
{
    public class WorkoutRequest
    {
        public Guid Id { get; set; }

        public DateTime WorkoutDateTime { get; set; }

        public WorkoutRequestStatus Status { get; set; }

        public Guid ClientId { get; set; }

        public required Client Client { get; set; }

        public Guid TrainerId { get; set; }

        public required Trainer Trainer { get; set; }

        public Workout? Workout { get; set; }
    }
}
