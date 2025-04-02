using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectComp1640.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? BlogId { get; set; }
        public Blog? Blog { get; set; }
        public string UserId { get; set; }   
        public AppUser User { get; set; }
    }
}
