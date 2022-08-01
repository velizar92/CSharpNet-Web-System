namespace CSharpNet_Web_System.Models.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Issue : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string UserId { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ResolvingDate { get; set; }


        [ForeignKey(nameof(Tutorial))]
        public int TutorialId { get; set; }
        public Tutorial Tutorial { get; set; }
    }
}