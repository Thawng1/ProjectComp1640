using ProjectComp1640.Dtos.Blog;
using ProjectComp1640.Dtos.Class;

namespace ProjectComp1640.Dtos
{
    public class SearchDto
    {
        public List<GetClassDto> Classes { get; set; } = new();
        public List<GetBlogDto> Blogs { get; set; } = new();
    }
}
