namespace CSharpNet_Web_System.Services.Comments.Models
{
    public class CommentDetailsServiceModel
    {
        public Guid Id { get; set; }

        public Guid TutorialId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
