namespace PulseGym.Logic.DTO
{
    public class GroupClassInDTO
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public int MaxClientNumber { get; set; }

        public required int Level { get; set; }
    }
}
