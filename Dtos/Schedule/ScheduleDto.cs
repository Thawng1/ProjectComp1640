namespace ProjectComp1640.Dtos.Schedule
{
    public class ScheduleDto
    {
        public DateTime ScheduleDate { get; set; }
        public DayOfWeek Day { get; set; }
        public string Slot { get; set; }
        public string LinkMeeting {  get; set; }
        public int? ClassId { get; set; }
        public int? ClassroomId { get; set; }
    }
}
