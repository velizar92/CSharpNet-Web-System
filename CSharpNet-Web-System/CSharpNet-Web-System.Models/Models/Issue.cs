namespace CSharpNet_Web_System.Models.Models
{
    using CSharpNet_Web_System.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Issue;
    public class Issue : BaseEntity
    {

        [Required]
        [MaxLength(IssueTitleMaxLength)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(IssueDescriptionMaxLength)]
        public string? Description { get; set; }

        public string? UserId { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ResolvementDate { get; set; }

        public IssueStatuses Status { get; set; }


        [ForeignKey(nameof(Tutorial))]
        public Guid TutorialId { get; set; }
        public Tutorial? Tutorial { get; set; }
    }
}