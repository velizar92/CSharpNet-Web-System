namespace CSharpNet_Web_System.Services.Tutorials
{    
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Tutorials.Models;

    public interface ITutorialService
    {
        Task<ResultServiceModel> AddTutorialToCourse(int courseId, string name, string description, List<Resource> resources);
        Task<ResultServiceModel> EditTutorial(int tutorialId, string name, string description, List<Resource> resources);
        Task<ResultServiceModel> DeleteTutorial(int tutorialId);
        Task<TutorialServiceModel> GetTutorialById(int tutorialId);
        Task<TutorialDetailsServiceModel> GetTutorialDetails(int tutorialId);
        Task<int?> GetTutorialIdByResourceId(int resourceId);
    }
}
