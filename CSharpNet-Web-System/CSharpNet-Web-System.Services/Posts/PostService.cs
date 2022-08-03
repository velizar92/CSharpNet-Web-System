namespace CSharpNet_Web_System.Services.Posts
{
    using CSharpNet_Web_System.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Services.Posts.Models;

    public class PostService : IPostService
    {
        private readonly CSharpNetWebDbContext _dbContext;

        public PostService(CSharpNetWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ResultServiceModel> CreatePost(string title, string content)
        {
            var post = new Post()
            {
                Title = title,
                Content = content,
            };

            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");      //TODO: Must be added try/catch block
        }


        public async Task<ResultServiceModel> DeletePost(int postId)
        {
            var postForDeletion = await _dbContext.Posts.FirstOrDefaultAsync(post => post.Id == postId);

            if (postForDeletion == null)
            {
                return new ResultServiceModel(false, "Invalid post id.");
            }

            _dbContext.Posts.Remove(postForDeletion);
            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");
        }


        public async Task<ResultServiceModel> EditPost(int postId, string title, string content)
        {
            var postForEditing = await _dbContext.Posts.FirstOrDefaultAsync(post => post.Id == postId);

            if (postForEditing == null)
            {
                return new ResultServiceModel(false, "Invalid post id.");
            }

            postForEditing.Title = title;
            postForEditing.Content = content;

            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");

        }

        public async Task<IEnumerable<PostServiceModel>> GetAllPosts()
        {
            var allPosts = await _dbContext.Posts
                                     .Select(p => new PostServiceModel
                                     {
                                         Title = p.Title,
                                         Content = p.Content,
                                     })
                                     .ToListAsync();

            return allPosts;
        }

        public async Task<PostServiceModel> GetPost(int postId)
        {
            var post = await _dbContext.Posts
                                     .Select(p => new PostServiceModel
                                     {
                                         Title = p.Title,
                                         Content = p.Content,
                                     })
                                     .FirstOrDefaultAsync();

            return post;
        }

        public async Task<IEnumerable<PostServiceModel>> GetPostsByCategoryId(int postCategoryId)
        {
            var posts = await _dbContext.Posts
                                     .Where(p => p.PostCategoryId == postCategoryId)
                                     .Select(p => new PostServiceModel
                                     {
                                         Title = p.Title,
                                         Content = p.Content,
                                     })
                                     .ToListAsync();

            return posts;
        }
    }
}
