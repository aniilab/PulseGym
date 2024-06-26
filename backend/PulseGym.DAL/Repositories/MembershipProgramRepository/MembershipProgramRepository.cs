﻿using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;
using PulseGym.Entities.Exceptions;

namespace PulseGym.DAL.Repositories
{
    public class MembershipProgramRepository : IMembershipProgramRepository
    {
        private readonly PulseGymDbContext _context;

        public MembershipProgramRepository(PulseGymDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<MembershipProgram>> GetAllAsync()
        {
            return await _context.MembershipPrograms.ToListAsync();
        }

        public async Task<MembershipProgram> GetByIdAsync(Guid id)
        {
            var membershipProgram = await _context.MembershipPrograms.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new NotFoundException(nameof(MembershipProgram), id);

            return membershipProgram;
        }

        public async Task CreateAsync(MembershipProgram program)
        {
            await _context.MembershipPrograms.AddAsync(program);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, MembershipProgram program)
        {
            var foundProgram = await _context.MembershipPrograms.FindAsync(id)
               ?? throw new NotFoundException(nameof(MembershipProgram), id);

            program.Id = id;

            _context.MembershipPrograms.Update(program);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var foundProgram = await _context.MembershipPrograms.FindAsync(id)
               ?? throw new NotFoundException(nameof(MembershipProgram), id);

            _context.MembershipPrograms.Remove(foundProgram);

            await _context.SaveChangesAsync();
        }
    }
}
