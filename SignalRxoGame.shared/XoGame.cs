using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRxoGame.shared
{
    public class XoGame
    {
        public string xPlayerConId { get; set; }
        public string oPlayerConId { get; set; }
        public string? CurrentPlayerId { get; set; }
        public string currentSympol => CurrentPlayerId == xPlayerConId ? "X" : "O";
        public bool GameStart { get; set; } 
        public bool GameStop { get; set; }
        public bool GameOver { get; set; }
        public bool GameDraw { get; set; }
        public string winnerId { get; set; } = string.Empty;
        public List<List<string>> GameBoard { get; set; } = new List<List<string>>(3);

        public XoGame()
        {
            InitGameBoard();
        }

        public void InitGameBoard()
        {
            GameBoard.Clear();
            for (int i = 0; i < 3; i++)
            {
                var row = new List<string>(3);
                for(int j = 0; j < 3; j++)
                {
                    row.Add(string.Empty);
                }
                GameBoard.Add(row);
            }
        }

        public void StartNewGame()
        {
            CurrentPlayerId = xPlayerConId;
            GameStart = true;
            GameStop = false;
            GameOver = false;
            winnerId = string.Empty;
            GameDraw = false;
            InitGameBoard();
        }
    }
}
