namespace CSharpNet_Web_System.Services.Courses.Models
{
    using CSharpNet_Web_System.Services.Tutorials.Models;

    public class CourseDetailsServiceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<TutorialServiceModel> Tutorials { get; set; }
    }
}
