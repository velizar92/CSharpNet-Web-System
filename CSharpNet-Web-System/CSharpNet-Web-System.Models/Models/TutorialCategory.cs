namespace CSharpNet_Web_System.Models.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class TutorialCategory
    {
        public TutorialCategory()
        {
            Tutorials = new HashSet<Tutorial>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Tutorial> Tutorials { get; set; }
    }
}
