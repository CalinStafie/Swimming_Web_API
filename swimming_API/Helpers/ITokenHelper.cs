using proiect_final_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace proiect_final_API.Helpers
{
    public interface ITokenHelper
    {
        Task<string> CreateAccessToken(User user);

        public string CreateRefreshToken();

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string _Token);
    }
}
