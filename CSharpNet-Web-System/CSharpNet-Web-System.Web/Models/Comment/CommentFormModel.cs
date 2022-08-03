
namespace CSharpNet_Web_System.Web.Models.Comment
{
    using System.ComponentModel.DataAnnotations;

    using static CSharpNet_Web_System.Models.DataConstants.Comment;
    using static CSharpNet_Web_System.Models.DataConstants.User;

    public class CommentFormModel
    {
        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(
           CommentContentMaxLength,
           MinimumLength = CommentContentMinLength,
           ErrorMessage = "The field Content must be a string with a minimum length of {2} and maximum length of {1}.")]
        public string Content { get; set; }


        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(
            FirstNameMaxLength,
            MinimumLength = FirstNameMinLength,
            ErrorMessage = "The field FirstName must be a string with a minimum length of {2} and maximum length of {1}.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(
           LastNameMaxLength,
           MinimumLength = LastNameMinLength,
           ErrorMessage = "The field LastName must be a string with a minimum length of {2} and maximum length of {1}.")]      
        public string LastName { get; set; }


        [Required(ErrorMessage = "Field {0} is required.")]
        public string ProfileImageUrl { get; set; }


        [Required]
        public int TutorialId { get; set; }
    }
}
