using ProjectComp1640.Model;

namespace ProjectComp1640.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
      //public int? BlogId { get; set; }
        public string UserFullName { get; set; }
        public string UserId { get; set; }


    }
}
