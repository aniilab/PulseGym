using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseGym.DAL.Enums
{
    public enum WorkoutRequestStatus
    {
        New = 0,
        AcceptedToSchedule = 1,
        DeclinedByAdmin = 2,
        DeclinedByTrainer = 3,
        DeclinedByClient = 4
    }
}
