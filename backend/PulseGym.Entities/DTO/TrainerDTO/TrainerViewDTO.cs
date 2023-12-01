namespace PulseGym.Entities.DTO
{
    public class TrainerViewDTO
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Category { get; set; }
        public ICollection<ActivityViewDTO>? Activities { get; set; }

        public ICollection<ClientViewDTO>? Clients { get; set; }

        public ICollection<WorkoutViewDTO>? Workouts { get; set; }

        public ICollection<WorkoutRequestViewDTO>? WorkoutRequests { get; set; }
    }
}
