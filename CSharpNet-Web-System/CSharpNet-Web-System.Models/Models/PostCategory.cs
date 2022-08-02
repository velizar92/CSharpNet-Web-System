namespace CSharpNet_Web_System.Models.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.PostCategory;

    public class PostCategory
    {
        public PostCategory()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PostCategoryNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}

