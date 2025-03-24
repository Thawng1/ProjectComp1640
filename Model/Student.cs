using System;

namespace ProjectComp1640.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string StudentCode { get; set; }
        public string Course { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual AppUser User { get; set; }
        public virtual ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();
    }
}
