using PulseGym.Entities.Enums;

namespace PulseGym.Logic.DTO
{
    public class GroupClassViewDTO
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public int MaxClientNumber { get; set; }

        public required ClassLevel Level { get; set; }
    }
}
