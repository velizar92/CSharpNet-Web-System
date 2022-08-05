namespace CSharpNet_Web_System.Web.Models.Issue
{
    using System.ComponentModel.DataAnnotations;
    using static CSharpNet_Web_System.Models.DataConstants.Issue;
    using static CSharpNet_Web_System.Infrastructure.Constants.ValidationConstants;

    public class IssueFormModel
    {
        [Required]
        public int TutorialId { get; set; }

        [Required(ErrorMessage = FIELD_REQUIRED)]
        [StringLength(
            IssueTitleMaxLength,
            MinimumLength = IssueTitleMinLength,
            ErrorMessage = MIN_MAX_STRING_VALIDATION)]
        public string Title { get; set; }


        [Required(ErrorMessage = FIELD_REQUIRED)]
        [StringLength(
            IssueDescriptionMaxLength,
            MinimumLength = IssueDescriptionMinLength,
            ErrorMessage = MIN_MAX_STRING_VALIDATION)]
        
        public string Description { get; set; }
    }
}
