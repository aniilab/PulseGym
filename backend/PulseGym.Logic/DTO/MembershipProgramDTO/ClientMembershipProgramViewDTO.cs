using PulseGym.Entities.Enums;

namespace PulseGym.Logic.DTO
{
    public class ClientMembershipProgramViewDTO
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required WorkoutType WorkoutType { get; set; }

        public int WorkoutRemainder { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
