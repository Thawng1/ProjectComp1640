using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectComp1640.Model
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }           // Người nhận

        public string? SenderId { get; set; }        // Người gửi (tuỳ chọn)

        [Required]
        public string Message { get; set; }          // Nội dung hiển thị

        public string? ActionUrl { get; set; }       // Link điều hướng khi click thông báo

        public bool IsRead { get; set; } = false;    // Trạng thái đã đọc

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Thời điểm tạo

        // Mối quan hệ với AppUser (EF Core sẽ tự join được)
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }     // Người nhận (1-N)

        [ForeignKey("SenderId")]
        public virtual AppUser Sender { get; set; }


    }
}
