namespace CSharpNet_Web_System.Web.Areas.Admin.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    using static CSharpNet_Web_System.Models.DataConstants.Course;
    using static CSharpNet_Web_System.Infrastructure.Constants.ValidationConstants;

    public class CourseFormModel
    {
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
