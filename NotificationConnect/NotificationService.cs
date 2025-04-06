using Microsoft.AspNetCore.SignalR;
using ProjectComp1640.Data;
using ProjectComp1640.Model;
using Microsoft.EntityFrameworkCore;

namespace ProjectComp1640.NotificationConnect
{
    public class NotificationService
    {
        private readonly ApplicationDBContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(ApplicationDBContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task SendNotification(string receiverId, string message, string? actionUrl = null, string? senderId = null)
        {
            var notification = new Notification
            {
                UserId = receiverId,
                SenderId = senderId,
                Message = message,
                ActionUrl = actionUrl
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.User(receiverId).SendAsync("ReceiveNotification", new
            {
                Message = message,
                Url = actionUrl,
                Time = notification.CreatedAt
            });
        }
        public async Task<List<Notification>> GetNotifications(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task MarkAsRead(int id)
        {
            var noti = await _context.Notifications.FindAsync(id);
            if (noti != null)
            {
                noti.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
