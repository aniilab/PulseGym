using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PulseGym.DAL.Models;

namespace PulseGym.DAL
{
    public class PulseGymDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Activity> Activities { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientMembershipProgram> ClientMembershipPrograms { get; set; }

        public DbSet<GroupClass> GroupClasses { get; set; }

        public DbSet<MembershipProgram> MembershipPrograms { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Workout> Workouts { get; set; }

        public DbSet<WorkoutRequest> WorkoutRequests { get; set; }

        public PulseGymDbContext(DbContextOptions<PulseGymDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasOne(c => c.User)
                                         .WithOne(u => u.Client)
                                         .HasForeignKey<Client>(c => c.UserId)
                                         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>().HasOne(c => c.PersonalTrainer)
                                         .WithMany(t => t.Clients)
                                         .HasForeignKey(c => c.PersonalTrainerId)
                                         .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ClientMembershipProgram>().HasOne(cmp => cmp.Client)
                                                          .WithMany(c => c.ClientMembershipPrograms)
                                                          .HasForeignKey(cmp => cmp.ClientId)
                                                          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClientMembershipProgram>().HasOne(cmp => cmp.MembershipProgram)
                                                          .WithMany(p => p.ClientMembershipPrograms)
                                                          .HasForeignKey(cmp => cmp.MembershipProgramId)
                                                          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Trainer>().HasOne(t => t.User)
                                          .WithOne(u => u.Trainer)
                                          .HasForeignKey<Trainer>(t => t.UserId)
                                          .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Workout>().HasMany(w => w.Clients)
                                          .WithMany(c => c.Workouts);

            modelBuilder.Entity<Workout>().HasOne(w => w.Trainer)
                                          .WithMany(t => t.Workouts)
                                          .HasForeignKey(w => w.TrainerId)
                                          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Workout>().HasOne(w => w.WorkoutRequest)
                                          .WithOne(wr => wr.Workout)
                                          .HasForeignKey<Workout>(w => w.WorkoutRequestId)
                                          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Workout>().HasOne(w => w.GroupClass)
                                          .WithMany(gc => gc.Workouts)
                                          .HasForeignKey(w => w.GroupClassId)
                                          .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<WorkoutRequest>().HasOne(w => w.Client)
                                                 .WithMany(c => c.WorkoutRequests)
                                                 .HasForeignKey(w => w.ClientId)
                                                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutRequest>().HasOne(w => w.Trainer)
                                                 .WithMany(t => t.WorkoutRequests)
                                                 .HasForeignKey(w => w.TrainerId)
                                                 .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
