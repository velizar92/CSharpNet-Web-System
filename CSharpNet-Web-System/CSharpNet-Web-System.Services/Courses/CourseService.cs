namespace CSharpNet_Web_System.Services.Courses
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using CSharpNet_Web_System.Data;
    using Microsoft.EntityFrameworkCore;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Courses.Models;
    using CSharpNet_Web_System.Services.Tutorials.Models;

    public class CourseService : ICourseService
    {
        private readonly CSharpNetWebDbContext _dbContext;

        public CourseService(CSharpNetWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCourse(string name, string description, string pictureFileName)
        {
            await _dbContext.Courses.AddAsync(
                new Course
                {
                    Name = name,
                    Description = description,
                    ImageUrl = pictureFileName
                });

            await _dbContext.SaveChangesAsync();
        }


        public async Task<ResultServiceModel> DeleteCourse(Guid courseId)
        {
            Course? course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId);

            if (course != null)
            {
                _dbContext.Courses.Remove(course);
                await _dbContext.SaveChangesAsync();
                // TODO: Use resources (resx). - CSWS-103
                return new ResultServiceModel(true, "OK");
            }

            return new ResultServiceModel(false, "Invalid course id");
        }


        public async Task<ResultServiceModel> EditCourse(Guid courseId, string name, string description, string pictureFileName)
        {
            // TODO: will be nice if we find a way to pass only param we need to update. - CSWS-105
            var course = await _dbContext.Courses.FindAsync(courseId);

            if (course == null)
            {
                return new ResultServiceModel(false, "Invalid course id.");
            }

            course.Name = name;
            course.Description = description;
            course.ImageUrl = pictureFileName;

            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");
        }


        public async Task<IEnumerable<CourseServiceModel>> GetAllCourses()
        {
            var allCourses = await _dbContext.Courses
                 .Select(c => new CourseServiceModel
                 {
                     Id = c.Id,
                     Name = c.Name,
                     Description = c.Description,
                     ImageUrl = c.ImageUrl
                 })
                 .ToListAsync();

            return allCourses;
        }


        public async Task<CourseDetailsServiceModel> GetCourseDetails(Guid courseId)
        {
            var courseDetails =
                 await _dbContext.Courses
                           .Where(c => c.Id == courseId)
                           .Select(x => new CourseDetailsServiceModel
                           {
                               Id = x.Id,
                               Name = x.Name,
                               Description = x.Description,
                               ImageUrl = x.ImageUrl,
                               Tutorials = x.Tutorials.Select(t => new TutorialServiceModel
                               {
                                   Id = t.Id,
                                   Title = t.Title,
                                   CourseId = t.CourseId,
                                   Description = t.Description,
                                   Resources = t.Resources
                               })                               
                           })
                          .FirstOrDefaultAsync();

            return courseDetails;
        }
    }
}
