using proiect_final_API.Helpers;
using proiect_final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Managers
{
    public interface IAuthManager
    {
        Task<bool> Register(RegisterModel registerModel);

        Task<int?> RegisterAsClient(RegisterModel registerModel);

        Task<int?> RegisterAsReceptionist(RegisterModel registerModel);

        Task<LoginResult> Login(LoginModel loginModel);

        Task<RefreshTokenResult> Refresh(RefreshModel refreshModel);
    }
}
