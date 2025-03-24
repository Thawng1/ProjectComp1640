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
    public class ClassroomController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public ClassroomController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/Classroom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassroomDto>>> GetClassrooms()
        {
            var dtoList = await _dbContext.Classrooms
                .Select(c => new ClassroomDto { Name = c.Name })
                .ToListAsync();

            return Ok(dtoList);
        }
        // GET: api/Classroom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classroom>> GetClassroom(int id)
        {
            var classroom = await _dbContext.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }
            return classroom;
        }
        // POST: api/Classroom
        [HttpPost]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassroom(int id, ClassroomDto classroomDto)
        {
            var classroom = await _dbContext.Classrooms.FindAsync(id);
            if (classroom == null)
            {  
                return NotFound();
            }
            classroom.Name = classroomDto.Name;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        // DELETE: api/Classroom/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassroom(int id)
        {
            var classroom = await _dbContext.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }
            _dbContext.Classrooms.Remove(classroom);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        private bool ClassroomExists(int id)
        {
            return _dbContext.Classrooms.Any(e => e.Id == id);
        }
    }
}
