namespace CSharpNet_Web_System.Web.Areas.Admin.Models
{
    using CSharpNet_Web_System.Models.Enums;
    public class IssueAdminViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }      
        public DateTime? CreatedOn { get; set; }
        public IssueStatuses Status { get; set; }
    }
}
