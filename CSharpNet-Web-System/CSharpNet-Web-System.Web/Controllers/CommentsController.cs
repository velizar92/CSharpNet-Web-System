namespace CSharpNet_Web_System.Web.Controllers
{
    using CSharpNet_Web_System.Services.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using CSharpNet_Web_System.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using CSharpNet_Web_System.Models.Models;
    using CSharpNet_Web_System.Web.Models.Comment;

    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManagerService;

        public CommentsController(ICommentService commentService, UserManager<User> userManagerService)
        {
            _commentService = commentService;
            _userManagerService = userManagerService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateComment(int tutorialId)
        {
            var user = await _userManagerService.GetUserAsync(HttpContext.User);

            return View(new CommentFormModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfileImageUrl = user.ProfileImageUrl,
                TutorialId = tutorialId
            });
           
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(CommentFormModel commentModel)
        {
            string userId = User.Id();
            var user = await _userManagerService.GetUserAsync(HttpContext.User);

            // TODO: Use constants for props. - CSWS-101
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("ProfileImageUrl");

            commentModel.ProfileImageUrl = user.ProfileImageUrl;
            commentModel.FirstName = user.FirstName;
            commentModel.LastName = user.LastName;

            if (!ModelState.IsValid)
            {
                return View(commentModel);
            }

            var resultServiceModel = await _commentService.CreateComment(commentModel.TutorialId, userId, commentModel.Content);

            if (resultServiceModel.Result == false)
            {
                return BadRequest(resultServiceModel.Message);
            }

            return RedirectToAction(nameof(AllComments), new { commentModel.TutorialId });
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int commentId, int lectureId)
        {
            var resultSeviceModel = await _commentService.DeleteComment(commentId);

            if (resultSeviceModel.Result == false)
            {
                return BadRequest(resultSeviceModel.Message);
            }

            return RedirectToAction(nameof(AllComments), new { lectureId });
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllComments(int tutorialId)
        {
            var allCommentsServiceModel = await _commentService.GetTutorialComments(tutorialId);
            List<CommentViewModel> comments = new List<CommentViewModel>();

            foreach (var commentModel in allCommentsServiceModel)
            {
                var user = await _userManagerService.FindByIdAsync(commentModel.UserId);

                CommentViewModel commentsViewModel = new CommentViewModel
                {
                    Id = commentModel.Id,
                    TutorialId = commentModel.TutorialId,
                    Content = commentModel.Content,
                    CreatedOn = commentModel.CreatedOn,
                    UpdatedOn = commentModel.UpdatedOn,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfileImageUrl = user.ProfileImageUrl,
                };

                comments.Add(commentsViewModel);
            }

            return View(comments);
        }

    }
}
