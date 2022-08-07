namespace CSharpNet_Web_System.Web.Controllers
{
    using CSharpNet_Web_System.Services.Courses;
    using CSharpNet_Web_System.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {

        private readonly ICourseService _courseService;

        public HomeController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var allCourses = await _courseService.GetAllCourses();

            return View(allCourses);
        }



        public IActionResult About()
        {
            return View();
        }


        public IActionResult Contact()
        {
            return View();
        }


        public IActionResult Details()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}