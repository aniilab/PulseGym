﻿using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;
using PulseGym.Entities.Exceptions;

namespace PulseGym.DAL.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly PulseGymDbContext _context;

        public TrainerRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Trainer>> GetAllAsync()
        {
            return await _context.Trainers.Include(t => t.User)
                                          .Include(t => t.Workouts)
                                          .Include(t => t.WorkoutRequests)
                                          .Include(t => t.Clients)
                                          .ToListAsync();
        }

        public async Task CreateAsync(Guid id, Trainer trainer)
        {
            trainer.UserId = id;

            await _context.Trainers.AddAsync(trainer);
            await _context.SaveChangesAsync();
        }

        public async Task<Trainer> GetByIdAsync(Guid userId)
        {
            var trainer = await _context.Trainers.Include(t => t.User)
                                                 .Include(t => t.Workouts)
                                                 .Include(t => t.WorkoutRequests)
                                                 .Include(t => t.Clients)
                                                 .FirstOrDefaultAsync(t => t.UserId == userId)
                ?? throw new NotFoundException(nameof(Trainer), userId);

            return trainer;
        }

        public async Task DeleteAsync(Guid trainerId)
        {
            var foundTrainer = await _context.Trainers.FindAsync(trainerId)
                ?? throw new NotFoundException(nameof(Trainer), trainerId);

            _context.Trainers.Remove(foundTrainer);

            await _context.SaveChangesAsync();
        }
    }
}
