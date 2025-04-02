namespace ProjectComp1640.Dtos.Other
{
    public class ScheduleDto
    {
        public DayOfWeek Day { get; set; }
        public string Slot { get; set; }
        public string LinkMeeting {  get; set; }
        public int? ClassId { get; set; }
        public int? ClassroomId { get; set; }
    }
}
