namespace CSharpNet_Web_System.Services.Issues
{
    using CSharpNet_Web_System.Services.Issues.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IIssueService
    {
        Task<ResultServiceModel> CreateIssue(string userId, int tutorialId, string title, string description);

        Task<ResultServiceModel> EditIssue(int issueId, string title, string description);

        Task<ResultServiceModel> DeleteIssue(int issueId);

        Task<ResultServiceModel> FixIssue(int issueId);
        Task<IssueDetailsServiceModel> GetIssueDetails(int issueId);

        Task<IEnumerable<IssueDetailsServiceModel>> GetAllReportedIssues();

        Task<IEnumerable<IssueDetailsServiceModel>> GetAllReportedIssuesForTutorial(int tutorialId);

        Task<IEnumerable<IssueDetailsServiceModel>> GetMyReportedIssues(string userId);
    }
}
