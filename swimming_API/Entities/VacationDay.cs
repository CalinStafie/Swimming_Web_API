using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Entities
{
    public class VacationDay
    {
        public long Id { get; set; }
        public long ReceptionistId { get; set; }
        public DateTime Date { get; set; }

        public virtual Receptionist Receptionist { get; set; }
        public virtual Address Address { get; set; }
    }
}
