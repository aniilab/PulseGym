using PulseGym.Entities.Enums;

namespace PulseGym.Logic.DTO
{
    public class TrainerViewDTO
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required WorkoutType Category { get; set; }
    }
}
