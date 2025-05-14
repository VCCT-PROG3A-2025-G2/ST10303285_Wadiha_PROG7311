using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FarmersConnectWebApp.Data
{
    public class DataSeeder
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Farmer", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed a default Farmer
            var farmerUser = new IdentityUser { UserName = "farmer@farm.com", Email = "farmer@farm.com", EmailConfirmed = true };
            if (userManager.FindByEmailAsync(farmerUser.Email).Result == null)
            {
                var result = await userManager.CreateAsync(farmerUser, "Farmer123!");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(farmerUser, "Farmer");
            }

            // Seed a default Employee
            var employeeUser = new IdentityUser { UserName = "employee@company.com", Email = "employee@company.com", EmailConfirmed = true };
            if (userManager.FindByEmailAsync(employeeUser.Email).Result == null)
            {
                var result = await userManager.CreateAsync(employeeUser, "Employee123!");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(employeeUser, "Employee");
            }
        }
    }
}
