using System.ComponentModel.DataAnnotations;

namespace ProjectComp1640.Model
{
    public class Subject
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string SubjectName { get; set; }
        public string Information { get; set; }
        public virtual ICollection<Class> Classes{ get; set; } = new List<Class>();
    }
}
