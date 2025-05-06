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
                }
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, _room.RoomId);
            await Clients.All.SendAsync("Rooms", gameRooms.OrderBy(r => r.RoomName));
            return _room;
        }

        public async Task<GameRoom?> JoinRoom(string roomId, string playerName)
        {
            var room = gameRooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room is not null)
            {
                var newPalyer = new Player()
                {
                    connectionId = Context.ConnectionId,
                    name = playerName
                };
                if (room.AddPlayer(newPalyer))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, room.RoomId);
                    await Clients.Group(room.RoomId).SendAsync("NewPlayerJoined", newPalyer);
                    return room;
                }
            }
            return null;
        }

        public async Task StartGame(string roomId)
        {
            var room = gameRooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room is not null)
            {
                room.myGame.StartNewGame();
                await Clients.Group(roomId).SendAsync("GameUpdate", room);
            }
        }

        public async Task PlayMove(string roomId, int row, int col, string playerId)
        {
            var room = gameRooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room is not null && room.myGame.PlayMove(row, col, playerId))
            {
                room.myGame.winnerId = room.myGame.CheckWinner();
                room.myGame.CheckIfDrow();
                if (!string.IsNullOrEmpty(room.myGame.winnerId) || room.myGame.GameDraw)
                {
                    room.myGame.GameOver = true;
                }
                await Clients.Group(roomId).SendAsync("GameUpdate", room);
            }
        }
    }
}
