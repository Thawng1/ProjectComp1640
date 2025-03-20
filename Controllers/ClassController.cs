using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Dtos.Other;
using ProjectComp1640.Model;

namespace ProjectComp1640.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ClassController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpPost("create-class")]
        public async Task<ActionResult<ClassDto>> CreateClass(ClassDto classDto)
        {
            var tutorExists = await _context.Tutors.AnyAsync(t => t.Id == classDto.TutorId);
            if (!tutorExists)
            {
                return BadRequest(new { message = "Không tồn tại ID của giáo viên này" });
            }
            var existingStudentIds = await _context.Students
                .Where(s => classDto.StudentIds.Contains(s.Id))
                .Select(s => s.Id)
                .ToListAsync();
            var invalidStudentIds = classDto.StudentIds.Except(existingStudentIds).ToList();
            if (invalidStudentIds.Any())
            {
                return BadRequest(new { message = "Những ID của học sinh dưới đây không tồn tại", invalidIds = invalidStudentIds });
            }
            var newClass = new Class
            {
                TutorId = classDto.TutorId,
                ClassName = classDto.ClassName,
                Description = classDto.Description,
                ClassStudents = classDto.StudentIds.Select(id => new ClassStudent { StudentId = id }).ToList()
            };
            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClass), new { id = newClass.Id }, classDto);
        }

        [HttpGet("get-all-classes")]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            var classes = await _context.Classes
                .Include(c => c.Tutor)
                .ThenInclude(t => t.User)
                .Include(c => c.ClassStudents)
                .ThenInclude(cs => cs.Student)
                .ThenInclude(s => s.User)
                .ToListAsync();
            var classDTOs = classes.Select(c => new ClassDto
            {
                TutorId = c.TutorId,
                ClassName = c.ClassName,
                Description = c.Description,
                StudentIds = c.ClassStudents.Select(cs => cs.StudentId).ToList()
            }).ToList();
            return Ok(classDTOs);
        }
        [HttpGet("get-class/{id}")]
        public async Task<ActionResult<ClassDto>> GetClass(int id)
        {
            var cls = await _context.Classes
                .Include(c => c.Tutor)
                .ThenInclude(t => t.User)
                .Include(c => c.ClassStudents)
                .ThenInclude(cs => cs.Student)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cls == null)
            {
                return NotFound();
            }
            var classDto = new ClassDto
            {
                TutorId = cls.TutorId,
                ClassName = cls.ClassName,
                Description = cls.Description,
                StudentIds = cls.ClassStudents.Select(cs => cs.StudentId).ToList()
            };
            return Ok(classDto);
        }

        [HttpPut("update-class/{id}")]
        public async Task<IActionResult> UpdateClass(int id, ClassDto classDto)
        {
            var cls = await _context.Classes.FirstOrDefaultAsync(s => s.Id == id);
            var checkClass = await _context.Classes.Include(c => c.ClassStudents).FirstOrDefaultAsync(c => c.Id == id);
            if (checkClass == null)
            {
                return NotFound();
            }
            checkClass.TutorId = classDto.TutorId;
            checkClass.ClassName = classDto.ClassName;
            checkClass.Description = classDto.Description;
            checkClass.ClassStudents = classDto.StudentIds.Select(id => new ClassStudent { StudentId = id, ClassId = id }).ToList();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Classes.Any(c => c.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("delete-class/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var cls = await _context.Classes.FindAsync(id);
            if (cls == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(cls);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

