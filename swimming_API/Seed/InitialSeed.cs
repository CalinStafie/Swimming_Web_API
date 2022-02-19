using Microsoft.AspNetCore.Identity;
using proiect_final_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Seed
{
    public class InitialSeed
    {
        private readonly RoleManager<Role> _roleManager;

        public InitialSeed(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

#pragma warning disable CS1998 
        public async void CreateRoles()
#pragma warning restore CS1998 
        {
            string[] roleNames =
            {
                "Admin",
                "Receptionist",
                "Client"
            };
            foreach (var roleName in roleNames)
            {
                var role = new Role
                {
                    Name = roleName
                };

                _roleManager.CreateAsync(role).Wait();
            }
        }
    }
}
