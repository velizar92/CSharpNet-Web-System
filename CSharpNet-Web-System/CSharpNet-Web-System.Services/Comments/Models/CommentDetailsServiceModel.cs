namespace CSharpNet_Web_System.Services.Comments.Models
{
    public class CommentDetailsServiceModel
    {
        public int Id { get; set; }

        public int TutorialId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
