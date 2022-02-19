using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Models.DTO_s
{
    public class SubscriptionDTO
    {
        public long Id { get; set; }
        public string SubscriptionName { get; set; }
        public int Cost { get; set; }
    }
}
