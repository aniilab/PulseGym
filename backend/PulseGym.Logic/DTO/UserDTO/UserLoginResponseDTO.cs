namespace PulseGym.Logic.DTO
{
    public class UserLoginResponseDTO
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? ImageUrl { get; set; }

        public required string Role { get; set; }
    }
}
