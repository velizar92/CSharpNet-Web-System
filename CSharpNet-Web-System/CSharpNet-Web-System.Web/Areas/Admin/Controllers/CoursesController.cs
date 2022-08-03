namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using CSharpNet_Web_System.Services.Courses;
    using CSharpNet_Web_System.Web.Areas.Admin.Models.Course;
    using CSharpNet_Web_System.Services.Storage;

    public class CoursesController : AdminController
    {
        private readonly ICourseService _courseService;
        private readonly IStorageService _fileStorageService;

        public CoursesController(ICourseService courseService, IStorageService fileStorageService)
        {
            _courseService = courseService;
            _fileStorageService = fileStorageService;
        }


        [HttpGet]    
        public IActionResult CreateCourse()
        {
            return View();
        }


        [HttpPost]      
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(CourseFormModel courseFormModel)
        {         
            if (!ModelState.IsValid)
            {              
                return View();
            }
        
            await _courseService.CreateCourse(courseFormModel.Name, courseFormModel.Description, courseFormModel.PictureFile.FileName);

            await _fileStorageService.SaveFile(@"\assets\images\courses", courseFormModel.PictureFile);

            return RedirectToAction(nameof(AllCourses));
        }


        [HttpGet]
        public async Task<IActionResult> EditCourse(int courseId)
        {

            var course = await _courseService.GetCourseDetails(courseId);

            if (course == null)
            {
                return NotFound();
            }

            CourseFormModel courseFormModel = new CourseFormModel
            {
                Name = course.Name,
                Description = course.Description,
            };

            return View(courseFormModel);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(int courseId, CourseFormModel courseFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _courseService.EditCourse(courseId, courseFormModel.Name, courseFormModel.Description, courseFormModel.PictureFile.FileName);

            await _fileStorageService.SaveFile(@"\assets\images\courses", courseFormModel.PictureFile);

            return RedirectToAction(nameof(AllCourses));
        }


        [HttpGet]       
        public async Task<IActionResult> DeleteCourse(int courseId)
        {        
            await _courseService.DeleteCourse(courseId);

            return RedirectToAction(nameof(AllCourses));
        }


        [HttpGet]       
        public async Task<IActionResult> AllCourses()
        {
            var allCourses = await _courseService.GetAllCourses();
            return View(allCourses);
        }
    }
}
