using Microsoft.AspNetCore.SignalR;
using SignalRxoGame.shared;

namespace SignalRxoGame.Classes
{
    public class GameHub : Hub
    {
        private static readonly List<GameRoom> gameRooms = new List<GameRoom>();

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Connector Id '{Context.ConnectionId}' connected");

            await Clients.Caller.SendAsync("Rooms", gameRooms.OrderBy(r => r.RoomName));

            //return base.OnConnectedAsync();
        }
    }
}
