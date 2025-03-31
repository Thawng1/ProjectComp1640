namespace ProjectComp1640.Model
{
    public class Blog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual AppUser User { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
