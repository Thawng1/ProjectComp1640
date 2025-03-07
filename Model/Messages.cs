using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectComp1640.Model
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; }  // Liên kết với AppUser

        [Required]
        public string ReceiverId { get; set; }  // Liên kết với AppUser

        [Required]
        public string Content { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("SenderId")]
        public virtual AppUser Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual AppUser Receiver { get; set; }
    }
}
