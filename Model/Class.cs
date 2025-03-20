using System.ComponentModel.DataAnnotations;

namespace ProjectComp1640.Model
{
    public class Class
    {
        public int Id { get; set; }
        public int? TutorId { get; set; }
        public virtual Tutor? Tutor { get; set; }
        public virtual ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();
        [Required]
        public string ClassName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
