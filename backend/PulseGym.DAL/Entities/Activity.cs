using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseGym.DAL.Entities
{
    public class Activity
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public DateTime DateTime { get; set; }
        public Guid? TrainerId { get; set; }

        public Trainer? Trainer { get; set; }

        public ICollection<Client> Clients { get; set;}
    }
}
