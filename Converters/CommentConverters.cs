using ProjectComp1640.Dtos.Comment;
using ProjectComp1640.Model;

namespace ProjectComp1640.Converters
{
    public static class CommentConverters
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                CommentId = commentModel.CommentId,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                Id = commentModel.Id
            };
        }
    }
}

