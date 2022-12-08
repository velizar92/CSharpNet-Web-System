namespace CSharpNet_Web_System.Web.Areas.Admin.Models.Tutorial
{
    using CSharpNet_Web_System.Models.Models;
    using System.ComponentModel.DataAnnotations;

    using static CSharpNet_Web_System.Models.DataConstants.Tutorial;
    using static CSharpNet_Web_System.Infrastructure.Constants.ValidationConstants;
    public class TutorialFormModel
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }


        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Заглавие")]
        [StringLength(
            TutorialTitleMaxLength,
            MinimumLength = TutorialTitleMinLength,
            ErrorMessage = MIN_MAX_STRING_VALIDATION)]
        public string Title { get; set; }


        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Описание")]
        [StringLength(
            TutorialDescriptionMaxLength,
            MinimumLength = TutorialDescriptionMinLength,
            ErrorMessage = MIN_MAX_STRING_VALIDATION)]
        public string Description { get; set; }

        [Display(Name = "Интернет Адрес")]
        public string InternetUrl { get; set; }

        [Display(Name = "Ресурси")]
        public Resource[]? Resources { get; set; }

        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Ресурси")]
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
