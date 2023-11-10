using Microsoft.EntityFrameworkCore;
using PulseGym.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseGym.DAL
{
    public class PulseGymDbContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<MembershipProgram> MembershipPrograms { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutRequest> WorkoutRequests { get; set; }
        public PulseGymDbContext(DbContextOptions<PulseGymDbContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasOne(a => a.Trainer)
                                           .WithMany(t => t.Activities)
                                           .HasForeignKey(a => a.TrainerId)
                                           .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Activity>().HasMany(a => a.Clients)
                                           .WithMany(c => c.Activities);


            modelBuilder.Entity<Client>().HasOne(c => c.User)
                                         .WithOne(u => u.Client)
                                         .HasForeignKey<Client>(c => c.UserId)
                                         .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Client>().HasOne(c => c.PersonalTrainer)
                                         .WithMany(t => t.Clients)
                                         .HasForeignKey(c => c.PersonalTrainerId)
                                         .OnDelete(DeleteBehavior.SetNull); 

            modelBuilder.Entity<Client>().HasOne(c => c.MembershipProgram)
                                         .WithMany(p => p.Clients)
                                         .HasForeignKey(c => c.MembershipProgram)
                                         .OnDelete(DeleteBehavior.Cascade); 


            modelBuilder.Entity<Trainer>().HasOne(t => t.User)
                                          .WithOne(u => u.Trainer)
                                          .HasForeignKey<Trainer>(t => t.UserId)
                                          .OnDelete(DeleteBehavior.Cascade); 


            modelBuilder.Entity<Workout>().HasOne(w => w.Client)
                                          .WithMany(c => c.Workouts)
                                          .HasForeignKey(w => w.ClientId)
                                          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Workout>().HasOne(w => w.Trainer)
                                          .WithMany(t => t.Workouts)
                                          .HasForeignKey(w => w.TrainerId)
                                          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Workout>().HasOne(w => w.WorkoutRequest)
                                          .WithOne(wr => wr.Workout)
                                          .HasForeignKey<Workout>(w => w.WorkoutRequestId)
                                          .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<WorkoutRequest>().HasOne(w => w.Client)
                                                 .WithMany(c => c.WorkoutRequests)
                                                 .HasForeignKey(w => w.ClientId)
                                                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutRequest>().HasOne(w => w.Trainer)
                                                 .WithMany(t => t.WorkoutRequests)
                                                 .HasForeignKey(w => w.TrainerId)
                                                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
