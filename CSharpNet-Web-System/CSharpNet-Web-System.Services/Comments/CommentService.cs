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


        public async Task<ResultServiceModel> CreateComment(int tutorialId, string userId, string content)
        {
            var turorial = await _dbContext.Tutorials
                            .Where(l => l.Id == tutorialId).FirstOrDefaultAsync();


            if (turorial == null)
            {
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

            return new ResultServiceModel(true, "OK");
        }


        public async Task<ResultServiceModel> DeleteComment(int commentId)
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


        public async Task<CommentServiceModel> GetCommentById(int commentId)
        {
            var comment = await _dbContext.Comments
                             .Where(c => c.Id == commentId)
                             .Select(c => new CommentServiceModel
                             {
                                 Content = c.Content
                             })
                             .FirstOrDefaultAsync();

            return comment;
        }


        public async Task<IEnumerable<CommentDetailsServiceModel>> GetTutorialComments(int tutorialId)
        {
            var tutorialComments = await _dbContext.Comments
                                 .Where(c => c.TutorialId == tutorialId)
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
