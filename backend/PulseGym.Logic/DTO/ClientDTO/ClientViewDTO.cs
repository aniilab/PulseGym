namespace PulseGym.Logic.DTO
{
    public class ClientViewDTO
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required DateTime Birthday { get; set; }

        public string? ImageUrl { get; set; }

        public string? Goal { get; set; }

        public double? InitialWeight { get; set; }

        public double? InitialHeight { get; set; }

        public TrainerViewDTO? PersonalTrainer { get; set; }

        public ICollection<MembershipProgramViewDTO>? MembershipPrograms { get; set; }
    }
}
