namespace PulseGym.DAL.Models
{
    public class Activity
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime DateTime { get; set; }

        public string? ImageUrl { get; set; }
    }
}
