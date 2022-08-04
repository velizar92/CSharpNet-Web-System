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
                await SeedCourses(services);
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

        private async Task SeedCourses(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<CSharpNetWebDbContext>();

            List<Course> courses = new List<Course>()
            {
                new Course
                {
                    Name = "C# Basics",
                    ImageUrl = "csharp_basics.jpeg",
                    Description = "Курсът C# Basics ще ви даде основни познания в програмирането със C#." +
                    "Започва се от самото начало с това \"Що е то програмен език?\", \"Какво е .NET?\" " +
                    "\"Какво е IDE\"?, \"Какво е Visual Studio и как да го използвате\", \"Какво е променлива?\", " +
                    "Кои са if/else конструкциите, for/while/do while и foreach циклите и куп други интересни неща. " +
                    "\nНека започнем! :)"
                },
                new Course
                {
                    Name = "C# Fundamentals",
                    ImageUrl = "csharp_fund.jpg",
                    Description = "Курсът C# Fundamentals стъпва върху наученото от вас в C# Basics. Тук ще се запознаете с листове, масиви," +
                    "асоциативни масиви и ще разберете какво са класовете и обектите и как те ви помагат. Ще разберете също какво са" +
                    "методите и как се работи с тях. " +
                    "\nНека започнем! :)"

                },
                new Course
                {
                    Name = "C# Advanced",
                    ImageUrl = "csharp_advanced.jpg",
                    Description = "Курсът C# Advanced ще ви даде познания за работата с различни структури от данни като: Стек, Опашка, Хешсет, Речник и др. " +
                    "Ще обърнем внимание на работата с многомерни масиви, а също така и с файлове. Ще се запознаете с функционалното програмиране в C# и какво е това LINQ." +
                    "Ще затвърдите знаният си, които вече имате за класовете и обектите. Ще се научите също да създавате шаблонни класове. " +
                    "\nНека започнем! :)"
                },
                new Course
                {
                    Name = "C# OOP",
                    ImageUrl = "csharp_oop.jpg",
                    Description = "Курсът C# OOP ще ви даде познания в Обектно Ориентираното Програмиране. Ще се запознаете с основните стълбове " +
                    "на OOП: Абстракция, Енкапсулация, Наследяване и Полиморфизъм. Ще разберете какво е това \"Exception\" и как да използвате try/catch/finally конструцията за обработване на изключения" +
                    "(Exceptions). Ще разберете що е то Interface и защо е толкова полезен. Ще научите SOLID принципите и ще затвърдите знанията си за това как да ги прилагате. Ще се запознаете с" +
                    "Unit тестването и с възможността да Mock-вате обекти. Ще се запознаете и с някои Design Patterns. " +
                    "\nНека започнем! :)"                  
                },

            };

            if (dbContext.Courses.Any() == false) 
            {
                await dbContext.Courses.AddRangeAsync(courses);
                await dbContext.SaveChangesAsync();
            }
        }


    }
}
