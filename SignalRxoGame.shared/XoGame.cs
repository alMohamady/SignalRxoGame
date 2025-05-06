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

        public void setPlayer()
        {
            CurrentPlayerId = CurrentPlayerId == xPlayerConId ? oPlayerConId : xPlayerConId;
        }

        public bool PlayMove(int row, int col, string playerId)
        {
            if ( playerId != CurrentPlayerId || row < 0 || row >= 3 ||  col < 0 || col >= 3 
                || GameBoard[row][col] != string.Empty)
            {
                return false;
            }
            GameBoard[row][col] = currentSympol;
            setPlayer();
            return true;
        }

        public string CheckWinner()
        {
            string[] players = { "x", "o" };

            foreach (var player in players)
            {
                // Check rows
                for (int i = 0; i < 3; i++)
                {
                    if (GameBoard[i][0] == player && GameBoard[i][1] == player && GameBoard[i][2] == player)
                        return player;
                }

                // Check columns
                for (int i = 0; i < 3; i++)
                {
                    if (GameBoard[0][i] == player && GameBoard[1][i] == player && GameBoard[2][i] == player)
                        return player;
                }

                // Check diagonals
                if (GameBoard[0][0] == player && GameBoard[1][1] == player && GameBoard[2][2] == player)
                    return player;

                if (GameBoard[0][2] == player && GameBoard[1][1] == player && GameBoard[2][0] == player)
                    return player;
            }

            return string.Empty;
        }

        public bool CheckIfDrow()
        {
            return GameDraw = GameBoard.All(row => row.All(col => !string.IsNullOrEmpty(col))); 
        }
    }
}
