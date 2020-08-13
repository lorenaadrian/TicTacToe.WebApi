using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeCore.Models
{
    public struct Position
    {
        public int x, y;
        public Position(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }
    }
}
