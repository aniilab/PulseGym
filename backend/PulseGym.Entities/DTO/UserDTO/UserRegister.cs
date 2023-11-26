﻿namespace PulseGym.Entities.DTO
{
    public class UserRegister
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

    }
}
