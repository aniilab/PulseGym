using PulseGym.DAL.Enums;

namespace PulseGym.DAL.Models
{
    public class GroupClass
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public int MaxClientNumber { get; set; }

        public ClassLevel Level { get; set; }

        public ICollection<Workout>? Workouts { get; set; }
    }
}
