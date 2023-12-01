namespace PulseGym.Entities.DTO
{
    public class ClientListItemDTO
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string MembershipProgram { get; set; }
    }
}
