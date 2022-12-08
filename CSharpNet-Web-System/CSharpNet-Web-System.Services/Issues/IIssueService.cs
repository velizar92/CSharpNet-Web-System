namespace CSharpNet_Web_System.Services.Issues
{
    using CSharpNet_Web_System.Services.Issues.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IIssueService
    {
        Task<ResultServiceModel> CreateIssue(string userId, Guid tutorialId, string title, string description);

        Task<ResultServiceModel> EditIssue(Guid issueId, string title, string description);

        Task<ResultServiceModel> DeleteIssue(Guid issueId);

        Task<ResultServiceModel> FixIssue(Guid issueId);
        Task<IssueDetailsServiceModel> GetIssueDetails(Guid issueId);

        Task<IEnumerable<IssueDetailsServiceModel>> GetAllReportedIssues();

        Task<IEnumerable<IssueDetailsServiceModel>> GetAllReportedIssuesForTutorial(Guid tutorialId);

        Task<IEnumerable<IssueDetailsServiceModel>> GetMyReportedIssues(string userId);
    }
}
