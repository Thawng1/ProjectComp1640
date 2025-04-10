using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Model;
using ProjectComp1640.Data;
using System;
using ProjectComp1640.Dtos.Class;
using ProjectComp1640.Dtos.Subject;
using Microsoft.AspNetCore.Authorization;

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
                Id = s.Id,
                SubjectName = s.SubjectName,
                Information = s.Information,
                Classes = s.Classes.Select(c => new CreateClassDto
                {
                    TutorName = c.Tutor?.User?.FullName ?? "No Tutor",
                    SubjectName = c.Subject.SubjectName ?? "No Subject",
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
                return NotFound("Cannot find this subject.");
            }
            var subjectDto = new GetSubjectDto
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName,
                Information = subject.Information,
                Classes = subject.Classes.Select(c => new CreateClassDto
                {
                    TutorName = c.Tutor.User.FullName ?? "No Tutor",
                    SubjectName = c.Subject.SubjectName ?? "No Subject",
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
            return CreatedAtAction(nameof(GetSubject), new { id = newSubject.Id }, new { message = "Create subject successfully.", subjectDto });
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
                    return NotFound(new { message = "Cannot find this subject." });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { message = "Update subject successfully." });
        }
        [HttpDelete("delete-subject/{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound(new { message = "Cannot find this subject." });
            }
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Delete subject succesfully." });
        }
    }
}
