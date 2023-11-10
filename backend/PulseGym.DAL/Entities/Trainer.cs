using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseGym.DAL.Entities
{
    public class Trainer
    {
        [Key]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public ICollection<int> Categories { get; set; }
        
        public User User { get; set; }

        public ICollection<Activity> Activities { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<Workout> Workouts { get; set; }
    }
}
