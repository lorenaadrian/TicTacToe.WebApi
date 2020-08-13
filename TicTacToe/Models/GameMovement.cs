using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeCore.Models;

namespace TicTacToe.Models
{
    public class GameMovement : IGameMovement
    {
        public Position Position { get; set; }
        public Players Player { get; set; }

        public ActionResulting Movement(string id)
        {
            return MovementGameTicTacToe.MovimentGame(Position, Player, id);
        }

    }
}
