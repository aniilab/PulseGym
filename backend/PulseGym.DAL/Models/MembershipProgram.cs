﻿using PulseGym.DAL.Enums;

namespace PulseGym.DAL.Models
{
    public class MembershipProgram
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public int Duration { get; set; }

        public WorkoutType WorkoutType { get; set; }

        public int WorkoutNumber { get; set; }

        public ICollection<ClientMembershipProgram>? ClientMembershipPrograms { get; set; }
    }
}
