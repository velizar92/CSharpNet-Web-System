namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using CSharpNet_Web_System.Services.Comments;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : AdminController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpGet]      
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var resultSeviceModel = await _commentService.DeleteComment(commentId);

            if (resultSeviceModel.Result == false)
            {
                return BadRequest(resultSeviceModel.Message);
            }

            return View();
        }

       
    }
}
