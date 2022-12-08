namespace CSharpNet_Web_System.Models.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Comment;

    public class Comment : BaseEntity
    {       
        [Required]
        [MaxLength(CommentContentMaxLength)]
        public string Content { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(Tutorial))]
        public Guid TutorialId { get; set; }
        public Tutorial Tutorial { get; set; }

    }
}