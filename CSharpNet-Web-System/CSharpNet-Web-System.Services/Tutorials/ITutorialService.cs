namespace CSharpNet_Web_System.Services.Tutorials
{    
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Tutorials.Models;

    public interface ITutorialService
    {
        Task<ResultServiceModel> AddTutorialToCourse(int courseId, string name, string description, string content, List<Resource> resources);
        Task<ResultServiceModel> EditTutorial(int lectureId, string name, string description, string content, List<Resource> resources);
        Task<ResultServiceModel> DeleteTutorial(int lectureId);
        Task<TutorialServiceModel> GetTutorialById(int lectureId);
        Task<TutorialDetailsServiceModel> GetTutorialDetails(int lectureId);
        Task<int?> GetTutorialIdByResourceId(int resourceId);
    }
}
