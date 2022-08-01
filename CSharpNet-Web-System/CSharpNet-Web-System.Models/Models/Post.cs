namespace CSharpNet_Web_System.Models.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Resources = new HashSet<Resource>();
        }
        public int Id { get; set; }
        public int Title { get; set; }
        public int Content { get; set; }

        [ForeignKey(nameof(PostCategory))]
        public int PostCategoryId { get; set; }
        public PostCategory PostCategory { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Resource> Resources { get; set; }

    }
}
