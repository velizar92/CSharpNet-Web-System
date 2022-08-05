namespace CSharpNet_Web_System.Web.Areas.Admin.Models.User
{
    using System.ComponentModel.DataAnnotations;

    using static CSharpNet_Web_System.Infrastructure.Constants.ValidationConstants;

    public class UserFormModel
    {

        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = MIN_MAX_STRING_VALIDATION, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Потвърди парола")]
        [Compare("Password", ErrorMessage = PASSWORDS_DONT_MATCH)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Профилна снимка")]
        public IFormFile ProfileImage { get; set; }
    }
}
