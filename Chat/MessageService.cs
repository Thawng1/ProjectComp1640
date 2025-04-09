using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Model;
using ProjectComp1640.NotificationConnect;

namespace ProjectComp1640.Chat
{
    public class MessageService
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly NotificationService _notificationService;

        public MessageService(ApplicationDBContext context, UserManager<AppUser> userManager, 
                              IHubContext<MessageHub> hubContext, NotificationService notificationService)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
            _notificationService = notificationService;
        }

        public async Task SendMessage(string senderId, string receiverId, string content)
        {
            var sender = await _userManager.FindByIdAsync(senderId);
            var receiver = await _userManager.FindByIdAsync(receiverId);

            if (sender == null || receiver == null)
                throw new Exception("Người gửi hoặc người nhận không tồn tại.");

            // Kiểm tra chỉ Student và Tutor có thể gửi tin nhắn
            if (!await IsStudentOrTutor(sender) || !await IsStudentOrTutor(receiver))
                throw new Exception("Chỉ Student và Tutor có thể nhắn tin!");

            var message = new Messages
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SentAt = DateTime.Now
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            Console.WriteLine("📨 Gửi SignalR tới: " + receiverId);
            // Gửi tin nhắn đến client của người nhận
            await _hubContext.Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, content, message.SentAt);
            await _hubContext.Clients.User(senderId)
               .SendAsync("ReceiveMessage", senderId, content, message.SentAt);

            // Gửi thông báo realtime
            string notiMessage = $"📩 Bạn có tin nhắn mới từ {sender.UserName}";
            

            await _notificationService.SendNotification(
                receiverId,
                notiMessage,
                senderId
            );
        }
        public async Task<List<Messages>> GetMessages(string senderId, string receiverId)
        {
            var messages = await _context.Messages
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
            var relatedNotifications = await _context.Notifications
                .Where(n => n.UserId == senderId &&         // người đang xem
                            n.SenderId == receiverId &&      // người đã gửi
                            !n.IsRead &&
                            n.Message.Contains("tin nhắn mới"))
                .ToListAsync();

                    foreach (var noti in relatedNotifications)
                    {
                        noti.IsRead = true;
                    }

                    if (relatedNotifications.Count > 0)
                        await _context.SaveChangesAsync();
            return messages;
        }

        private async Task<bool> IsStudentOrTutor(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains("Student") || roles.Contains("Tutor");
        }
    }
}
