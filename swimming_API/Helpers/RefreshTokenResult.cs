using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Helpers
{
    public class RefreshTokenResult
    {
        public bool Success { get; set; }
        public string NewAccessToken { get; set; }
    }
}
