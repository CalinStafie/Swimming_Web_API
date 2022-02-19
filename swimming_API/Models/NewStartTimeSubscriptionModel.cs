﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Models
{
    public class NewStartTimeSubscriptionModel
    {
        public long ClientId { get; set; }
        public long ReceptionistId { get; set; }
        public long SubscriptionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime NewStartTime { get; set; }
    }
}
