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

        public async Task<GameRoom> CreateRoom(string playerName, string roomName)
        {
            var _room = gameRooms.Where(r => r.RoomName == roomName).FirstOrDefault();
            if (_room == null)
            {
                var room = new GameRoom()
                {
                    RoomId = Guid.NewGuid().ToString(),
                    RoomName = roomName,
                };

                var player = new Player()
                {
                    connectionId = Context.ConnectionId,
                    name = playerName
                };
                if (room.AddPlayer(player))
                {
                    gameRooms.Add(room);
                    _room = room;
                }            }
            await Clients.All.SendAsync("Rooms", gameRooms.OrderBy(r => r.RoomName));
            return _room;
        }
    }
}
