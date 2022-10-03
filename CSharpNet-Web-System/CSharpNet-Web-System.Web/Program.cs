using CSharpNet_Web_System.Data;
using CSharpNet_Web_System.Infrastructure.Seed;
using CSharpNet_Web_System.Models.Models;
using CSharpNet_Web_System.Services.Comments;
using CSharpNet_Web_System.Services.Courses;
using CSharpNet_Web_System.Services.Email;
using CSharpNet_Web_System.Services.Issues;
using CSharpNet_Web_System.Services.Resources;
using CSharpNet_Web_System.Services.Storage;
using CSharpNet_Web_System.Services.Tutorials;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CSharpNetWebDbContext>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;

    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<ITutorialService, TutorialService>();
builder.Services.AddTransient<IResourceService, ResourceService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IIssueService, IssueService>();
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IDbInitializer, DbInitializer>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

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
app.UseCookiePolicy();

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