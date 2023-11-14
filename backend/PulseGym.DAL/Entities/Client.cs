using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PulseGym.DAL.Entities
{
    public class Client
    {
        [Key]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public required User User { get; set; }

        public string? Goal { get; set; }

        public double? InitialWeight { get; set; }

        public double? InitialHeight { get; set; }

        public Guid MembershipProgramId { get; set; }

        public required MembershipProgram MembershipProgram { get; set; }

        public Guid? PersonalTrainerId { get; set; }

        public Trainer? PersonalTrainer { get; set; }

        public ICollection<Activity>? Activities { get; set; }

        public ICollection<Workout>? Workouts { get; set; }

        public ICollection<WorkoutRequest>? WorkoutRequests { get; set; }

    }
}
