using System.ComponentModel.DataAnnotations;

namespace ProjectComp1640.Dtos.Comment
{
    public class CreateCommentDto
    {
        
        public string Content { get; set; } = string.Empty;
        public int BlogId { get; set; }
    }
}
