using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeCore.Models;

namespace TicTacToe.Models
{
    public class GameTicTacToe : IGameTicTacToe
    {
        private TicTacToeCore.Models.GameTicTacToe _game;
        public string Id { get; private set; }
        public string FirstPlayer { get; set; }
        public GameTicTacToe()
        {
            _game = new TicTacToeCore.Models.GameTicTacToe();
            if (_game.StartGame().StatusAction)
            {
                Id = _game.Id.ToString();
                FirstPlayer = _game.Player.ToString();
            }
        }

    }
}
