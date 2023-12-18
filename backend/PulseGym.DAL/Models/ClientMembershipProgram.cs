namespace PulseGym.DAL.Models
{
    public class ClientMembershipProgram
    {
        public Guid Id { get; set; }

        public Guid MembershipProgramId { get; set; }

        public MembershipProgram? MembershipProgram { get; set; }

        public Guid ClientId { get; set; }

        public Client? Client { get; set; }

        public int WorkoutRemainder { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
