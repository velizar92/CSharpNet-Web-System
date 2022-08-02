namespace CSharpNet_Web_System.Services.Courses
{
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Services.Courses.Models;
    
    public interface ICourseService
    {
        Task CreateCourse(string name, string description, string pictureFileName);

        Task<ResultServiceModel> EditCourse(int courseId, string name, string description, string pictureFileName);

        Task<ResultServiceModel> DeleteCourse(int courseId);

        Task<IEnumerable<CourseServiceModel>> GetAllCourses();
    }
}
