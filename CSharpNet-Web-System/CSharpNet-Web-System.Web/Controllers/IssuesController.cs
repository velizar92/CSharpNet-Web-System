namespace CSharpNet_Web_System.Web.Controllers
{
    using CSharpNet_Web_System.Infrastructure.Extensions;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Issues;
    using CSharpNet_Web_System.Web.Models.Issue;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class IssuesController : Controller
    {

        private readonly IIssueService _issueService;
        private readonly UserManager<User> _userManagerService;

        public IssuesController(IIssueService issueService, UserManager<User> userManagerService)
        {
            _issueService = issueService;
            _userManagerService = userManagerService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateIssue(int tutorialId)
        {
            var user = await _userManagerService.GetUserAsync(HttpContext.User);

            return View(new IssueFormModel
            {
                TutorialId = tutorialId,
            });
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIssue(IssueFormModel issueModel)
        {
            string userId = User.Id();

            if (!ModelState.IsValid)
            {
                return View(issueModel);
            }

            var resultModelService = await _issueService.CreateIssue(userId, issueModel.TutorialId, issueModel.Title, issueModel.Description);

            if (resultModelService.Result == false)
            {
                return BadRequest(resultModelService.Message);
            }

            return RedirectToAction(nameof(MyReportedIssues));
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyReportedIssues()
        {
            string userId = User.Id();
            var myIssuesServiceModel = await _issueService.GetMyReportedIssues(userId);
            List<IssueViewModel> issueViewModels = new List<IssueViewModel>();

            var user = await _userManagerService.GetUserAsync(HttpContext.User);

            foreach (var issueServiceModel in myIssuesServiceModel)
            {
                issueViewModels.Add(
                    new IssueViewModel
                    {
                        IssueId = issueServiceModel.IssueId,
                        TutorialId = issueServiceModel.TutorialId,
                        TutorialTitle = issueServiceModel.TutorialTitle,
                        Title = issueServiceModel.Title,
                        Description = issueServiceModel.Description,
                        UserProfileImageUrl = user.ProfileImageUrl,
                        CreatedOn = issueServiceModel.CreatedOn,
                        Status = issueServiceModel.Status
                    }
                 );
            }

            return View(issueViewModels);
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
