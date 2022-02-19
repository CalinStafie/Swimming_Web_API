using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Models
{
    public class ClientUserPostModel
    {
        public RegisterModel registerModel { get; set; }
        public ClientDTO clientDTO { get; set; }
    }
}
