namespace CSharpNet_Web_System.Models.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PostCategory
    {
        public PostCategory()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}

