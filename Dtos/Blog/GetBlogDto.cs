using ProjectComp1640.Model;

namespace ProjectComp1640.Dtos.Blog
{
    public class GetBlogDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Url { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual AppUser User { get; set; }

        public List<int> CommentIds { get; set; }
    }
}
