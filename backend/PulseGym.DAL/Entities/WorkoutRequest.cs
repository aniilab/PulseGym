using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseGym.DAL.Entities
{
    public class WorkoutRequest
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid TrainerId { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }
        public Client Client { get; set; }
        public Trainer Trainer { get; set; }
        public Workout Workout { get; set; }

    }
}
