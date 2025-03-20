namespace ProjectComp1640.Dtos.Other
{
    public class ClassDto
    {
        public int? TutorId { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public List<int> StudentIds { get; set; }
    }
}
