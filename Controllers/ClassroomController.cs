using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Dtos.Classroom;
using ProjectComp1640.Model;

namespace ProjectComp1640.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ClassroomController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public ClassroomController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/Classroom
        [HttpGet("get-all-classrooms")]
        public async Task<ActionResult<IEnumerable<GetClassroomDto>>> GetClassrooms()
        {
            var dtoList = await _dbContext.Classrooms
                .Select(c => new GetClassroomDto { 
                    Id = c.Id,
                    Name = c.Name 
                })
                .ToListAsync();

            return Ok(dtoList);
        }
        // GET: api/Classroom/5
        [HttpGet("get-classroom/{id}")]
        public async Task<ActionResult<Classroom>> GetClassroom(int id)
        {
            var classroom = await _dbContext.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound(new { message = "Cannot find this classroom." });
            }
            return classroom;
        }
        // POST: api/Classroom
        [HttpPost("create-classroom")]
        public async Task<ActionResult<Classroom>> CreateClassroom(ClassroomDto classroomDto)
        {
            if (_dbContext.Classrooms.Any(c => c.Name == classroomDto.Name))
            {
                return BadRequest("Classroom name already exists.");
            }
            var classroom = new Classroom 
            { 
                Name = classroomDto.Name 
            };
            _dbContext.Classrooms.Add(classroom);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClassroom), new { id = classroom.Id }, classroom);
        }
        [HttpPut("update-classroom/{id}")]
        public async Task<IActionResult> PutClassroom(int id, ClassroomDto classroomDto)
        {
            var classroom = await _dbContext.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound(new { message = "Cannot find this classroom." });
            }
            classroom.Name = classroomDto.Name;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        // DELETE: api/Classroom/5
        [HttpDelete("delete-classroom/{id}")]
        public async Task<IActionResult> DeleteClassroom(int id)
        {
            var classroom = await _dbContext.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound(new { message = "Cannot find this classroom." });
            }
            _dbContext.Classrooms.Remove(classroom);
            await _dbContext.SaveChangesAsync();
            return Ok(new { message = "Delete classroom succesfully." });
        }
    }
}
