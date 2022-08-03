namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using CSharpNet_Web_System.Services.Resources;
    using Microsoft.AspNetCore.Mvc;

    public class ResourcesController : AdminController
    {
        private readonly IResourceService _resourceService;
       

        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }


        [HttpGet]
        public async Task<IActionResult> DeleteResource(int resourceId, int tutorialId)
        {    
            var resultServiceModel = await _resourceService.DeleteResource(resourceId);

            if (resultServiceModel.Result == false)
            {
                return BadRequest(resultServiceModel.Message);
            }

            return RedirectToAction("Details", "Tutorials", new { tutorialId });
        }
    }
}
