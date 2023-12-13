namespace PulseGym.Logic.DTO
{
    public class ClientMembershipProgramViewDTO
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string WorkoutType { get; set; }

        public int WorkoutRemainder { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
