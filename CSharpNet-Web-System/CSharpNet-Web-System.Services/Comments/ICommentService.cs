namespace CSharpNet_Web_System.Services.Comments
{  
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Services.Comments.Models;

    public interface ICommentService
    {
        Task<ResultServiceModel> CreateComment(Guid tutorialId, string userId, string content);

        Task<ResultServiceModel> DeleteComment(Guid commentId);

        Task<CommentServiceModel> GetCommentById(Guid commentId);

        Task<IEnumerable<CommentDetailsServiceModel>> GetTutorialComments(Guid tutorialId);
    }
}
