namespace PulseGym.Entities.DTO
{
    public class WorkoutInDTO
    {
        public DateTime WorkoutDateTime { get; set; }

        public Guid ClientId { get; set; }

        public Guid TrainerId { get; set; }
    }
}
