using Cards.Common.Enums;
using Cards.Common.Extensions;
using Cards.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Cards.Infrastructure.DataAccess.DataSeed;

public static class InitialData
{
    public static async Task SeedUsers(this UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            foreach (var role in EnumUtilExtension.GetEnumeratedValues<Roles>())
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = role.Name
                });
            }
        }

        if (!userManager.Users.Any())
        {
            
            var users = new ApplicationUser[]
            {
                new()
                {
                    UserName = "admin@cards.dev",
                    Email = "admin@cards.dev",
                    EmailConfirmed = true
                },
                new()
                {
                    UserName = "user@cards.dev",
                    Email = "user@cards.dev",
                    EmailConfirmed = true
                },
                new()
                {
                    UserName = "user.test@cards.dev",
                    Email = "user.test@cards.dev",
                    EmailConfirmed = true
                },
                new()
                {
                    UserName = "test.user@cards.dev",
                    Email = "test.user@cards.dev",
                    EmailConfirmed = true
                }
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Password@123");
                await userManager.AddToRoleAsync(user, user.UserName!.Contains("admin") ? nameof(Roles.Admin):nameof(Roles.Member));
            }
        }
    }
}