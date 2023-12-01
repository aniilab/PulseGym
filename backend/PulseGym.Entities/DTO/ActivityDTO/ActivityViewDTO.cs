namespace PulseGym.Entities.DTO
{
    public class ActivityViewDTO
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string Category { get; set; }

        public DateTime DateTime { get; set; }

        public TrainerViewDTO? Trainer { get; set; }

        public required ICollection<ClientViewDTO> Clients { get; set; }
    }
}
