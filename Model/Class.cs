using System.ComponentModel.DataAnnotations;

namespace ProjectComp1640.Model
{
    public class Class
    {
        public int Id { get; set; }
        [Required]
        public string ClassName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int TotalSlot { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? TutorId { get; set; }
        public virtual Tutor? Tutor { get; set; }
        public int? SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();
    }
}
