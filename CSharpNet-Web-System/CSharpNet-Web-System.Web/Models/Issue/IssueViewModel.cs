using CSharpNet_Web_System.Models.Enums;

namespace CSharpNet_Web_System.Web.Models.Issue
{
    public class IssueViewModel
    {
        public int IssueId { get; set; }
        public int TutorialId { get; set; }
        public string TutorialTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserProfileImageUrl { get; set; }
        public DateTime? CreatedOn { get; set; }
        public IssueStatuses Status { get; set; }

    }

}
