namespace CSharpNet_Web_System.Services.Courses.Models
{
    using CSharpNet_Web_System.Models.Models;

    public class CourseDetailsServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<Tutorial> Tutorials { get; set; }
    }
}
