using Microsoft.AspNetCore.SignalR;

namespace SignalRxoGame.Classes
{
    public class GameHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Connector Id '{Context.ConnectionId}' connected");
            return base.OnConnectedAsync();
        }
    }
}
