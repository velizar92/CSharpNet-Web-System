namespace CSharpNet_Web_System.Services.Courses
{
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Services.Courses.Models;
    
    public interface ICourseService
    {
        Task CreateCourse(string name, string description, string pictureFileName);

        Task<ResultServiceModel> EditCourse(Guid courseId, string name, string description, string pictureFileName);

        Task<ResultServiceModel> DeleteCourse(Guid courseId);

        Task<IEnumerable<CourseServiceModel>> GetAllCourses();

        Task<CourseDetailsServiceModel> GetCourseDetails(Guid courseId);
    }
}
