namespace CSharpNet_Web_System.Infrastructure.Seed
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Data;
    using CSharpNet_Web_System.Models.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class DbInitializer : IDbInitializer
    {
        public async Task InitializeDatabase(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<User>>();

                await SeedResourceTypes(services);
                await SeedRoles(services);
                await SeedAdminUsers(services);
            }
        }


        private async Task SeedAdminUsers(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var dbContext = services.GetRequiredService<CSharpNetWebDbContext>();

            if (userManager.Users.Any() == false)
            {
                User adminUser = new User
                {
                    FirstName = "Velizar",
                    LastName = "Gerasimov",
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    ProfileImageUrl = "admin_avatar.jpg"
                };

                IdentityResult result = await userManager.CreateAsync(adminUser, "test123");
                await userManager.AddClaimAsync(adminUser, new Claim("ProfileImageUrl", adminUser.ProfileImageUrl));

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                }

                await dbContext.SaveChangesAsync();
            }
        }


        private async Task SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var dbContext = services.GetRequiredService<CSharpNetWebDbContext>();

            if (roleManager.RoleExistsAsync("Admin").Result == false)
            {
                IdentityRole adminRole = new IdentityRole();
                adminRole.Name = "Admin";
                IdentityResult roleResult = await roleManager.CreateAsync(adminRole);
            }

            if (roleManager.RoleExistsAsync("Learner").Result == false)
            {
                IdentityRole learnerRole = new IdentityRole();
                learnerRole.Name = "Learner";
                IdentityResult roleResult = await roleManager.CreateAsync(learnerRole);
            }

            await dbContext.SaveChangesAsync();
        }


        private async Task SeedResourceTypes(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<CSharpNetWebDbContext>();

            List<ResourceType> resourceTypes = new List<ResourceType>
            {
                new ResourceType{ Name = "PPT Presentation" },
                new ResourceType{ Name = "Video MP4" },
                new ResourceType{ Name = "PDF Document" },
            };

            if (dbContext.ResourceTypes.Any() == false)
            {
                await dbContext.ResourceTypes.AddRangeAsync(resourceTypes);
                await dbContext.SaveChangesAsync();
            }
        }


    }
}
