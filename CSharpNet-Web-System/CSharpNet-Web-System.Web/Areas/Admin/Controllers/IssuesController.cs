namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using CSharpNet_Web_System.Services.Issues;
    using CSharpNet_Web_System.Web.Areas.Admin.Models;
    using Microsoft.AspNetCore.Mvc;

    public class IssuesController : AdminController
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }


        [HttpGet]
        public async Task<IActionResult> FixIssue(int issueId)
        {
            var resultServiceModel = await _issueService.FixIssue(issueId);

            if (resultServiceModel.Result == false)
            {
                return BadRequest(resultServiceModel.Message);
            }

            return RedirectToAction(nameof(MyIssues));
        }


        [HttpGet]
        public async Task<IActionResult> MyIssues()
        {
            var allReportedIssuesServiceModel = await _issueService.GetAllReportedIssues();
            List<IssueAdminViewModel> allIssuesAdminViewModel = new List<IssueAdminViewModel>();


            // TODO: do you have addAll method for collections? - CSWS-100
            foreach (var issueServiceModel in allReportedIssuesServiceModel)
            {
                allIssuesAdminViewModel.Add(
                    new IssueAdminViewModel
                    {
                        Id = issueServiceModel.IssueId,
                        Title = issueServiceModel.Title,
                        Description = issueServiceModel.Description,
                        CreatedOn = issueServiceModel.CreatedOn,
                        Status = issueServiceModel.Status
                    }
                );
            }

            return View(allIssuesAdminViewModel);
        }

    }
}
