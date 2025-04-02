using Microsoft.AspNetCore.SignalR;

namespace ProjectComp1640.NotificationConnect
{
    public class NotificationHub:Hub
    {
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
