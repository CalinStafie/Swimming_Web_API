using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Entities
{
    public class Purchase
    {
        public long ClientId { get; set; }
        public long ReceptionistId { get; set; }
        public long SubscriptionId { get; set; }
        public DateTime StartTime { get; set; } // data incepere abonament(poate sa nu coincida cu data la care a fost emis)
        public DateTime EndTime { get; set; } // data sfarsit abonament

        public virtual Client Client { get; set; }
        public virtual Receptionist Receptionist { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}
