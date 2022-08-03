namespace CSharpNet_Web_System.Web.Models.Issue
{
    using System.ComponentModel.DataAnnotations;
    using static CSharpNet_Web_System.Models.DataConstants.Issue;

    public class IssueFormModel
    {
        [Required]
        public int TutorialId { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(
            IssueTitleMaxLength,
            MinimumLength = IssueTitleMinLength,
            ErrorMessage = "The field Title must be a string with a minimum length of {2} and maximum length of {1}.")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(
            IssueDescriptionMaxLength,
            MinimumLength = IssueDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2} and maximum length of {1}.")]
        
        public string Description { get; set; }
    }
}
