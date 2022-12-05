namespace CSharpNet_Web_System.Models.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Tutorial;

    public class Tutorial : BaseEntity
    {
        public Tutorial()
        {
            // TODO: Probably it is not necessary to have the hashsets in the object itself, right?
            // We have foreigh key in Resources, comments and issues, so we can load them on demand from DB when needed (select * from Comments where "tutorialId" == "id")
            Issues = new HashSet<Issue>();
            Resources = new HashSet<Resource>();
            Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TutorialTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(TutorialDescriptionMaxLength)]
        public string Description { get; set; }

        [Url]
        public string? InternetUrl { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; }


        public ICollection<Issue> Issues { get; set; }
        public ICollection<Resource> Resources { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}