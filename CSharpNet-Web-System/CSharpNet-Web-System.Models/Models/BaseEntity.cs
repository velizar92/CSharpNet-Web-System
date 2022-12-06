namespace CSharpNet_Web_System.Models.Models
{
    public class BaseEntity
    {
        // TODO: Base entity should also have the unique identifier prop.
        // Actions to be done related to this task - CSWS-104
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
