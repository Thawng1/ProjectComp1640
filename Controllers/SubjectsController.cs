using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Model;
using ProjectComp1640.Data;
using System;
using ProjectComp1640.Dtos.Class;
using ProjectComp1640.Dtos.Subject;

namespace ProjectComp1640.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public SubjectsController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet("get-all-subjects")]
        public async Task<ActionResult<IEnumerable<GetSubjectDto>>> GetAllSubjects()
        {
            var subjects = await _context.Subjects
                .Include(s => s.Classes).ThenInclude(c => c.Tutor).ThenInclude(t => t.User)
                .Include(s => s.Classes).ThenInclude(c => c.ClassStudents).ThenInclude(cs => cs.Student).ThenInclude(s => s.User).
                ToListAsync();
            var subjectDtos = subjects.Select(s => new GetSubjectDto
            {
                SubjectName = s.SubjectName,
                Information = s.Information,
                Classes = s.Classes.Select(c => new CreateClassDto
                {
                    TutorName = c.Tutor.User.FullName ?? "Không có giáo viên",
                    SubjectName = c.Subject.SubjectName ?? "Không có môn học",
                    ClassName = c.ClassName,
                    TotalSlot = c.TotalSlot,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Description = c.Description,
                    StudentNames = c.ClassStudents.Where(cs => cs.Student?.User != null).Select(cs => cs.Student.User.FullName).ToList()
                }).ToList()
            }).ToList();
            return Ok(subjectDtos);
        }
        [HttpGet("get-subject/{id}")]
        public async Task<ActionResult<GetSubjectDto>> GetSubject(int id)
        {
            var subject = await _context.Subjects
                .Include(s => s.Classes).ThenInclude(c => c.Tutor).ThenInclude(t => t.User)
                .Include(s => s.Classes).ThenInclude(c => c.ClassStudents).ThenInclude(cs => cs.Student).ThenInclude(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null)
            {
                return NotFound($"Không tìm thấy môn học với ID '{id}'.");
            }
            var subjectDto = new GetSubjectDto
            {
                SubjectName = subject.SubjectName,
                Information = subject.Information,
                Classes = subject.Classes.Select(c => new CreateClassDto
                {
                    TutorName = c.Tutor.User.FullName ?? "Không có giáo viên",
                    SubjectName = c.Subject.SubjectName ?? "Không có môn học",
                    ClassName = c.ClassName,
                    TotalSlot = c.TotalSlot,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Description = c.Description,
                    StudentNames = c.ClassStudents.Where(cs => cs.Student?.User != null).Select(cs => cs.Student.User.FullName).ToList()
                }).ToList()
            };
            return Ok(subjectDto);
        }
        [HttpPost("create-subject")]
        public async Task<ActionResult<Subject>> CreateSubject(SubjectDto subjectDto)
        {
            var newSubject = new Subject
            {
                SubjectName = subjectDto.SubjectName,
                Information = subjectDto.Information,
            };
            _context.Subjects.Add(newSubject);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSubject), new { id = newSubject.Id }, new { message = "Tạo môn thành công.", subjectDto });
        }
        [HttpPut("update-subject/{id}")]
        public async Task<IActionResult> UpdateSubject(int id, SubjectDto subjectDto)
        {
            var sbj = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            sbj.SubjectName = subjectDto.SubjectName;
            sbj.Information = subjectDto.Information;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Subjects.Any(s => s.Id == id))
                {
                    return NotFound(new { message = "Không tìm thấy môn học này." });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { message = "Cập nhật môn thành công." });
        }
        [HttpDelete("delete-subject/{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound(new { message = "Không tìm thấy môn học này." });
            }
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Xóa môn thành công." });
        }
    }
}
