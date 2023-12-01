namespace PulseGym.Entities.DTO
{
    public class TrainerListItemDTO
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Category { get; set; }
    }
}
