﻿namespace PulseGym.Entities.DTO
{
    public class UserLoginRequestDTO
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
