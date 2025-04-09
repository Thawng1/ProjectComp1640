using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Dtos.Dashboard;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ApplicationDBContext _context;

    public DashboardController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        var totalStudents = await _context.Students.CountAsync();
        var totalTutors = await _context.Tutors.CountAsync();
        var totalClasses = await _context.Classes.CountAsync();

        // Lấy số buổi học hôm nay
        var lessonsToday = await _context.Schedules
            .CountAsync(s => s.ScheduleDate.Date == DateTime.Today);

        // Biểu đồ học sinh mới tạo theo tháng
        var monthlyStudentStats = await _context.Students
            .GroupBy(s => s.CreatedAt.Month)
            .Select(g => new ChartDataDto
            {
                Label = $"Tháng {g.Key}",
                Value = g.Count()
            }).ToListAsync();

        // Top 5 giảng viên theo số lớp và rating
        var topTutors = await _context.Tutors
            .Include(t => t.User)
            .OrderByDescending(t => t.Classes.Count)
            .Take(5)
            .Select(t => new TopTutorDto
            {
                FullName = t.User != null ? t.User.FullName : "Unknown",
                TotalClasses = t.Classes.Count,
                Rating = t.Rating
            }).ToListAsync();

        var dashboard = new DashboardDto
        {
            TotalStudents = totalStudents,
            TotalTutors = totalTutors,
            TotalClasses = totalClasses,
            LessonsToday = lessonsToday,
            MonthlyStudentStats = monthlyStudentStats,
            TopTutors = topTutors
        };

        return Ok(dashboard);
    }
}