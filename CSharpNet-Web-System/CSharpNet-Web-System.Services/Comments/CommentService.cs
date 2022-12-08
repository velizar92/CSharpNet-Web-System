namespace CSharpNet_Web_System.Services.Comments
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Data;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Comments.Models;
    using Microsoft.EntityFrameworkCore;

    public class CommentService : ICommentService
    {

        private readonly CSharpNetWebDbContext _dbContext;

        public CommentService(CSharpNetWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ResultServiceModel> CreateComment(Guid tutorialId, string userId, string content)
        {
            var turorial = await _dbContext.Tutorials
                            .Where(l => l.Id == tutorialId).FirstOrDefaultAsync();

            if (turorial == null)
            {
                // TODO: Use resoruces (resx). - CSWS-103
                return new ResultServiceModel(false, "Invalid tutorial id.");
            }

            var comment = new Comment()
            {
                TutorialId = tutorialId,
                UserId = userId,
                Content = content,
            };

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            // TODO: Use resoruces (resx). - CSWS-103
            return new ResultServiceModel(true, "OK");
        }


        public async Task<ResultServiceModel> DeleteComment(Guid commentId)
        {
            var comment = await _dbContext.Comments
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
            {
                return new ResultServiceModel(true, "Invalid comment id");
            }
         
            _dbContext.Comments.Remove(comment);

            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");

        }


        public async Task<CommentServiceModel> GetCommentById(Guid commentId)
        {
            var comment = await _dbContext.Comments
                             .Where(c => c.Id == commentId)
                             .Select(c => new CommentServiceModel
                             {
                                 Content = c.Content
                             })
                             .FirstOrDefaultAsync();

            // TODO check for null or mark method as it can return NULL. - CSWS-100

            return comment;
        }


        public async Task<IEnumerable<CommentDetailsServiceModel>> GetTutorialComments(Guid tutorialId)
        {
            var tutorialComments = await _dbContext.Comments
                                 .Where(c => c.TutorialId == tutorialId)
                                 .OrderByDescending(i => i.CreatedOn)
                                 .Select(c => new CommentDetailsServiceModel
                                 {
                                     Id = c.Id,
                                     TutorialId = tutorialId,
                                     Content = c.Content,
                                     UserId = c.UserId,
                                     CreatedOn = c.CreatedOn,
                                     UpdatedOn = c.UpdatedOn
                                 })
                                 .ToListAsync();

            return tutorialComments;
        }
    }
}
