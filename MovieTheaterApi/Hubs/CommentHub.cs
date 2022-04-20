using Microsoft.AspNetCore.SignalR;

namespace MovieTheaterApi.Hubs
{
    public class CommentHub : Hub
    {
        public async Task SendAsync(string message)
        {
            await this.Clients.All.SendAsync("send",message);
        }
    }
}
