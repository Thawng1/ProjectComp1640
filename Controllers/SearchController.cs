using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Dtos;
using ProjectComp1640.Dtos.Blog;
using ProjectComp1640.Dtos.Class;

namespace ProjectComp1640.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public SearchController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet("search")]
        public async Task<ActionResult<SearchDto>> Search([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Please enter the name of class or blog title to search.");
            }
            var lowerQuery = query.ToLower();
            var matchedClasses = await _context.Classes
                .Include(c => c.Subject)
                .Include(c => c.Tutor).ThenInclude(t => t.User)
                .Include(c => c.ClassStudents).ThenInclude(cs => cs.Student).ThenInclude(s => s.User)
                .Where(c => c.ClassName.ToLower().Contains(lowerQuery))
                .ToListAsync();

            var classDtos = matchedClasses.Select(c => new GetClassDto
            {
                id = c.Id,
                TutorName = c.Tutor?.User?.FullName ?? "No Tutor",
                TutorId = c.Tutor.Id,
                TutorUserId = c.Tutor.UserId,
                SubjectName = c.Subject.SubjectName,
                ClassName = c.ClassName,
                TotalSlot = c.TotalSlot,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Description = c.Description,
                StudentNames = c.ClassStudents.Select(cs => cs.Student.User.FullName).ToList(),
                StudentIds = c.ClassStudents.Where(cs => cs.Student?.User != null).Select(cs => cs.Student.Id).ToList(),
                StudentUserIds = c.ClassStudents.Where(cs => cs.Student?.User != null).Select(cs => cs.Student.UserId).ToList()
            }).ToList();

            var matchedBlogs = await _context.Blogs
                .Include(b => b.User)
                .Include(b => b.Comments)
                .Where(b =>
                    b.Title.ToLower().Contains(lowerQuery) ||
                    b.Content.ToLower().Contains(lowerQuery))
                .ToListAsync();

            var blogDtos = matchedBlogs.Select(b => new GetBlogDto
            {
                Id = b.Id,
                Title = b.Title,
                ShortContent = b.Content?.Length > 100 ? b.Content.Substring(0, 100) + "..." : b.Content,
                UserName = b.User.FullName,
                UserId = b.UserId,
            }).ToList();

            return Ok(new SearchDto
            {
                Classes = classDtos,
                Blogs = blogDtos
            });
        }
    }
}
