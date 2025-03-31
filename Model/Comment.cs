using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectComp1640.Model
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        
        
        public int? Id { get; set; }
        public Blog? Blog { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
