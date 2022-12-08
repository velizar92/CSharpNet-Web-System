namespace CSharpNet_Web_System.Services.Tutorials.Models
{
    using System.Collections.Generic;
    public class TutorialDetailsServiceModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InternetUrl { get; set; }

        public IEnumerable<string> ResourceUrls { get; set; }
    }
}
