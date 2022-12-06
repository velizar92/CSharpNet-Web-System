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

            // TODO: Maybe just to show dialog with error in such cases would be enough --> "Operation was not successful. Reason/Please, try again later." - CSWS-102
            if (resultSeviceModel.Result == false)
            {
                return BadRequest(resultSeviceModel.Message);
            }

            return View();
        }

       
    }
}
