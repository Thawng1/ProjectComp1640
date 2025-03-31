using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectComp1640.Converters;
using ProjectComp1640.Interfaces;
using ProjectComp1640.Model;

namespace ProjectComp1640.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IComment _IComment;
        // private readonly UserManager<AppUser> _userManager;
        public CommentController(IComment IComment)
        {
            _IComment = IComment;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll() 
        {
            var comment = await _IComment.GetAllAsync();

            var commentDto = comment.Select(s => s.ToCommentDto()); 
            return Ok(commentDto);
        }


    }
}
