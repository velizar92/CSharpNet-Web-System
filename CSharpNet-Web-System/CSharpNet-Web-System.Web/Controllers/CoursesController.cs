namespace CSharpNet_Web_System.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using CSharpNet_Web_System.Services.Courses;

    public class CoursesController : Controller
    {

        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllCourses()
        {
            var allCourses = await _courseService.GetAllCourses();
            return View(allCourses);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int courseId)
        {
            var courseDetails = await _courseService.GetCourseDetails(courseId);

            // TODO: React on such places if having it as null (dialog) - CSWS-103

            return View(courseDetails);
        }
    }
}
