namespace CSharpNet_Web_System.Web.Models.Comment
{
    using System.ComponentModel.DataAnnotations;

    using static CSharpNet_Web_System.Models.DataConstants.Comment;
    using static CSharpNet_Web_System.Models.DataConstants.User;
    using static CSharpNet_Web_System.Infrastructure.InfrastructureConstants;

    public class CommentFormModel
    {
        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Съдържание")]
        [StringLength(
           CommentContentMaxLength,
           MinimumLength = CommentContentMinLength,
           ErrorMessage = MIN_MAX_STRING_VALIDATION)]
        public string Content { get; set; }


        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Име")]
        [StringLength(
            FirstNameMaxLength,
            MinimumLength = FirstNameMinLength,
            ErrorMessage = MIN_MAX_STRING_VALIDATION)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Фамилия")]
        [StringLength(
           LastNameMaxLength,
           MinimumLength = LastNameMinLength,
           ErrorMessage = MIN_MAX_STRING_VALIDATION)]      
        public string LastName { get; set; }


        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Профилна снимка")]
        public string ProfileImageUrl { get; set; }


        [Required]
        public Guid TutorialId { get; set; }
    }
}
