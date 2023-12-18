namespace PulseGym.Entities.DTO
{
    public class MembershipProgramViewDTO
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public double Price { get; set; }
    }
}
