using System.ComponentModel.DataAnnotations;

namespace PulseGym.DAL.Models
{
    public class MembershipProgram
    {
        [Key]
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public double Price { get; set; }

        public ICollection<Client>? Clients { get; set; }
    }
}
