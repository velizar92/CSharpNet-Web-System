namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using CSharpNet_Web_System.Services.Storage;
    using CSharpNet_Web_System.Services.Tutorials;
    using CSharpNet_Web_System.Web.Areas.Admin.Models.Tutorial;
    using CSharpNet_Web_System.Models.Models;

    public class TutorialsController : AdminController
    {
        private readonly ITutorialService _tutorialService;
        private readonly IStorageService _fileStorageService;

        public TutorialsController(ITutorialService tutorialService, IStorageService fileStorageService)
        {
            _tutorialService = tutorialService;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public IActionResult CreateTutorial()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTutorial(int courseId, TutorialFormModel tutorialFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var resources = GetResources(tutorialFormModel.Files);

            var resultServiceModel = await _tutorialService.AddTutorialToCourse
                                                (courseId, 
                                                tutorialFormModel.Title,
                                                tutorialFormModel.Description,
                                                tutorialFormModel.Content,
                                                resources);

            if (resultServiceModel.Result == false)
            {
                return BadRequest(resultServiceModel.Message);
            }

            await _fileStorageService.SaveFiles(@"\assets\resources", tutorialFormModel.Files);
            return RedirectToAction("Details", "Courses", new { courseId });
        }


        [HttpGet]
        public async Task<IActionResult> EditTutorial(int id)
        {
            var tutorial = await _tutorialService.GetTutorialById(id);

            if (tutorial == null)
            {
                return NotFound();
            }

            TutorialFormModel tutorialFormModel = new TutorialFormModel()
            {
                Id = tutorial.Id,
                CourseId = tutorial.CourseId,
                Title = tutorial.Title,
                Description = tutorial.Description,
                Content = tutorial.Content,
                Resources = tutorial.Resources.ToArray(),
            };

            return View(tutorialFormModel);
        }


        [HttpPost, DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTutorial(int id, TutorialFormModel tutorialFormModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(tutorialFormModel);
            }

            var tutorial = await _tutorialService.GetTutorialById(id);

            if (tutorial == null)
            {
                return NotFound();
            }

            var resources = GetResources(tutorialFormModel.Files);
            var resultServiceModel = await _tutorialService.EditTutorial(id,
                                                            tutorialFormModel.Title,
                                                            tutorialFormModel.Description,
                                                            tutorialFormModel.Content,
                                                            resources);

            if (resultServiceModel.Result == false)
            {
                return BadRequest(resultServiceModel.Message);
            }

            await _fileStorageService.SaveFiles(@"\assets\resources", tutorialFormModel.Files);
            return RedirectToAction(nameof(Details), new { id });
        }



        [HttpGet]
         public async Task<IActionResult> DeleteTutorial(int id, int courseId)
        {
            var resultServiceModel = await _tutorialService.DeleteTutorial(id);

            if (resultServiceModel.Result == true)
            {            
                return RedirectToAction("Details", "Courses", new { courseId });
            }

            return BadRequest();
        }



        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {         
            var tutorialDetails = await _tutorialService.GetTutorialDetails(id);
       
             return View(tutorialDetails);                 
        }




        private List<Resource> GetResources(IEnumerable<IFormFile> resourceFiles)
        {
            List<Resource> resources = new List<Resource>();
            int tempResourceId = 1;

            foreach (var resourceFile in resourceFiles)
            {
                if (resourceFile.ContentType == "pdf")
                {
                    tempResourceId = 3;
                }
                else if (resourceFile.ContentType == "mp4")
                {
                    tempResourceId = 2;
                }
                else
                {
                    tempResourceId = 1;
                }

                resources.Add(new Resource { Name = resourceFile.FileName, ResourceTypeId = tempResourceId });
            }

            return resources;
        }
    }
}
