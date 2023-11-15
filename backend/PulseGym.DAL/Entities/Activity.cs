using System.ComponentModel.DataAnnotations;

namespace PulseGym.DAL.Entities
{
    public class Activity
    {
        [Key]
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public int Category { get; set; }

        public DateTime DateTime { get; set; }

        public Guid? TrainerId { get; set; }

        public Trainer? Trainer { get; set; }

        public required ICollection<Client> Clients { get; set; }
    }
}
