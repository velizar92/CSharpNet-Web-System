namespace CSharpNet_Web_System.Models.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.ResourceType;

    public class ResourceType
    {
        // TODO: Check warning here
        // actions to be done with CSWS-100
        public ResourceType()
        {
            Resources = new HashSet<Resource>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ResourceTypeNameMaxLength)]
        public string Name { get; set; }
        public ICollection<Resource> Resources { get; set; }
    }
}
