﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Dtos.Schedule;
using ProjectComp1640.Model;

namespace ProjectComp1640.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public ScheduleController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/Schedule
        [HttpGet("get-all-schedules")]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedules()
        {
            var schedules = await _dbContext.Schedules
                .Include(s => s.Class)
                .Include(s => s.Classroom)
                .ToListAsync();
            var scheduleList = schedules.Select(s => new ScheduleDto
            {
                Day = s.Day,
                Slot = s.Slot,
                LinkMeeting = s.LinkMeeting,
                ClassId = s.ClassId,
                ClassroomId = s.ClassroomId,
            });
            return Ok(scheduleList);
        }
        // GET: api/Schedule/5
        [HttpGet("get-schedule/{id}")]
        public async Task<ActionResult<ScheduleDto>> GetSchedule(int id)
        {
            var s = await _dbContext.Schedules
                .Include(s => s.Class)
                .Include(s => s.Classroom)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (s == null)
            {
                return NotFound();
            }
            var dto = new ScheduleDto
            {
                Day = s.Day,
                Slot = s.Slot,
                LinkMeeting = s.LinkMeeting,
                ClassId = s.ClassId,
                ClassroomId = s.ClassroomId,
            };
            return Ok(dto);
        }
        [HttpPost("create-schedule")]
        public async Task<IActionResult> CreateSchedule(ScheduleDto scheduleDto)
        {
            var cls = await _dbContext.Classes.FirstOrDefaultAsync(c => c.Id == scheduleDto.ClassId);
            if (cls == null)
            {
                return NotFound("Không tìm thấy Lớp học");
            }
            var current = cls.StartDate;
            while (current.DayOfWeek != scheduleDto.Day)
            {
                current = current.AddDays(1);
            }
            var schedule = new Schedule
            {
                ScheduleDate = current,
                Day = scheduleDto.Day,
                Slot = scheduleDto.Slot,
                LinkMeeting = scheduleDto.LinkMeeting,
                ClassId = scheduleDto.ClassId,
                ClassroomId = scheduleDto.ClassroomId
            };
            _dbContext.Schedules.Add(schedule);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Id }, scheduleDto);
        }
        [HttpPut("update-schedule/{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, ScheduleDto scheduleDto)
        {
            var schedule = await _dbContext.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound("Không tìm thấy lịch học này");
            }
            var cls = await _dbContext.Classes.FindAsync(id);
            if (cls == null) {
                return NotFound("Không tìm thấy Lớp học");
            }
            schedule.Day = scheduleDto.Day;
            schedule.Slot = scheduleDto.Slot;
            schedule.LinkMeeting = scheduleDto.LinkMeeting;
            schedule.ClassId = scheduleDto.ClassId;
            schedule.ClassroomId = scheduleDto.ClassroomId;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("delete-schedule/{id}")]
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
        [HttpPost("create-recurring")]
        public async Task<IActionResult> CreateRecurringSchedule(ScheduleDto scheduleDto)
        {
            var cls = await _dbContext.Classes.FirstOrDefaultAsync(c => c.Id == scheduleDto.ClassId);
            if (cls == null)
            { 
                return NotFound("Class not found"); 
            }
            var current = cls.StartDate;
            while (current.DayOfWeek != scheduleDto.Day)
            {
                current = current.AddDays(1);
            }
            var firstWeekDate = cls.StartDate;
            var schedules = new List<Schedule>();
            while (current <= cls.EndDate)
            {
                schedules.Add(new Schedule
                {
                    ScheduleDate = current,
                    Day = scheduleDto.Day,
                    Slot = scheduleDto.Slot,
                    LinkMeeting = scheduleDto.LinkMeeting,
                    ClassId = scheduleDto.ClassId,
                    ClassroomId = scheduleDto.ClassroomId
                });
                current = current.AddDays(7);
            }
            _dbContext.Schedules.AddRange(schedules);
            await _dbContext.SaveChangesAsync();
            return Ok(schedules.Select(s => new
            {
                s.Id,
                s.ScheduleDate,
                s.Day,
                s.Slot,
                s.LinkMeeting,
                s.ClassId,
                s.ClassroomId
            }));
        }

    }
}
