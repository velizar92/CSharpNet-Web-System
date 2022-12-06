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
            // TODO: Lets define DbException class and log "result.Errors" if occured. Also to rename the package name from "Seed" to "DB"/"Database" or smth.
            // Same fits for other DB operations i.e create user from WebSite UI.
            // Actions to be done in this task - CSWS-102
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
            // TODO: User manager can be passed as method param as well (already initialized above). Same applies for DB context.
            // Actions to be done in this task - CSWS-100
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
                    ProfileImageUrl = "admin_avatar.jpg",
                    EmailConfirmed = true
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

            // TODO: Replacing the hardcoded strings with already existing constants (for the roles).
            // Actions to be done with this task - CSWS-100
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

            //TODO: Transfer to constants or Enum.
            // Actions to be done with this task - CSWS-100          
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

            // TODO: Export this stuff to resource files when done.
            // Actions to be done in CSWS-103
            List<Course> courses = new List<Course>()
            {
                new Course
                {
                    Name = "C# Basics",
                    ImageUrl = "csharp_basics.jpeg",
                    Description = "Курсът C# Basics ще ви даде основни познания в програмирането със C#. " +
                    "Започва се от самото начало с това \"Що е то програмен език?\", \"Какво е .NET?\" " +
                    "\"Какво е IDE\"?, \"Какво е Visual Studio и как да го използвате\", \"Какво е променлива?\", " +
                    "Кои са if/else конструкциите, for/while/do while и foreach циклите и куп други интересни неща. ",
                    Tutorials = new List<Tutorial>()
                    {
                        new Tutorial
                        {
                            Title = "Въведение в .NET и C#",
                            Description = "В тази тема ще се запознаем с .NET платформата " +
                            "и езика C#.",                          
                        },
                        new Tutorial
                        {
                            Title = "Visual Studio IDE",
                            Description = "В тази тема ще научим какво е среда за разработка " +
                            "и по-конкретно ще се запознаем с Visual Studio."
                        },
                        new Tutorial
                        {
                            Title = "Visual Studio 2022 Community - инсталация",
                            Description = "В тази тема ще покажем как се инсталира Visual Studio 2022 Community."
                        },
                        new Tutorial
                        {
                            Title = "Създаване на C# конзолен проект във Visual Studio.",
                            Description = "В тази тема ще покажем как се създава конзолен " +
                            "C# проект във Visual Studio."
                        },
                        new Tutorial
                        {
                            Title = "Архитектура на C# програмите.",
                            Description = "В тази тема ще покажем каква е архитектурата на " +
                            "C# програмите като цяло."
                        },
                        new Tutorial
                        {
                            Title = "Работа с конзолата.",
                            Description = "В тази тема ще разберем какво е конзолата и как да работим " +
                            "с нея."
                        },
                        new Tutorial
                        {
                            Title = "Типове данни, променливи и константи.",
                            Description = "В тази тема ще разберем как да боравим с различни " +
                            "типове данни, променливи и константи."
                        },
                        new Tutorial
                        {
                            Title = "Променливи и работа с конзолата.",
                            Description = "В тази тема ще затвърдим знанията си за конзолата и " +
                            "работата с променливи."
                        },
                        new Tutorial
                        {
                            Title = "If-else конструкции.",
                            Description = "В тази тема ще научим как да работим с условна логика."
                        },
                        new Tutorial
                        {
                            Title = "Switch-case конструкция.",
                            Description = "В тази тема ще научим как да работим със switch-case."
                        },
                        new Tutorial
                        {
                            Title = "Цикли.",
                            Description = "В тази тема ще научим как да работим с цикли."
                        },
                        new Tutorial
                        {
                            Title = "Вложени цикли.",
                            Description = "В тази тема ще научим как да работим с вложени цикли."
                        }
                    }
                },
                new Course
                {
                    Name = "C# Fundamentals",
                    ImageUrl = "csharp_fund.jpg",
                    Description = "Курсът C# Fundamentals стъпва върху наученото от вас в C# Basics. Тук ще се запознаете с листове, масиви," +
                    "асоциативни масиви и ще разберете какво са класовете и обектите и как те ви помагат. Ще разберете също какво са" +
                    "методите и как се работи с тях. "
                },
                new Course
                {
                    Name = "C# Advanced",
                    ImageUrl = "csharp_advanced.jpg",
                    Description = "Курсът C# Advanced ще ви даде познания за работата с различни структури от данни като: Стек, Опашка, Хешсет, Речник и др. " +
                    "Ще обърнем внимание на работата с многомерни масиви, а също така и с файлове. Ще се запознаете с функционалното програмиране в C# и какво е това LINQ." +
                    "Ще затвърдите знаният си, които вече имате за класовете и обектите. Ще се научите също да създавате шаблонни класове. "
                },
                new Course
                {
                    Name = "C# OOP",
                    ImageUrl = "csharp_oop.jpg",
                    Description = "Курсът C# OOP ще ви даде познания в Обектно Ориентираното Програмиране. Ще се запознаете с основните стълбове " +
                    "на OOП: Абстракция, Енкапсулация, Наследяване и Полиморфизъм. Ще разберете какво е това \"Exception\" и как да използвате try/catch/finally конструцията за обработване на изключения" +
                    "(Exceptions). Ще разберете що е то Interface и защо е толкова полезен. Ще научите SOLID принципите и ще затвърдите знанията си за това как да ги прилагате. Ще се запознаете с" +
                    "Unit тестването и с възможността да Mock-вате обекти. Ще се запознаете и с някои Design Patterns. "
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
