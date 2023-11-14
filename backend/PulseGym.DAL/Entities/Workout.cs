namespace PulseGym.DAL.Entities
{
    public class Workout
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid TrainerId { get; set; }

        public DateTime WorkoutDateTime { get; set; }
        public required string Title { get; set; }
        public string? ExerciseDescription { get; set; }
        public int Status { get; set; }
        public required Client Client { get; set; }
        public required Trainer Trainer { get; set; }
        public Guid? WorkoutRequestId { get; set; }
        public WorkoutRequest? WorkoutRequest { get; set; }

    }
}
