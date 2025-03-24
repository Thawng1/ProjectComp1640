using System.ComponentModel.DataAnnotations;

namespace ProjectComp1640.Model
{
    public class Classroom
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    }
}
