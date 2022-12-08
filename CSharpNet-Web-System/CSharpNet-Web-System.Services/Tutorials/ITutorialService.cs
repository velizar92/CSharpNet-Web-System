namespace CSharpNet_Web_System.Services.Tutorials
{    
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Tutorials.Models;

    public interface ITutorialService
    {
        Task<ResultServiceModel> AddTutorialToCourse(Guid courseId, string name, string description, string internetUrl, List<Resource> resources);
        Task<ResultServiceModel> EditTutorial(Guid tutorialId, string name, string description, string internetUrl, List<Resource> resources);
        Task<ResultServiceModel> DeleteTutorial(Guid tutorialId);
        Task<TutorialServiceModel> GetTutorialById(Guid tutorialId);
        Task<TutorialDetailsServiceModel> GetTutorialDetails(Guid tutorialId);
        Task<Guid?> GetTutorialIdByResourceId(Guid resourceId);
    }
}
