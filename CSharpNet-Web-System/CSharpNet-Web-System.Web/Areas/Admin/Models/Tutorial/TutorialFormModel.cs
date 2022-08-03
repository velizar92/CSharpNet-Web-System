using CSharpNet_Web_System.Models.Models;
using System.ComponentModel.DataAnnotations;

using static CSharpNet_Web_System.Models.DataConstants.Tutorial;

namespace CSharpNet_Web_System.Web.Areas.Admin.Models.Tutorial
{
    public class TutorialFormModel
    {
        public int Id { get; set; }

        public int CourseId { get; set; }


        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(
            TutorialTitleMaxLength,
            MinimumLength = TutorialTitleMinLength,
            ErrorMessage = "The field Title must be a string with a minimum length of {2} and maximum length of {1}.")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(
            TutorialDescriptionMaxLength,
            MinimumLength = TutorialDescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2} and maximum length of {1}.")]
        public string Description { get; set; }

        public Resource[]? Resources { get; set; }

        public IEnumerable<IFormFile> Files { get; set; }
    }
}
