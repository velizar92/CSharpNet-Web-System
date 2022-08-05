namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Storage;
    using CSharpNet_Web_System.Web.Areas.Admin.Models.User;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;

    using static CSharpNet_Web_System.Infrastructure.Constants.IdentityConstants;
    public class UsersController : AdminController
    {

        private readonly UserManager<User> _userManager;
        private readonly IStorageService _fileStorageService;

        public UsersController(        
           UserManager<User> userManager,
           IStorageService fileStorageService)
        {
            _userManager = userManager;
            _fileStorageService = fileStorageService;
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserFormModel userFormModel)
        {
            var user = GetUser(userFormModel.FirstName, userFormModel.LastName,
                                userFormModel.Email, userFormModel.ProfileImage.FileName);

            await _userManager.CreateAsync(user, userFormModel.Password);
            await _userManager.AddToRoleAsync(user, LearnerRole);
            await _userManager.AddClaimAsync(user, new Claim("ProfileImageUrl", user.ProfileImageUrl));

            await _fileStorageService.SaveFile(@"\assets\images\users", userFormModel.ProfileImage);

            return RedirectToAction(nameof(AllUsers));
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return View(new UserFormModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserFormModel userFormModel)
        {
            var user = GetUser(userFormModel.FirstName, userFormModel.LastName,
                                userFormModel.Email, userFormModel.ProfileImage.FileName);

            await _userManager.UpdateAsync(user);

            await _fileStorageService.SaveFile(@"\assets\images\users", userFormModel.ProfileImage);          
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(AllUsers));
        }


        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
          
            return View(users);
        }


        private User GetUser(string firstName, string lastName,
           string email, string profileImageUrl)
        {
            User user = new()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                Email = email,
                ProfileImageUrl = profileImageUrl,
            };

            return user;
        }
    }
}
