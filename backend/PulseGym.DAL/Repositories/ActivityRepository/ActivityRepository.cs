﻿using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;
using PulseGym.Entities.Exceptions;

namespace PulseGym.DAL.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly PulseGymDbContext _context;

        public ActivityRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Activity>> GetAllAsync()
        {
            return await _context.Activities.ToListAsync();
        }

        public async Task<Activity> GetByIdAsync(Guid id)
        {
            var activity = await _context.Activities.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new NotFoundException(nameof(Activity), id);

            return activity;
        }

        public async Task CreateAsync(Activity activity)
        {
            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, Activity activity)
        {
            var foundActivity = await _context.Activities.FindAsync(id)
               ?? throw new NotFoundException(nameof(Activity), id);

            foundActivity.Title = activity.Title;
            foundActivity.Description = activity.Description;
            foundActivity.ImageUrl = activity.ImageUrl;
            foundActivity.DateTime = activity.DateTime;

            _context.Activities.Update(foundActivity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var foundActivity = await _context.Activities.FindAsync(id)
               ?? throw new NotFoundException(nameof(Activity), id);

            _context.Activities.Remove(foundActivity);

            await _context.SaveChangesAsync();
        }
    }
}
