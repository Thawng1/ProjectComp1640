using System;

namespace ProjectComp1640.Model
{
    public class Admin
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Liên kết với AppUser
        public string Department { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual  AppUser User { get; set; }
    }
}
