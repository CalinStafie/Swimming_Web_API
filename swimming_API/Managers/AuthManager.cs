using Microsoft.AspNetCore.Identity;
using proiect_final_API.Entities;
using proiect_final_API.Helpers;
using proiect_final_API.Models;
using proiect_final_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager, ITokenHelper tokenHelper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHelper = tokenHelper;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "Email not found"
                };

            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
            if (result.Succeeded)
            {
                var token = await _tokenHelper.CreateAccessToken(user);
                var refreshToken = _tokenHelper.CreateRefreshToken();

                user.RefreshToken = refreshToken;
                await _userManager.UpdateAsync(user);

                return new LoginResult
                {
                    Success = true,
                    Message = "Logged in",
                    AccessToken = token,
                    RefreshToken = refreshToken
                };
            }
            return new LoginResult
            {
                Success = false,
                Message = "Wrong password"
            };
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {
            var user = new User
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
            };

            // Encrypt password
            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, registerModel.Role);
                return true;
            }
            return false;
        }

        public async Task<int?> RegisterAsClient(RegisterModel registerModel)
        {
            var user = new User
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
            };

            // Encrypt password
            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Client");
                return user.Id;
            }
            return null;
        }

        public async Task<int?> RegisterAsReceptionist(RegisterModel registerModel)
        {
            var user = new User
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
            };

            // Encrypt password
            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Receptionist");
                return user.Id;
            }
            return null;
        }

        public async Task<RefreshTokenResult> Refresh(RefreshModel refreshModel)
        {
            var principal = _tokenHelper.GetPrincipalFromExpiredToken(refreshModel.AccessToken);
            var username = principal.Identity.Name;

            var user = await _userManager.FindByEmailAsync(username);

            if (user.RefreshToken != refreshModel.RefreshToken)
            {
                return new RefreshTokenResult
                {
                    Success = false
                };
            }

            var newJwtToken = await _tokenHelper.CreateAccessToken(user);

            return new RefreshTokenResult
            {
                Success = true,
                NewAccessToken = newJwtToken
            };
        }
    }
}
