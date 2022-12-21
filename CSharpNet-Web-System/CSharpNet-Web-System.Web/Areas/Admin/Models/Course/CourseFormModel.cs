namespace CSharpNet_Web_System.Web.Areas.Admin.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    using static CSharpNet_Web_System.Models.DataConstants.Course;
    using static CSharpNet_Web_System.Infrastructure.InfrastructureConstants;

    public class CourseFormModel
    {
        // TODO: Generate resource files (resx) and use them for labels (not hardcoded in "Display" annotation), validation messages etc. Will be done when UI work started and JS framework is chosen.
        // Actions in ticket - CSWS-103
        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Име")]
        public string Name { get; set; }


        [Required(ErrorMessage = FIELD_REQUIRED)]
        [Display(Name = "Описание")]
        [StringLength(
            CourseDescriptionMaxLength,
            MinimumLength = CourseDescriptionMinLength,
            ErrorMessage = MIN_MAX_STRING_VALIDATION)]
        public string Description { get; set; }

        [Display(Name = "Снимка аватар")]
        [Required(ErrorMessage = FIELD_REQUIRED)]
        public IFormFile PictureFile { get; set; }
    }
}
