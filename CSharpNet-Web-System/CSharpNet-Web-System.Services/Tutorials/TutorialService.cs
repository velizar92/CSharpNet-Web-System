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

        public async Task<ResultServiceModel> AddTutorialToCourse(int courseId, string title, string description, List<Resource> resources)
        {
            var tutorial = new Tutorial
            {
                Title = title,
                Description = description,
                Resources = resources,
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


        public async Task<ResultServiceModel> EditTutorial(int tutorialId, string title, string description, List<Resource> resources)
        {
            var tutorial = await _dbContext.Tutorials.FindAsync(tutorialId);
            if (tutorial == null)
            {
                return new ResultServiceModel(false, "Invalid tutorial id.");
            }

            tutorial.Title = title;
            tutorial.Description = description;

            foreach (var resourceItem in resources)
            {
                tutorial.Resources.Add(resourceItem);
            }

            await _dbContext.SaveChangesAsync();
            return new ResultServiceModel(true, "OK");
        }


        public async Task<TutorialServiceModel> GetTutorialById(int tutorialId)
        {
            var tutorial = await _dbContext.Tutorials
                               .Where(l => l.Id == tutorialId)
                               .Select(l => new TutorialServiceModel
                               {
                                   Id = l.Id,
                                   CourseId = l.CourseId,
                                   Title = l.Title,
                                   Description = l.Description,
                                   Resources = l.Resources.ToArray(),
                               })
                               .FirstOrDefaultAsync();

            return tutorial;
        }


        public async Task<TutorialDetailsServiceModel> GetTutorialDetails(int tutorialId)
        {
            var tutorialDetails = await _dbContext.Tutorials
                     .Where(l => l.Id == tutorialId)
                     .Select(l => new TutorialDetailsServiceModel
                     {
                         Id = l.Id,
                         CourseId = l.CourseId,
                         Title = l.Title,
                         Description = l.Description,
                         ResourceUrls = l.Resources.Select(x => x.Name).ToArray()
                     })
                     .FirstOrDefaultAsync();

            return tutorialDetails;
        }

        public async Task<int> GetTutorialIdByResourceId(int resourceId)
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
