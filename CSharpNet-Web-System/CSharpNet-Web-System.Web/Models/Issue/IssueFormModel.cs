namespace CSharpNet_Web_System.Web.Models.Issue
{
    using System.ComponentModel.DataAnnotations;
    using static CSharpNet_Web_System.Models.DataConstants.Issue;
    using static CSharpNet_Web_System.Infrastructure.Constants.InfrastructureConstants;

    public class IssueFormModel
    {
        [Required]
        public Guid TutorialId { get; set; }

        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Заглавие")]
        [StringLength(
            IssueTitleMaxLength,
            MinimumLength = IssueTitleMinLength,
            ErrorMessage = MIN_MAX_STRING_VALIDATION)]
        public string Title { get; set; }


        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Описание")]
        [StringLength(
            IssueDescriptionMaxLength,
            MinimumLength = IssueDescriptionMinLength,
            ErrorMessage = MIN_MAX_STRING_VALIDATION)]
        
        public string Description { get; set; }

    }
}
