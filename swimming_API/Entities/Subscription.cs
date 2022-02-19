using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Entities
{
    public class Subscription
    {
        public long Id { get; set; }
        public string SubscriptionName { get; set; }
        public int Cost { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
