namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using CSharpNet_Web_System.Services.Posts;
    using CSharpNet_Web_System.Services.Storage;
    using CSharpNet_Web_System.Web.Areas.Admin.Models.Post;

    public class PostsController : AdminController
    {
        private readonly IPostService _postService;
        private readonly IStorageService _fileStorageService;

        public PostsController(IPostService postService, IStorageService fileStorageService)
        {
            _postService = postService;
            _fileStorageService = fileStorageService;
        }


        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(PostFormModel postFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _postService.CreatePost(postFormModel.Title, postFormModel.Content);

            return RedirectToAction(nameof(AllPosts));
        }


        [HttpGet]
        public async Task<IActionResult> EditPost(int postId)
        {
            var post = await _postService.GetPost(postId);

            if (post == null)
            {
                return NotFound();
            }

            PostFormModel postFormModel = new PostFormModel
            {
                Title = post.Title,
                Content = post.Content,
            };

            return View(postFormModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int postId, PostFormModel postFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _postService.EditPost(postId, postFormModel.Title, postFormModel.Content);

         
            return RedirectToAction(nameof(AllPosts));
        }


        [HttpGet]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var resutServiceModel = await _postService.DeletePost(postId);

            if (resutServiceModel.Result == false)
            {
                return BadRequest(resutServiceModel.Message);
            }

            return RedirectToAction(nameof(AllPosts));
        }


        [HttpGet]
        public async Task<IActionResult> AllPosts()
        {
            var allPosts = await _postService.GetAllPosts();
            return View(allPosts);
        }

    }
}
