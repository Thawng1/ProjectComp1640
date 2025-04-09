using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Dtos.Class;
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
        public async Task<ActionResult<ClassDto>> CreateClass(CreateClassDto createClassDto)
        {
            var tutorUser = await _context.Users.FirstOrDefaultAsync(t => t.FullName == createClassDto.TutorName);
            if (tutorUser == null)
            {
                return NotFound($"Cannot found tutor with name '{createClassDto.TutorName}'");
            }
            var tutor = await _context.Tutors.FirstOrDefaultAsync(t => t.UserId == tutorUser.Id);
            var subjectName = await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectName == createClassDto.SubjectName);
            if (subjectName == null)
            {
                return NotFound($"Cannot found subject with name '{createClassDto.SubjectName}'");
            }
            var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == subjectName.Id);
            if (createClassDto.EndDate < createClassDto.StartDate)
            {
                return BadRequest("End date cannot be earlier than start date.");
            }
            if(createClassDto.TotalSlot <= 0)
            {
                return BadRequest("Number of total slots must be greater than 0.");
            }
            var checkClassNameExists = await _context.Classes.AnyAsync(c => c.ClassName == createClassDto.ClassName);
            if (checkClassNameExists)
            {
                return BadRequest($"Class name '{createClassDto.ClassName}' is already taken.");
            }
            var classStudents = new List<ClassStudent>();
            foreach (var studentName in createClassDto.StudentNames)
            {
                var studentUser = await _context.Users.FirstOrDefaultAsync(u => u.FullName == studentName);
                if (studentUser == null) 
                { 
                    return NotFound($"Student with name '{studentName}' not found.");
                }
                var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == studentUser.Id);
                classStudents.Add(new ClassStudent
                {
                    StudentId = student.Id
                });
            }
            var newClass = new Class
            {
                TutorId = tutor.Id,
                SubjectId = subject.Id,
                ClassName = createClassDto.ClassName,
                TotalSlot = createClassDto.TotalSlot,
                StartDate = createClassDto.StartDate,
                EndDate = createClassDto.EndDate,
                Description = createClassDto.Description,
                ClassStudents = classStudents
            };
            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();
            await UpdateSubjectClassesAsync(newClass.SubjectId, newClass);
            return CreatedAtAction(nameof(GetClass), new { id = newClass.Id }, createClassDto);
        }

        [HttpGet("get-all-classes")]
        public async Task<ActionResult<IEnumerable<GetClassDto>>> GetAllClasses()
        {
            var classes = await _context.Classes
                .Include(c => c.Tutor).ThenInclude(t => t.User)
                .Include(c => c.Subject)
                .Include(c => c.ClassStudents).ThenInclude(cs => cs.Student).ThenInclude(s => s.User)
                .ToListAsync();
            var classDTOs = classes.Select(c => new GetClassDto
            {
                id = c.Id,
                TutorName = c.Tutor?.User?.FullName ?? "No Tutor",
                TutorId = c.Tutor?.Id,
                TutorUserId = c.Tutor?.UserId,
                SubjectName = c.Subject?.SubjectName ?? "No Subject",
                ClassName = c.ClassName,
                TotalSlot = c.TotalSlot,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Description = c.Description,
                StudentNames = c.ClassStudents.Where(cs => cs.Student?.User != null).Select(cs => cs.Student.User.FullName).DefaultIfEmpty("No Students").ToList(),
                StudentIds = c.ClassStudents.Where(cs => cs.Student?.User !=null).Select(cs => cs.Student.Id).ToList(),
                StudentUserIds = c.ClassStudents.Where(cs => cs.Student?.User !=null).Select(cs => cs.Student.UserId).ToList()
            }).ToList();
            return Ok(classDTOs);
        }
        [HttpGet("get-class/{id}")]
        public async Task<ActionResult<GetClassDto>> GetClass(int id)
        {
            var cls = await _context.Classes
                .Include(c => c.Subject)
                .Include(c => c.Tutor.User)
                .Include(c => c.ClassStudents).ThenInclude(cs => cs.Student.User)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cls == null)
            {
                return NotFound($"Cannot find class with ID: '{id}'.");
            }
            var classDto = new GetClassDto
            {
                id = cls.Id,
                TutorName = cls.Tutor?.User?.FullName ?? "No Tutor",
                TutorId = cls.Tutor?.Id,
                TutorUserId = cls.Tutor?.UserId,
                SubjectName = cls.Subject.SubjectName,
                ClassName = cls.ClassName,
                TotalSlot = cls.TotalSlot,
                StartDate = cls.StartDate,
                EndDate = cls.EndDate,
                Description = cls.Description,
                StudentNames = cls.ClassStudents.Select(cs => cs.Student.User.FullName).DefaultIfEmpty("No Students").ToList(),
                StudentIds = cls.ClassStudents.Where(cs => cs.Student?.User != null).Select(cs => cs.Student.Id).ToList(),
                StudentUserIds = cls.ClassStudents.Where(cs => cs.Student?.User != null).Select(cs => cs.Student.UserId).ToList()
            };
            return Ok(classDto);
        }
        [HttpPut("update-class/{id}")]
        public async Task<IActionResult> UpdateClass(int id, CreateClassDto createClassDto)
        {
            var cls = await _context.Classes.Include(c => c.ClassStudents).FirstOrDefaultAsync(c => c.Id == id);
            if (cls == null)
            {
                return NotFound($"Cannot find class with ID: '{id}'.");
            }
            var checkClassNameExists = await _context.Classes.AnyAsync(c => c.ClassName == createClassDto.ClassName && c.Id != id);
            if (checkClassNameExists)
            {
                return BadRequest($"Class name '{createClassDto.ClassName}' is already taken.");
            }
            cls.ClassName = createClassDto.ClassName;
            cls.TotalSlot = createClassDto.TotalSlot;
            cls.StartDate = createClassDto.StartDate;
            cls.EndDate = createClassDto.EndDate;
            cls.Description = createClassDto.Description;
            if (!string.IsNullOrEmpty(createClassDto.SubjectName))
            {
                var subjectName = await _context.Subjects.FirstOrDefaultAsync(sn => sn.SubjectName == createClassDto.SubjectName);
                if (subjectName == null)
                {
                    return NotFound($"Subject '{createClassDto.SubjectName}' not found.");
                }
                var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == subjectName.Id);
                cls.SubjectId = subject.Id;
            }
            if (!string.IsNullOrEmpty(createClassDto.TutorName))
            {
                var tutorUser = await _context.Users.FirstOrDefaultAsync(u => u.FullName == createClassDto.TutorName);
                if (tutorUser == null)
                { 
                    return NotFound($"Tutor with name '{createClassDto.TutorName}' not found."); 
                }
                var tutor = await _context.Tutors.FirstOrDefaultAsync(t => t.UserId == tutorUser.Id);
                cls.TutorId = tutor.Id;
            }
            if (createClassDto.EndDate < createClassDto.StartDate)
            {
                return BadRequest("End date cannot be earlier than start date.");
            }
            if (createClassDto.TotalSlot <= 0)
            {
                return BadRequest("Number of total slots must be greater than 0.");
            }
            var newClassStudents = new List<ClassStudent>();
            foreach (var studentName in createClassDto.StudentNames)
            {
                var studentUser = await _context.Users.FirstOrDefaultAsync(u => u.FullName == studentName);
                if (studentUser == null)
                {
                    return NotFound($"Student with name '{studentName}' not found.");
                }
                var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == studentUser.Id);
                newClassStudents.Add(new ClassStudent
                {
                    StudentId = student.Id
                });
            }
            _context.ClassStudents.RemoveRange(cls.ClassStudents);
            cls.ClassStudents = newClassStudents;
            try
            {
                _context.Entry(cls).State = EntityState.Modified;
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
                return NotFound(new { message = "Cannot find this class." });
            }
            _context.Classes.Remove(cls);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Delete class succesfully." });
        }
        private async Task UpdateSubjectClassesAsync(int? subjectId, Class newClass)
        {
            if (subjectId != null)
            {
                var subject = await _context.Subjects
                    .Include(s => s.Classes)
                    .FirstOrDefaultAsync(s => s.Id == subjectId);
                if (subject != null && !subject.Classes.Contains(newClass))
                {
                    subject.Classes.Add(newClass);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}

