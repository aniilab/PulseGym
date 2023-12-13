namespace PulseGym.Logic.DTO
{
    public class MembershipProgramInDTO
    {
        public required string Name { get; set; }

        public double Price { get; set; }

        public int Duration { get; set; }

        public int WorkoutType { get; set; }

        public int WorkoutNumber { get; set; }
    }
}
