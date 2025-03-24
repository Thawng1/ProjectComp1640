﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Dtos.Account;
using System.Security.Claims;

namespace ProjectComp1640.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileStudentsandTutorController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ProfileStudentsandTutorController(ApplicationDBContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("students")]
        public async Task<IActionResult> GetStudents()
        {
            if(_context.Students.Any())
            {
                var students = await _context.Students
                .Include(s => s.User)
                .Select(s => new {
                    s.Id,
                    s.StudentCode,
                    s.Course,
                    s.Status,
                    User = s.User != null ? new { s.User.FullName, s.User.UserName, s.User.Email } : null
                })
                .ToListAsync();

                return Ok(students);
               
            }
            return BadRequest("Student not found");
        }
        [Authorize(Roles = "Student")]
        [HttpGet("students/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            // Lấy userId từ token
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            // Tìm sinh viên theo id và kiểm tra UserId có khớp với user đang đăng nhập không
            var student = await _context.Students
                .Include(s => s.User)
                .Where(s => s.Id == id && s.UserId == userId)
                .Select(s => new
                {
                    s.Id,
                    s.StudentCode,
                    s.Course,
                    s.Status,
                    User = new
                    {
                        s.User.FullName,
                        s.User.UserName,
                        s.User.Email
                    }
                })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return Forbid(); // Không được phép nếu cố xem thông tin người khác
            }

            return Ok(student);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("tutors")]
        public async Task<IActionResult> GetTutors()
        {

            if (_context.Tutors.Any())
            {
                var tutors = await _context.Tutors
                .Include(t => t.User)
                .Select(t => new {
                    t.Id,
                    t.Department,
                    t.ExperienceYears,
                    t.Rating,
                    User = t.User != null ? new { t.User.FullName, t.User.UserName, t.User.Email } : null
                })
                .ToListAsync();
                return Ok(tutors);
            }
               return BadRequest("Tutor not found");

          
        }
        [Authorize(Roles = "Tutor")]
        [HttpGet("tutors/{id}")]
        public async Task<IActionResult> GetTutorById(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var tutor = await _context.Tutors
                .Include(t => t.User)
                .Where(t => t.Id == id && t.UserId == userId)
                .Select(t => new
                {
                    t.Id,
                    t.Department,
                    t.ExperienceYears,
                    t.Rating,
                    User = new
                    {
                        t.User.FullName,
                        t.User.UserName,
                        t.User.Email
                    }
                })
                .FirstOrDefaultAsync();

            if (tutor == null)
            {
                return Forbid(); // Không cho phép nếu không phải chính giảng viên đó
            }

            return Ok(tutor);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-student/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] RegisterStudentDto registerDto)
        {
            if (registerDto == null) return BadRequest("Invalid data!");

            var student = await _context.Students.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound("Student not found!");

            student.StudentCode = registerDto.StudentCode;
            student.Course = registerDto.Course;
            student.Status = registerDto.Status;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Student updated successfully!" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-tutor/{id}")]
        public async Task<IActionResult> UpdateTutor(int id, [FromBody] RegisterTutorDto registerDto)
        {
            if (registerDto == null) return BadRequest("Invalid data!");

            var tutor = await _context.Tutors.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
            if (tutor == null) return NotFound("Tutor not found!");

            tutor.Department = registerDto.Department;
            tutor.ExperienceYears = registerDto.ExperienceYears ?? 0;
            tutor.Rating = registerDto.Rating ?? 0;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Tutor updated successfully!" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-student/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound("Student not found!");

            if (student.User != null)
            {
                _context.Users.Remove(student.User);
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Student deleted successfully!" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-tutor/{id}")]
        public async Task<IActionResult> DeleteTutor(int id)
        {
            var tutor = await _context.Tutors.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
            if (tutor == null) return NotFound("Tutor not found!");

            if (tutor.User != null)
            {
                _context.Users.Remove(tutor.User);
            }

            _context.Tutors.Remove(tutor);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Tutor deleted successfully!" });
        }
    }
}
