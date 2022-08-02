namespace CSharpNet_Web_System.Models.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Post;

    public class Post : BaseEntity
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Resources = new HashSet<Resource>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PostTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey(nameof(PostCategory))]
        public int PostCategoryId { get; set; }
        public PostCategory PostCategory { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Resource> Resources { get; set; }

    }
}
