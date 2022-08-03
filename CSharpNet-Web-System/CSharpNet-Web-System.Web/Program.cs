using CSharpNet_Web_System.Data;
using CSharpNet_Web_System.Infrastructure.Seed;
using CSharpNet_Web_System.Models.Models;
using CSharpNet_Web_System.Services.Comments;
using CSharpNet_Web_System.Services.Courses;
using CSharpNet_Web_System.Services.Issues;
using CSharpNet_Web_System.Services.Posts;
using CSharpNet_Web_System.Services.Resources;
using CSharpNet_Web_System.Services.Tutorials;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CSharpNetWebDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CSharpNetWebDbContext>();

builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<ITutorialService, TutorialService>();
builder.Services.AddTransient<IResourceService, ResourceService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IIssueService, IssueService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IDbInitializer, DbInitializer>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

SeedDatabase();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.InitializeDatabase(app);
    }
}