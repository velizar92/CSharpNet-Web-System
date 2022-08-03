using System.ComponentModel.DataAnnotations;

namespace CSharpNet_Web_System.Web.Models.Comment
{
    public class CommentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int TutorialId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string ProfileImageUrl { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
