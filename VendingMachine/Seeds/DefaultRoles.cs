using VendingMachine.Core.Constants;
using Microsoft.AspNetCore.Identity;

namespace VendingMachine.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Buyer));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Seller));
            }
        }
    }
}
