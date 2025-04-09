using ProjectComp1640.Model;

namespace ProjectComp1640.Dtos.Blog
{
    public class GetBlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public IFormFile File { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
