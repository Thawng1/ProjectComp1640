namespace ProjectComp1640.Dtos.Class
{
    public class ClassDto
    {
        public string TutorName { get; set; }
        public int? TutorId { get; set; }
        public int? SubjectId { get; set; }
        public string ClassName { get; set; }
        public int TotalSlot { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public List<int> StudentIds { get; set; }
    }
}
