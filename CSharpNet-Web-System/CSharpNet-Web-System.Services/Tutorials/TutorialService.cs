namespace CSharpNet_Web_System.Services.Tutorials
{  
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using CSharpNet_Web_System.Data;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Tutorials.Models;
    using Microsoft.EntityFrameworkCore;

    public class TutorialService : ITutorialService
    {
        private readonly CSharpNetWebDbContext _dbContext;

        public TutorialService(CSharpNetWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultServiceModel> AddTutorialToCourse(int courseId, string title, string description,
            string internetUrl, List<Resource> resources)
        {
            var tutorial = new Tutorial
            {
                Title = title,
                Description = description,
                InternetUrl = internetUrl,
                Resources = resources 
            };

            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
            {
                return new ResultServiceModel(false, "Invalid course id.");
            }

            course.Tutorials.Add(tutorial);
            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");
        }


        public async Task<ResultServiceModel> DeleteTutorial(int tutorialId)
        {
            var tutorial = await _dbContext.Tutorials.FindAsync(tutorialId);

            if (tutorial == null)
            {
                return new ResultServiceModel(false, "Invalid tutorial id.");
            }

            var resources = _dbContext.Resources.Where(r => r.TutorialId == tutorialId).ToList();
            tutorial.Resources = resources;

            foreach (var resource in tutorial.Resources)
            {
                _dbContext.Resources.Remove(resource);
            }

            _dbContext.Tutorials.Remove(tutorial);
            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");
        }


        public async Task<ResultServiceModel> EditTutorial(int tutorialId, string title, string description, 
            string internetUrl, List<Resource> resources)
        {
            // TODO: Edit only different props if possible.

            var tutorial = await _dbContext.Tutorials.FindAsync(tutorialId);
            if (tutorial == null)
            {
                return new ResultServiceModel(false, "Invalid tutorial id.");
            }

            tutorial.Title = title;
            tutorial.Description = description;
            tutorial.InternetUrl = internetUrl;

            foreach (var resourceItem in resources)
            {
                tutorial.Resources.Add(resourceItem);
            }

            await _dbContext.SaveChangesAsync();
            return new ResultServiceModel(true, "OK");
        }


        public async Task<TutorialServiceModel> GetTutorialById(int tutorialId)
        {

            // TODO: InternetUrl might be null
            var tutorial = await _dbContext.Tutorials
                               .Where(t => t.Id == tutorialId)
                               .Select(t => new TutorialServiceModel
                               {
                                   Id = t.Id,
                                   CourseId = t.CourseId,
                                   Title = t.Title,
                                   Description = t.Description,
                                   InternetUrl = t.InternetUrl,
                                   Resources = t.Resources.ToArray(), 
                               })
                               .FirstOrDefaultAsync();

            // TOOD: tutorial might be null

            return tutorial;
        }


        public async Task<TutorialDetailsServiceModel> GetTutorialDetails(int tutorialId)
        {
            // TODO: Same actions as for the method above.

            var tutorialDetails = await _dbContext.Tutorials
                     .Where(t => t.Id == tutorialId)
                     .Select(t => new TutorialDetailsServiceModel
                     {
                         Id = t.Id,
                         CourseId = t.CourseId,
                         Title = t.Title,
                         Description = t.Description,   
                         InternetUrl = t.InternetUrl,
                         ResourceUrls = t.Resources.Select(x => x.Name).ToArray()
                     })
                     .FirstOrDefaultAsync();

            return tutorialDetails;
        }


        public async Task<int?> GetTutorialIdByResourceId(int resourceId)
        {
            var resource = await _dbContext.Resources
                          .FirstOrDefaultAsync(r => r.Id == resourceId);

            if (resource != null)
            {
                return resource.TutorialId;
            }
                
            return -1;
        }
    }
}
