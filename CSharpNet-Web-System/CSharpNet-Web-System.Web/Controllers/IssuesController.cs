namespace CSharpNet_Web_System.Web.Controllers
{
    using CSharpNet_Web_System.Infrastructure.Extensions;
    using CSharpNet_Web_System.Services.Issues;
    using CSharpNet_Web_System.Web.Models.Issue;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    public class IssuesController : Controller
    {

        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }


        [HttpGet]
        [Authorize]
        public IActionResult CreateIssue()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIssue(IssueFormModel issueModel, int id)
        {
            string userId = User.Id();

            if (!ModelState.IsValid)
            {
                return View(issueModel);
            }

            var resultModelService = await _issueService.CreateIssue(userId, id, issueModel.Title, issueModel.Description);

            if (resultModelService.Result == false)
            {
                return BadRequest(resultModelService.Message);
            }

            return RedirectToAction(nameof(MyReportedIssues), new { id });
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditIssue(int id)
        {
            var issue = await _issueService.GetIssueDetails(id);

            IssueFormModel issueFormModel = new IssueFormModel
            {
                TutorialId = issue.TutorialId,
                Title = issue.Title,
                Description = issue.Description
            };

            return View(issueFormModel);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIssue(int id, IssueFormModel issueModel)
        {
            if (!ModelState.IsValid)
            {
                return View(issueModel);
            }

            var resultServiceModel = await _issueService.EditIssue(id, issueModel.Title, issueModel.Description);

            if (resultServiceModel.Result == false)
            {
                return BadRequest(resultServiceModel.Message);
            }

            return RedirectToAction(nameof(MyReportedIssues));
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyReportedIssues()
        {
            string userId = User.Id();
            var myIssues = await _issueService.GetMyReportedIssues(userId);

            return View(myIssues);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int issueId)
        {
            var issueDetails = await _issueService.GetIssueDetails(issueId);

            return View(issueDetails);
        }
    }
}
