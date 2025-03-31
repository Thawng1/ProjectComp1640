using Microsoft.AspNetCore.Identity;

namespace ProjectComp1640.Model
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public virtual Student Students { get; set; }
        public virtual Tutor Tutors { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
