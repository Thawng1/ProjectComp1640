namespace ProjectComp1640.Dtos.Dashboard
{
    public class DashboardDto
    {
        public int TotalStudents { get; set; }
        public int TotalTutors { get; set; }
        public int TotalClasses { get; set; }
        public int LessonsToday { get; set; }
        public List<ChartDataDto> MonthlyStudentStats { get; set; }
        public List<TopTutorDto> TopTutors { get; set; }
    }
}
