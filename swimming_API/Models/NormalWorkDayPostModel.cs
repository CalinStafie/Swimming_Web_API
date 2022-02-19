using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Models
{
    public class NormalWorkDayPostModel
    {
        public long Id { get; set; }
        public long ReceptionistId { get; set; }
        public int DayOfWeek { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
        public string BreakStartHour { get; set; }
        public string BreakEndHour { get; set; }
    }
}
