using System;

namespace ProjectComp1640.Model
{
    public class Tutor
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Department { get; set; }
        public int ExperienceYears { get; set; }
        public float Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual AppUser User { get; set; }
    }
}
