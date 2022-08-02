namespace CSharpNet_Web_System.Services.Courses
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using CSharpNet_Web_System.Data;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Courses.Models;
    using Microsoft.EntityFrameworkCore;

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


        public async Task<ResultServiceModel> DeleteCourse(int courseId)
        {
            Course? course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId);

            if (course != null)
            {
                _dbContext.Courses.Remove(course);
                await _dbContext.SaveChangesAsync();

                return new ResultServiceModel(true, "OK");
            }

            return new ResultServiceModel(false, "Invalid course id");
        }


        public async Task<ResultServiceModel> EditCourse(int courseId, string name, string description, string pictureFileName)
        {
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
    }
}
