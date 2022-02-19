using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Entities
{
    public class User : IdentityUser<int>
    {
        public string RefreshToken { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual Client Client { get; set; }
        public virtual Receptionist Receptionist { get; set; }
    }
}
