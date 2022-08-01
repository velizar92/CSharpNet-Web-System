namespace CSharpNet_Web_System.Models.Models
{   
    using System.Collections.Generic;
    public class Course
    {
        public Course()
        {        
            Tutorials = new HashSet<Tutorial>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public string ImageUrl { get; set; }
       
        public ICollection<Tutorial> Tutorials { get; set; }
    }
}
