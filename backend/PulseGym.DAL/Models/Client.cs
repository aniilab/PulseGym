using System.ComponentModel.DataAnnotations;

namespace PulseGym.DAL.Models
{
    public class Client
    {
        [Key]
        public Guid UserId { get; set; }

        public required User User { get; set; }

        public string? Goal { get; set; }

        public double? InitialWeight { get; set; }

        public double? InitialHeight { get; set; }

        public Guid? PersonalTrainerId { get; set; }

        public Trainer? PersonalTrainer { get; set; }

        public ICollection<Workout>? Workouts { get; set; }

        public ICollection<WorkoutRequest>? WorkoutRequests { get; set; }

        public ICollection<ClientMembershipProgram>? ClientMembershipPrograms { get; set; }
    }
}
