﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Models.DTO_s
{
    public class ReceptionistDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string JobDescription { get; set; }

        public int UserId { get; set; }
    }
}
