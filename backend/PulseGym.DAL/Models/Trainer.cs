using System.ComponentModel.DataAnnotations;

using PulseGym.Entities.Enums;

namespace PulseGym.DAL.Models
{
    public class Trainer
    {
        [Key]
        public Guid UserId { get; set; }

        public required User User { get; set; }

        public TrainerCategory Category { get; set; }

        public ICollection<Activity>? Activities { get; set; }

        public ICollection<Client>? Clients { get; set; }

        public ICollection<Workout>? Workouts { get; set; }

        public ICollection<WorkoutRequest>? WorkoutRequests { get; set; }
    }
}
