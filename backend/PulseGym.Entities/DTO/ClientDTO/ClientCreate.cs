namespace PulseGym.Entities.DTO
{
    public class ClientCreate
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public Guid MembershipProgramId { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
