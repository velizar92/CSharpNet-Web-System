namespace CSharpNet_Web_System.Services.Issues
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Data;
    using CSharpNet_Web_System.Models.Enums;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Issues.Models;
    using Microsoft.EntityFrameworkCore;

    public class IssueService : IIssueService
    {

        private readonly CSharpNetWebDbContext _dbContext;

        public IssueService(CSharpNetWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ResultServiceModel> CreateIssue(string userId, Guid tutorialId, string title, string description)
        {
            var tutorial = await _dbContext.Tutorials
                             .Where(t => t.Id == tutorialId)
                             .FirstOrDefaultAsync();

            if (tutorial == null)
            {
                return new ResultServiceModel(false, "Invalid tutorial id.");
            }

            var issue = new Issue()
            {
                Title = title,
                Description = description,
                TutorialId = tutorialId,
                UserId = userId,
                Status = IssueStatuses.Pending
            };

            tutorial.Issues.Add(issue);
            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");
        }


        public async Task<ResultServiceModel> DeleteIssue(Guid issueId)
        {
            var issue = await _dbContext.Issues.FirstOrDefaultAsync(i => i.Id == issueId);

            if (issue == null)
            {
                return new ResultServiceModel(false, "Invalid issue id.");
            }

            _dbContext.Issues.Remove(issue);
            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");
        }


        public async Task<ResultServiceModel> FixIssue(Guid issueId)
        {
            var issue = await _dbContext.Issues.FirstOrDefaultAsync(i => i.Id == issueId);

            if (issue == null)
            {
                return new ResultServiceModel(false, "Invalid issue id.");
            }

            issue.Status = IssueStatuses.Fixed;
            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");
        }


        public async Task<ResultServiceModel> EditIssue(Guid issueId, string title, string description)
        {
            var issue = await _dbContext.Issues.FirstOrDefaultAsync(i => i.Id == issueId);

            if (issue == null)
            {
                return new ResultServiceModel(false, "Invalid issue id.");
            }

            issue.Title = title;
            issue.Description = description;

            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");
        }


        public async Task<IEnumerable<IssueDetailsServiceModel>> GetAllReportedIssues()
        {
            var allReportedIssues = await _dbContext.Issues
                              .OrderByDescending(i => i.CreatedOn)
                              .Select(i => new IssueDetailsServiceModel
                              {
                                  TutorialId = i.TutorialId,
                                  TutorialTitle = i.Tutorial.Title,
                                  IssueId = i.Id,
                                  Title = i.Title,
                                  Description = i.Description,      
                                  Status = i.Status
                              })
                              .ToListAsync();

            return allReportedIssues;
        }


        public async Task<IEnumerable<IssueDetailsServiceModel>> GetAllReportedIssuesForTutorial(Guid tutorialId)
        {
            var allReportedIssuesForTutorial = await _dbContext.Issues
                                 .Where(i => i.TutorialId == tutorialId)
                                 .Select(i => new IssueDetailsServiceModel
                                 {
                                     TutorialId = i.TutorialId,
                                     TutorialTitle = i.Tutorial.Title,
                                     IssueId = i.Id,
                                     Title = i.Title,
                                     Description = i.Description,                                   
                                 })
                                 .ToListAsync();

            return allReportedIssuesForTutorial;
        }


        public async Task<IssueDetailsServiceModel> GetIssueDetails(Guid issueId)
        {
            var issue = await _dbContext.Issues.FirstOrDefaultAsync(i => i.Id == issueId);

            var issueDetails = _dbContext.Issues
                                        .Where(i => i.Id == issueId)
                                        .Select(i => new IssueDetailsServiceModel
                                        {
                                            TutorialId = issue.TutorialId,
                                            Title = issue.Title,
                                            Description = issue.Description,                                           
                                        })
                                        .FirstOrDefault();

            return issueDetails;
        }


        public async Task<IEnumerable<IssueDetailsServiceModel>> GetMyReportedIssues(string userId)
        {
            var myReportedIssues = await _dbContext
                                .Issues
                                .Where(i => i.UserId == userId)
                                .OrderByDescending(i => i.CreatedOn)
                                .Select(i => new IssueDetailsServiceModel
                                {
                                    TutorialId = i.TutorialId,
                                    TutorialTitle = i.Tutorial.Title,
                                    IssueId = i.Id,
                                    Title = i.Title,
                                    Description = i.Description,   
                                    CreatedOn = i.CreatedOn,
                                    Status = i.Status
                                })
                                .ToListAsync();

            return myReportedIssues;
        }
    }
}
