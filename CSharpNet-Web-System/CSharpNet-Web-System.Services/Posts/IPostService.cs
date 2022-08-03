namespace CSharpNet_Web_System.Services.Posts
{
    using CSharpNet_Web_System.Services.Posts.Models;

    public interface IPostService
    {
        Task<ResultServiceModel> CreatePost(string title, string content);

        Task<ResultServiceModel> EditPost(int postId, string title, string content);

        Task<ResultServiceModel> DeletePost(int postId);

        Task<PostServiceModel> GetPost(int postId);

        Task<IEnumerable<PostServiceModel>> GetAllPosts();

        Task<IEnumerable<PostServiceModel>> GetPostsByCategoryId(int postCategoryId);
    }
}
