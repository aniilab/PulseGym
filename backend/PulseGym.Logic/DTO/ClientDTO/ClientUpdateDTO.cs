﻿namespace PulseGym.Logic.DTO
{
    public class ClientUpdateDTO
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required DateTime Birthday { get; set; }

        public required string Goal { get; set; }

        public decimal InitialWeight { get; set; }

        public decimal InitialHeight { get; set; }
    }
}