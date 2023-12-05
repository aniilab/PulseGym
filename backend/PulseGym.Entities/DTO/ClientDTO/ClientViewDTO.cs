namespace PulseGym.Entities.DTO
{
    public class ClientViewDTO
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? Goal { get; set; }

        public double? InitialWeight { get; set; }

        public double? InitialHeight { get; set; }

        public required string MembershipProgram { get; set; }

        public TrainerViewDTO? PersonalTrainer { get; set; }

    }
}
