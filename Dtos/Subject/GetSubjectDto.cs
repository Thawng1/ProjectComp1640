using ProjectComp1640.Dtos.Class;

namespace ProjectComp1640.Dtos.Subject
{
    public class GetSubjectDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string Information { get; set; }
        public List<CreateClassDto> Classes { get; set; } = new List<CreateClassDto>();
    }
}
