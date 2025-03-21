namespace ProjectComp1640.Model
{
    public class Schedule
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public string Slot { get; set; }
        public int? ClassId { get; set; }
        public virtual Class? Class { get; set; }
    }
}
