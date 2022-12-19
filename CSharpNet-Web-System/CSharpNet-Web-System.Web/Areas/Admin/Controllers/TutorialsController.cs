namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using CSharpNet_Web_System.Services.Storage;
    using CSharpNet_Web_System.Services.Tutorials;
    using CSharpNet_Web_System.Web.Areas.Admin.Models.Tutorial;
    using CSharpNet_Web_System.Models.Models;

    using static CSharpNet_Web_System.Infrastructure.Constants.InfrastructureConstants;

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
        public async Task<IActionResult> CreateTutorial(Guid courseId, TutorialFormModel tutorialFormModel)
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
                                                tutorialFormModel.InternetUrl,
                                                resources);

            if (resultServiceModel.Result == false)
            {
                return BadRequest(resultServiceModel.Message);
            }

            // TODO: Replace with localhost path (with upload). Also define potential debug and release configurations for this (localhost and release url) after testing. - CSWS-108
            await _fileStorageService.SaveFiles(@"\assets\resources", tutorialFormModel.Files);
            return RedirectToAction("Details", "Courses", new { courseId, Area = "" });
        }


        [HttpGet]
        public async Task<IActionResult> EditTutorial(Guid id)
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
                InternetUrl = tutorial.InternetUrl,
                Resources = tutorial.Resources.ToArray(),
            };

            return View(tutorialFormModel);
        }


        [HttpPost, DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTutorial(Guid id, TutorialFormModel tutorialFormModel)
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
                                                            tutorialFormModel.InternetUrl,
                                                            resources);

            if (resultServiceModel.Result == false)
            {
                return BadRequest(resultServiceModel.Message);
            }

            Guid courseId = tutorial.CourseId;

            await _fileStorageService.SaveFiles(@"\assets\resources", tutorialFormModel.Files);
            return RedirectToAction("Details", "Courses", new { courseId, Area = "" });
        }



        [HttpGet]
         public async Task<IActionResult> DeleteTutorial(Guid id, Guid courseId)
        {
            var resultServiceModel = await _tutorialService.DeleteTutorial(id);

            if (resultServiceModel.Result == true)
            {            
                return RedirectToAction("Details", "Courses", new { courseId, Area = "" });
            }

            return BadRequest();
        }



        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
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
                if (resourceFile.ContentType == PDF)
                {
                    tempResourceId = 3;
                }
                else if (resourceFile.ContentType == MP4)
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
