using Microsoft.AspNetCore.Identity;

namespace PulseGym.DAL.Entities
{
    public class User : IdentityUser<Guid>
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string? ImageUrl { get; set; }

        public Client? Client { get; set; }

        public Trainer? Trainer { get; set; }
    }
}
