namespace CSharpNet_Web_System.Models.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Resource;

    public class Resource : BaseEntity
    {

        [Required]
        [MaxLength(ResourceNameMaxLength)]
        public string Name { get; set; }

        [ForeignKey(nameof(ResourceType))]
        public int ResourceTypeId { get; set; }
        public ResourceType ResourceType { get; set; }


        [ForeignKey(nameof(Tutorial))]
        public Guid TutorialId { get; set; }
        public Tutorial? Tutorial { get; set; }

    }
}
