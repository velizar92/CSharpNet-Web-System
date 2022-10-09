namespace CSharpNet_Web_System.Services.Tutorials.Models
{
    using CSharpNet_Web_System.Models.Models;

    public class TutorialServiceModel
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string InternetUrl { get; set; }

        public IEnumerable<Resource> Resources { get; set; }
    }
}
