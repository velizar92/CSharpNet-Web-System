namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using CSharpNet_Web_System.Services.Issues;
    using Microsoft.AspNetCore.Mvc;

    public class IssuesController : AdminController
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }


        [HttpGet]
        public async Task<IActionResult> FixIssue(int issueId, int tutorialId)
        {
            var resultServiceModel = await _issueService.DeleteIssue(issueId);

            if (resultServiceModel.Result == false)
            {
                return BadRequest(resultServiceModel.Message);
            }

            return RedirectToAction(nameof(MyIssues), new { tutorialId });
        }


        [HttpGet]
        public async Task<IActionResult> MyIssues()
        {
            var allReportedIssues = await _issueService.GetAllReportedIssues();

            return View(allReportedIssues);
        }

    }
}
