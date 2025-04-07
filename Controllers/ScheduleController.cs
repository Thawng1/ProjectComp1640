using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Dtos.Schedule;
using ProjectComp1640.Model;

namespace ProjectComp1640.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public ScheduleController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("create-schedule")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSchedule(ScheduleDto scheduleDto)
        {
            var cls = await _dbContext.Classes.FirstOrDefaultAsync(c => c.Id == scheduleDto.ClassId);
            if (cls == null)
            {
                return NotFound("Không tìm thấy Lớp học");
            }
            var clsrm = await _dbContext.Classrooms.AnyAsync(c => c.Id == scheduleDto.ClassroomId);
            if (!clsrm)
            {
                return NotFound($"Không tìm thấy phòng học với ID {scheduleDto.ClassroomId}.");
            }
            var dupSchedule = await _dbContext.Schedules.AnyAsync(s =>
                s.ScheduleDate == scheduleDto.ScheduleDate &&
                s.Day == scheduleDto.Day &&
                s.Slot == scheduleDto.Slot &&
                s.ClassId == scheduleDto.ClassId &&
                s.ClassroomId == scheduleDto.ClassroomId
                );
            if (dupSchedule)
            {
                return BadRequest($"Đã có lịch học trùng của lớp {scheduleDto.ClassId} lớp học {scheduleDto.ClassroomId} vào thứ {scheduleDto.Day}, ngày ({scheduleDto.ScheduleDate:yyyy-MM-dd}) ở tiết học thứ {scheduleDto.Slot}.");
            }
            var schedule = new Schedule
            {
                ScheduleDate = scheduleDto.ScheduleDate,
                Day = scheduleDto.Day,
                Slot = scheduleDto.Slot,
                LinkMeeting = scheduleDto.LinkMeeting,
                ClassId = scheduleDto.ClassId,
                ClassroomId = scheduleDto.ClassroomId
            };
            _dbContext.Schedules.Add(schedule);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Id }, new { message = "Thêm lịch thành công", scheduleDto });
        }
        [HttpGet("get-all-schedules")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GetScheduleDto>>> GetSchedules()
        {
            var schedules = await _dbContext.Schedules
                .Include(s => s.Class)
                .Include(s => s.Classroom)
                .ToListAsync();
            var scheduleList = schedules.Select(s => new GetScheduleDto
            {
                Id = s.Id,
                ScheduleDate = s.ScheduleDate,
                Day = s.Day,
                Slot = s.Slot,
                LinkMeeting = s.LinkMeeting,
                ClassId = s.ClassId,
                ClassroomId = s.ClassroomId,
            });
            return Ok(scheduleList);
        }
        [HttpGet("get-schedule/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetScheduleDto>> GetSchedule(int id)
        {
            var s = await _dbContext.Schedules
                .Include(s => s.Class)
                .Include(s => s.Classroom)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            var dto = new GetScheduleDto
            {
                Id = s.Id,
                ScheduleDate = s.ScheduleDate,
                Day = s.Day,
                Slot = s.Slot,
                LinkMeeting = s.LinkMeeting,
                ClassId = s.ClassId,
                ClassroomId = s.ClassroomId,
            };
            return Ok(dto);
        }
        [HttpPut("update-schedule/{id}")]
        [Authorize(Roles = "Admin, Tutor")]
        public async Task<IActionResult> UpdateSchedule(int id, ScheduleDto scheduleDto)
        {
            var schedule = await _dbContext.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound("Không tìm thấy lịch học này.");
            }
            var cls = await _dbContext.Classes.FindAsync(scheduleDto.ClassId);
            if (cls == null) {
                return NotFound("Không tìm thấy Lớp học.");
            }
            var clsrm = await _dbContext.Classrooms.FindAsync(scheduleDto.ClassroomId);
            if (clsrm == null)
            {
                return NotFound($"Không tìm thấy phòng học.");
            }
            var dupSchedule = await _dbContext.Schedules.AnyAsync(s =>
                s.ScheduleDate == scheduleDto.ScheduleDate &&
                s.Day == scheduleDto.Day &&
                s.Slot == scheduleDto.Slot &&
                s.ClassId == scheduleDto.ClassId &&
                s.ClassroomId == scheduleDto.ClassroomId
                );
            if (dupSchedule)
            {
                return BadRequest($"Đã có lịch học trùng của lớp {scheduleDto.ClassId} lớp học {scheduleDto.ClassroomId} vào thứ {scheduleDto.Day}, ngày ({scheduleDto.ScheduleDate:yyyy-MM-dd}) ở tiết học thứ {scheduleDto.Slot}.");
            }
            schedule.ScheduleDate = scheduleDto.ScheduleDate;
            schedule.Day = scheduleDto.Day;
            schedule.Slot = scheduleDto.Slot;
            schedule.LinkMeeting = scheduleDto.LinkMeeting;
            schedule.ClassId = scheduleDto.ClassId;
            schedule.ClassroomId = scheduleDto.ClassroomId;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("delete-schedule/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _dbContext.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            _dbContext.Schedules.Remove(schedule);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("create-recurring-schedules")]
        [Authorize(Roles = "Admin")]
        //public async Task<IActionResult> CreateRecurringSchedule(ScheduleDto scheduleDto)
        //{
        //    var cls = await _dbContext.Classes.FirstOrDefaultAsync(c => c.Id == scheduleDto.ClassId);
        //    if (cls == null)
        //    { 
        //        return NotFound("Không tìm thấy lớp học"); 
        //    }
        //    var clsrm = await _dbContext.Classrooms.AnyAsync(c => c.Id == scheduleDto.ClassroomId);
        //    if (clsrm == null)
        //    {
        //        return NotFound($"Không tìm thấy phòng học.");
        //    }
        //    var current = cls.StartDate;
        //    while (current.DayOfWeek != scheduleDto.Day)
        //    {
        //        current = current.AddDays(1);
        //    }
        //    var schedules = new List<Schedule>();
        //    while (current <= cls.EndDate)
        //    {
        //        schedules.Add(new Schedule
        //        {
        //            ScheduleDate = current,
        //            Day = scheduleDto.Day,
        //            Slot = scheduleDto.Slot,
        //            LinkMeeting = scheduleDto.LinkMeeting,
        //            ClassId = scheduleDto.ClassId,
        //            ClassroomId = scheduleDto.ClassroomId
        //        });
        //        current = current.AddDays(7);
        //    }
        //    _dbContext.Schedules.AddRange(schedules);
        //    await _dbContext.SaveChangesAsync();
        //    return Ok(schedules.Select(s => new
        //    {
        //        s.Id,
        //        s.ScheduleDate,
        //        s.Day,
        //        s.Slot,
        //        s.LinkMeeting,
        //        s.ClassId,
        //        s.ClassroomId
        //    }));
        //}
        public async Task<IActionResult> CreateRecurringSchedule(ScheduleDto scheduleDto)
        {
            var cls = await _dbContext.Classes.FirstOrDefaultAsync(c => c.Id == scheduleDto.ClassId);
            if (cls == null)
            {
                return NotFound("Không tìm thấy lớp học");
            }
            var clsrm = await _dbContext.Classrooms.AnyAsync(c => c.Id == scheduleDto.ClassroomId);
            if (clsrm == null)
            {
                return NotFound($"Không tìm thấy phòng học.");
            }
            var scheduleDates = new List<DateTime>();
            var current = cls.StartDate;
            while (current.DayOfWeek != scheduleDto.Day)
            {
                current = current.AddDays(1);
            }
            while (current <= cls.EndDate)
            {
                scheduleDates.Add(current);
                current = current.AddDays(7);
            }
            var dupSchedule = await _dbContext.Schedules.Where(
                s => scheduleDates.Contains(s.ScheduleDate) &&
                s.Day == scheduleDto.Day &&
                s.Slot == scheduleDto.Slot &&
                s.ClassId == scheduleDto.ClassId &&
                s.ClassroomId == scheduleDto.ClassroomId)
                .Select(s => s.ScheduleDate)
                .ToListAsync();
            if (dupSchedule.Any())
            {
                return BadRequest(new
                    {
                        Message = "Tồn tại lịch học trùng nên không thể tạo mới.",
                        Conflicts = dupSchedule.Select(d => d.ToString("yyyy-MM-dd"))
                    });
            }
            var schedules = scheduleDates.Select(date => new Schedule
            {
                ScheduleDate = date,
                Day = scheduleDto.Day,
                Slot = scheduleDto.Slot,
                LinkMeeting = scheduleDto.LinkMeeting,
                ClassId = scheduleDto.ClassId,
                ClassroomId = scheduleDto.ClassroomId
            }).ToList();
            _dbContext.Schedules.AddRange(schedules);
            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "Lịch học được tạo thành công.",
                Created = schedules.Select(s => new
                {
                    s.Id,
                    s.ScheduleDate,
                    s.Day,
                    s.Slot,
                    s.LinkMeeting,
                    s.ClassId,
                    s.ClassroomId
                })
            });
        }

    }
}
