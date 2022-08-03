namespace CSharpNet_Web_System.Web.Controllers
{
    using CSharpNet_Web_System.Services.Posts;
    using CSharpNet_Web_System.Services.Storage;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IStorageService _fileStorageService;

        public PostsController(IPostService postService, IStorageService fileStorageService)
        {
            _postService = postService;
            _fileStorageService = fileStorageService;
        }


        [HttpGet]
        public async Task<IActionResult> AllPosts(int postCategoryId)
        {
            var posts = await _postService.GetPostsByCategoryId(postCategoryId);

            return View(posts);
        }

    }
}
