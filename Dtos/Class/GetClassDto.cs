namespace ProjectComp1640.Dtos.Class
{
    public class GetClassDto
    {
        public int id { get; set; }
        public string TutorName { get; set; }
        public int TutorId { get; set; }
        public string TutorUserId { get; set; }
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public int TotalSlot { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public List<string> StudentNames { get; set; } = new List<string>();
        public List<int> StudentIds { get; set; } = new List<int>();
        public List<string> StudentUserIds { get; set; } = new List<string>();
    }
}
