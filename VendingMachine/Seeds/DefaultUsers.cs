﻿using VendingMachine.Core.Constants;
using VendingMachine.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace VendingMachine.Seeds
{
    public class DefaultUsers
    {
        public static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            User buyer = new()
            {
                UserName = "Buyer@VendingMachine.com",
                Email = "Buyer@VendingMachine.com",
                EmailConfirmed = true
            };
            User seller = new()
            {
                UserName = "Seller@VendingMachine.com",
                Email = "Seller@VendingMachine.com",
                EmailConfirmed = true
            };
            await CreateUser(userManager, buyer, AppRoles.Buyer, "P@ssword123");
            await CreateUser(userManager, seller, AppRoles.Seller, "P@ssword123");
        }
        private static async Task CreateUser(UserManager<User> userManager, User userToCreate, string role, string password)
        {
            var user = await userManager.FindByNameAsync(userToCreate.UserName!);
            if (user is null)
            {
                await userManager.CreateAsync(userToCreate, password);
                await userManager.AddToRoleAsync(userToCreate, role);
            }
        }
    }
}
