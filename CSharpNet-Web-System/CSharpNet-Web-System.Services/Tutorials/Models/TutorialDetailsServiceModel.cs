namespace CSharpNet_Web_System.Services.Tutorials.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class TutorialDetailsServiceModel
    {  public int Id { get; set; }

        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public IEnumerable<string> ResourceUrls { get; set; }
    }
}
