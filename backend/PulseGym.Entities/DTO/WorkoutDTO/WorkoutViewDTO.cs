namespace PulseGym.Entities.DTO
{
    public class WorkoutViewDTO
    {
        public Guid Id { get; set; }

        public DateTime WorkoutDateTime { get; set; }

        public string? Title { get; set; }

        public string? ExerciseDescription { get; set; }

        public required string Status { get; set; }

        public required ClientViewDTO Client { get; set; }

        public required TrainerViewDTO Trainer { get; set; }

    }
}
