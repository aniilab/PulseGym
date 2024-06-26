﻿using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;
using PulseGym.Entities.Exceptions;

namespace PulseGym.DAL.Repositories
{
    public class WorkoutRequestRepository : IWorkoutRequestRepository
    {
        private readonly PulseGymDbContext _context;

        public WorkoutRequestRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(WorkoutRequest entity)
        {
            await _context.WorkoutRequests.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<WorkoutRequest>> GetAllAsync()
        {
            return await _context.WorkoutRequests.Include(wr => wr.Trainer)
                                                    .ThenInclude(t => t.User)
                                                 .Include(wr => wr.Client)
                                                    .ThenInclude(c => c.User)
                                                 .Include(wr => wr.Workout)
                                                 .ToListAsync();
        }

        public async Task<WorkoutRequest> GetByIdAsync(Guid id)
        {
            var foundWorkoutRequest = await _context.WorkoutRequests.Include(wr => wr.Client).ThenInclude(c => c.User).FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new NotFoundException(nameof(WorkoutRequest), id);

            return foundWorkoutRequest;

        }

        public async Task UpdateAsync(Guid id, WorkoutRequest request)
        {
            var foundWorkoutRequest = await _context.WorkoutRequests.FindAsync(id)
               ?? throw new NotFoundException(nameof(WorkoutRequest), id);

            request.Id = id;

            _context.WorkoutRequests.Update(request);

            await _context.SaveChangesAsync();
        }
    }
}
