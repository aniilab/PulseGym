namespace PulseGym.Logic.DTO
{
    public class WorkoutRequestInDTO
    {
        public DateTime WorkoutDateTime { get; set; }

        public Guid ClientId { get; set; }

        public Guid TrainerId { get; set; }
    }
}
