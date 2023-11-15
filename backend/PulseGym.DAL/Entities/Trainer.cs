using System.ComponentModel.DataAnnotations;

namespace PulseGym.DAL.Entities
{
    public class Trainer
    {
        [Key]
        public Guid UserId { get; set; }

        public required User User { get; set; }

        public int Category { get; set; }

        public ICollection<Activity>? Activities { get; set; }

        public ICollection<Client>? Clients { get; set; }

        public ICollection<Workout>? Workouts { get; set; }

        public ICollection<WorkoutRequest>? WorkoutRequests { get; set; }
    }
}
