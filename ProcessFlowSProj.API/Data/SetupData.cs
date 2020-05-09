using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProcessFlowSProj.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Data
{
    public class SetupData
    {
        //private readonly UserManager<StaffEntity> _userManager;
        //private readonly RoleManager<Role> _roleManager;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        //public SetupData(UserManager<StaffEntity> userManager, RoleManager<Role> roleManager)
        //{
        //    _roleManager = roleManager;
        //    _userManager = userManager;
        //}

        public SetupData(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async void SeedAdminUser()
        {
            var adminUsername = "adminUser";
            var scope = _serviceScopeFactory.CreateScope();
            var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<StaffEntity>>();
            var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            if (!_userManager.Users.Where(u => u.UserName == adminUsername).Any())
            {
                //create some roles
                var roles = new List<Role>
                {
                    new Role{Name = "Administrator"}
                };

                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }


                var adminUser = new StaffEntity
                {
                    UserName = adminUsername,
                    FirstName = "Administrator",
                    LastName = "Administrator",
                    Gender = "Male",
                    RoleEntityId = -1    //adminRole
                };

                var result = await _userManager.CreateAsync(adminUser, "password");

                if(result.Succeeded)
                {
                    var admin = await _userManager.FindByNameAsync(adminUsername);
                    await _userManager.AddToRoleAsync(admin, "Administrator");
                }
            }
        }
    }
}
