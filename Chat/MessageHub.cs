using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectComp1640.Chat
{
    public class MessageHub:Hub    
    {
        public async Task SendMessage(string senderId, string receiverId, string content)
        {
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, content);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                            ?? Context.User?.FindFirst("nameid")?.Value;
            Console.WriteLine(" KẾT NỐI TỪ USER ID: " + userId);
            if (userId != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
                Console.WriteLine($"Người dùng {userId} đã vào group SignalR");
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                 ?? Context.User?.FindFirst("nameid")?.Value;
            if (userId != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
