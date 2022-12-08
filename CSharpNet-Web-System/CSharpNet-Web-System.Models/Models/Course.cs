namespace CSharpNet_Web_System.Models.Models
{   
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Course;

    public class Course : BaseEntity
    {
        public Course()
        {        
            Tutorials = new HashSet<Tutorial>();
        }

        [Required]
        [MaxLength(CourseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CourseDescriptionMaxLength)]
        public string Description { get; set; }       
        public string ImageUrl { get; set; }
       
        public ICollection<Tutorial> Tutorials { get; set; }
    }
}
