using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeCore.Models;

namespace TicTacToe.Models
{
    public interface IGameMovement
    {
        public ActionResulting Movement(string id);
    }
}
