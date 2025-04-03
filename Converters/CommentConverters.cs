using ProjectComp1640.Dtos.Comment;
using ProjectComp1640.Model;

namespace ProjectComp1640.Converters
{
    public static class CommentConverters
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                UserFullName = comment.User?.FullName ?? "Unknown User",
                UserId = comment.UserId
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDto dto, string userId)
        {
            return new Comment
            {
                Content = dto.Content,
                BlogId = dto.BlogId,
                UserId = userId,
                CreatedOn = DateTime.UtcNow

            };
        }
    }
}

    