using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseGym.DAL.Entities
{
    public class Client
    {
        [Key]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid? PersonalTrainerId { get; set; }
        public string Goal {  get; set; }
        public double InitialWeight { get; set; }
        public double CurrentWeight {  get; set; }
        public double Height { get; set; }

        public MembershipProgram MembershipProgram { get; set; }
        public Trainer? PersonalTrainer { get; set; }
        public User User { get; set; }

        public ICollection<Activity> Activities { get; set; }
        public ICollection<Workout> Workouts { get; set; }

    }
}
