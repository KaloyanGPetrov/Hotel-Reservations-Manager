using Hotel_Reservations_Manager.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hotel_Reservations_Manager.Data.Seeder
{
    public static class UserSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            await SeedRoles(roleManager);

            await SeedAllUsers(userManager);
        }

        private static async Task SeedAllUsers(UserManager<User> userManager)
        {
            var adminUser = new User
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };
            await SeedUser(userManager, adminUser, "Admin#123", "Admin");
            var regularUser = new User
            {
                UserName = "user@user.com",
                Email = "user@user.com",
                EmailConfirmed = true
            };
            await SeedUser(userManager, regularUser, "User#123", "User");
        }

        private static async Task SeedUser(UserManager<User> userManager, User adminuser, string password, string roleName)
        {
            var user = await userManager.FindByEmailAsync(adminuser.Email);
            if (user == null)
            {
                var created = await userManager
                    .CreateAsync(adminuser, password);
                if (created.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminuser, roleName);
                }
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };
            foreach (var role in roleNames)
            {
                bool roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
