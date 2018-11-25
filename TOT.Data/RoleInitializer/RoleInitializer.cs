using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TOT.Entities;
using TOT.Entities.IdentityEntities;

namespace TOT.Data.RoleInitializer
{
    public static class RoleInitializer
    {
        private const string adminEmail = "Admin@gmail.com";
        private const string password = "Admin";
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (await rolesManager.FindByNameAsync(Roles.Admin) == null)
            {
                await rolesManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if (await rolesManager.FindByNameAsync(Roles.Approver) == null)
            {
                await rolesManager.CreateAsync(new IdentityRole(Roles.Approver));
            }
            if (await userManager.FindByNameAsync("Admin") == null)
            {
                User admin = new User {
                    UserName = "Admin",
                    Email = adminEmail,
                    Fired = false,
                    Name = "Admin",
                    Surname = "Admin",
                    Patronymic = "Admin",
                    PositionId =1,
                    HireDate = new DateTime(2000, 01, 01)
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }
        }
    }
}
