using CSharpNet_Web_System.Models.Enums;

namespace CSharpNet_Web_System.Services.Issues.Models
{
    public class IssueDetailsServiceModel
    {
        public int IssueId { get; set; }
        public int TutorialId { get; set; }
        public string TutorialTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }
        public IssueStatuses Status { get; set; }

    }
}
