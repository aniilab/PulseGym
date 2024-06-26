﻿using PulseGym.Entities.Enums;

namespace PulseGym.Logic.DTO
{
    public class MembershipProgramViewDTO
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public double Price { get; set; }

        public int Duration { get; set; }

        public required WorkoutType WorkoutType { get; set; }

        public int WorkoutNumber { get; set; }
    }
}
