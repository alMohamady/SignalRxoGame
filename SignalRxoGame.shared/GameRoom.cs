using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRxoGame.shared
{
    public class GameRoom
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public List<Player> players { get; set; } = new();
        public XoGame myGame { get; set; } = new();


        public bool AddPlayer(Player player)
        {
            if (!players.Contains(player) && players.Count < 2)
            {
                players.Add(player);
                if (players.Count == 1)
                {
                    myGame.xPlayerConId = player.connectionId;
                }
                else
                {
                   myGame.oPlayerConId = player.connectionId;   
                }
                return true;
            }
            return false;
        }
    }
}
