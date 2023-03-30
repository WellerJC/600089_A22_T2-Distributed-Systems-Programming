using Microsoft.AspNetCore.SignalR;

namespace SignalRLab.Hubs
{
    public class ChatroomHub : Hub
    {
        public async Task BroadCastMessage(string username, string message)
        {
            await Clients.All.SendAsync("GetMessage", username, message);
        }
    }
}
