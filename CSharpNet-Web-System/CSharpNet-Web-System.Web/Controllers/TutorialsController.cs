namespace CSharpNet_Web_System.Web.Controllers
{
    using CSharpNet_Web_System.Services.Tutorials;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TutorialsController : Controller
    {
        private readonly ITutorialService _tutorialService;       

        public TutorialsController(ITutorialService tutorialService)
        {
            _tutorialService = tutorialService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var tutorialDetails = await _tutorialService.GetTutorialDetails(id);

            return View(tutorialDetails);
        }

    }
}
