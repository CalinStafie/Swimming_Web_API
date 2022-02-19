using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Models
{
    public class PurchasesInformationModel
    {
        public ClientDTO clientDTO { get; set; }
        public ReceptionistDTO receptionistDTO { get; set; }
        public SubscriptionDTO subscriptionDTO{ get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
