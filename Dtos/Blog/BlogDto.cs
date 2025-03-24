namespace ProjectComp1640.Dtos.Blog
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile File { get; set; }
    }
}
