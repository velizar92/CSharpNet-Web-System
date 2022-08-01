namespace CSharpNet_Web_System.Models.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Comment : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }


        [ForeignKey(nameof(Tutorial))]
        public int TutorialId { get; set; }
        public Tutorial Tutorial { get; set; }


        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}