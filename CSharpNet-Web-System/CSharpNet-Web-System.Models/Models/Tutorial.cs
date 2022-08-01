namespace CSharpNet_Web_System.Models.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tutorial
    {
        public Tutorial()
        {
            Issues = new HashSet<Issue>();
            Resources = new HashSet<Resource>();
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [ForeignKey(nameof(TutorialCategory))]
        public int TutorialCategoryId { get; set; }
        public TutorialCategory TutorialCategory { get; set; }

        public ICollection<Issue> Issues { get; set; }
        public ICollection<Resource> Resources { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}