using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Entities
{
    public class Receptionist
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string JobDescription { get; set; }

        public virtual ICollection<NormalWorkDay> NormalWorkDays { get; set; }
        public virtual ICollection<VacationDay> VacationDays { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
