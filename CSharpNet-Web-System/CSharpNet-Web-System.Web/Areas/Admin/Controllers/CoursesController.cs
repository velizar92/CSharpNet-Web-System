namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using CSharpNet_Web_System.Services.Courses;
    using CSharpNet_Web_System.Web.Areas.Admin.Models.Course;

    public class CoursesController : AdminController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        [Authorize]
        public IActionResult CreateCourse()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(CourseFormModel courseModel)
        {         
            if (!ModelState.IsValid)
            {              
                return View();
            }
        
            await _courseService.CreateCourse(courseModel.Name, courseModel.Description, courseModel.PictureFile.FileName);

            //await this.storageService.SaveFile(@"\assets\img\courses", courseModel.PictureFile);

            return RedirectToAction(nameof(AllCourses));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(int courseId, CourseFormModel courseModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _courseService.EditCourse(courseId, courseModel.Name, courseModel.Description, courseModel.PictureFile.FileName);

            //await this.storageService.SaveFile(@"\assets\img\courses", courseModel.PictureFile);

            return RedirectToAction(nameof(AllCourses));
        }


        [HttpGet]
        [Authorize]
        public IActionResult AllCourses()
        {
            return View();
        }
    }
}
