namespace CSharpNet_Web_System.Models.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.TutorialCategory;

    public class TutorialCategory
    {
        public TutorialCategory()
        {
            Tutorials = new HashSet<Tutorial>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TutorialCategoryNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Tutorial> Tutorials { get; set; }
    }
}
