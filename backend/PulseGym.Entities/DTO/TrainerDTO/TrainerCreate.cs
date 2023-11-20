namespace PulseGym.Entities.DTO.TrainerDTO
{
    public class TrainerCreate
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public int Category { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
