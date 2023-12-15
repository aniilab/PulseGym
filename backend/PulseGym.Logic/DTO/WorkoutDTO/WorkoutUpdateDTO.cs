namespace PulseGym.Logic.DTO
{
    public class WorkoutUpdateDTO
    {
        public DateTime? WorkoutDateTime { get; set; }

        public string? Notes { get; set; }

        public Guid? TrainerId { get; set; }

        public Guid? GroupClassId { get; set; }
    }
}
