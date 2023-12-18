namespace PulseGym.Logic.DTO
{
    public class ActivityViewDTO
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime DateTime { get; set; }

        public string? ImageUrl { get; set; }
    }
}
