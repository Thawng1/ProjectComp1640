using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectComp1640.Converters;
using ProjectComp1640.Dtos.Comment;
using ProjectComp1640.Interfaces;
using ProjectComp1640.Migrations;
using ProjectComp1640.Model;
using ProjectComp1640.NotificationConnect;
using System.Security.Claims;

namespace ProjectComp1640.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IComment _IComment;
        private readonly NotificationService _notificationService;

        public CommentController(IComment IComment, NotificationService notificationService)
        {
            _IComment = IComment;
            _notificationService = notificationService;
        }
        // -------------------------- Lấy tất cả bình luận --------------------------
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _IComment.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }
        // -------------------------- Lấy bình luận theo ID --------------------------
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _IComment.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }
        // -------------------------- Lấy bình luận theo BlogId --------------------------
        [HttpGet("blog/{blogId}")]
        [Authorize]
        public async Task<IActionResult> GetByBlogId(int blogId)
        {
            var comments = await _IComment.GetByBlogIdAsync(blogId);
            if (comments == null || !comments.Any())
            {
                return NotFound("No comments found for this blog.");
            }

            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }
        // -------------------------- Tạo bình luận --------------------------
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized("User not authenticated.");

            var comment = dto.ToCommentFromCreate(userId);
            await _IComment.CreateAsync(comment);

            // ✅ Gửi thông báo cho chủ blog (nếu không phải người comment)
            var blog = await _IComment.GetBlogByIdAsync(dto.BlogId); // bạn cần tạo hàm này trong IComment
            if (blog != null && blog.UserId != userId)
            {
                await _notificationService.SendNotification(
                    receiverId: blog.UserId,
                    senderId: userId,
                    message: "Bài viết của bạn vừa nhận một bình luận mới.",
                    actionUrl: $"/blogs/{dto.BlogId}"
                );
            }


            return Ok(new { message = "Comment created successfully." });
        }

        // -------------------------- Xóa bình luận --------------------------
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await _IComment.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound(new { message = "Comment not found!" });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (comment.UserId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            await _IComment.DeleteAsync(comment);
            return Ok(new { message = "Comment deleted successfully." });
        }
        // -------------------------- Cập nhật bình luận --------------------------
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _IComment.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound(new { message = "Comment not found!" });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (comment.UserId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            // Cập nhật nội dung bình luận
            comment.Content = dto.Content;

            var updatedComment = await _IComment.UpdateAsync(id, comment);
            return Ok(updatedComment.ToCommentDto());
        }




    }
}
    