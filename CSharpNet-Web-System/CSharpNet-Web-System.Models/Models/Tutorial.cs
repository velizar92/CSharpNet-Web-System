﻿namespace CSharpNet_Web_System.Models.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Tutorial;

    public class Tutorial : BaseEntity
    {
        // TODO: Check warning here
        // actions to be done with CSWS-100
        public Tutorial()
        {
            Issues = new HashSet<Issue>();
            Resources = new HashSet<Resource>();
            Comments = new HashSet<Comment>();
        }


        [Required]
        [MaxLength(TutorialTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(TutorialDescriptionMaxLength)]
        public string Description { get; set; }

        [Url]
        public string? InternetUrl { get; set; }

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }


        public ICollection<Issue> Issues { get; set; }
        public ICollection<Resource> Resources { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}