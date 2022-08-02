namespace CSharpNet_Web_System.Services.Comments
{  
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Services.Comments.Models;

    public interface ICommentService
    {
        Task<ResultServiceModel> CreateComment(int lectureId, string userId, string content);

        Task<ResultServiceModel> DeleteComment(int commentId);

        Task<CommentServiceModel> GetCommentById(int commentId);

        Task<IEnumerable<CommentDetailsServiceModel>> GetTutorialComments(int lectureId);
    }
}
