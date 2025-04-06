namespace ProjectComp1640.Dtos.Notification
{
    public class NotificationDto
    {
        public string Message { get; set; }
        public string? ActionUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
