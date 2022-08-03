namespace CSharpNet_Web_System.Web.Areas.Admin.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    using static CSharpNet_Web_System.Models.DataConstants.Course;

    public class CourseFormModel
    {
        [Required(ErrorMessage = "Field {0} is required.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(
            CourseDescriptionMaxLength,
            MinimumLength = CourseDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2} and maximum length of {1}.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Field {0} is required.")]
        public IFormFile PictureFile { get; set; }
    }
}
