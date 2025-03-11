using Microsoft.AspNetCore.SignalR;
using System;
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
            var userId = Context.User?.Identity?.Name;
            if (userId != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.Identity?.Name;
            if (userId != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
