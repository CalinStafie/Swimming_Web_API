using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Models
{
    public class ClientPurchasesModel
    {
        public string ReceptionistName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SubscriptionName { get; set; }
    }
}
