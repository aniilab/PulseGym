using PulseGym.Entities.Enums;

namespace PulseGym.Logic.DTO
{
    public class WorkoutRequestViewDTO
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public required WorkoutRequestStatus Status { get; set; }

        public required ClientViewDTO Client { get; set; }

        public required TrainerViewDTO Trainer { get; set; }
    }
}
