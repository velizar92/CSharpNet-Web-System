namespace CSharpNet_Web_System.Models.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Resource;

    public class Resource : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ResourceNameMaxLength)]
        public string Name { get; set; }

        [ForeignKey(nameof(ResourceType))]
        public int ResourceTypeId { get; set; }
        public ResourceType ResourceType { get; set; }


        [ForeignKey(nameof(Tutorial))]
        public int TutorialId { get; set; }
        public Tutorial Tutorial { get; set; }


        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
