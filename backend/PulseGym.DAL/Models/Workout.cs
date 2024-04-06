using System.ComponentModel.DataAnnotations;

using PulseGym.Entities.Enums;

namespace PulseGym.DAL.Models
{
    public class Workout
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime WorkoutDateTime { get; set; }

        public string? Notes { get; set; }

        public WorkoutStatus Status { get; set; }

        public WorkoutType WorkoutType { get; set; }

        public Guid? TrainerId { get; set; }

        public Trainer? Trainer { get; set; }

        public Guid? GroupClassId { get; set; }

        public GroupClass? GroupClass { get; set; }

        public Guid? WorkoutRequestId { get; set; }

        public WorkoutRequest? WorkoutRequest { get; set; }

        public required ICollection<Client> Clients { get; set; }

        public Workout()
        {
            Clients = new List<Client>();
        }
    }
}
