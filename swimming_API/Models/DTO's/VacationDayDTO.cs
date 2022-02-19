using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Models.DTO_s
{
    public class VacationDayDTO
    {
        public long Id { get; set; }
        public long ReceptionistId { get; set; }
        public DateTime Date { get; set; }
    }
}
