using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Entities
{
    public class NormalWorkDay
    {
        public long Id { get; set; }
        public long ReceptionistId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public TimeSpan BreakStartHour { get; set; }
        public TimeSpan BreakEndHour { get; set; }

        public virtual Receptionist Receptionist { get; set; }
    }
}
