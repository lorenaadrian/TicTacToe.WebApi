using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeCore.Models
{
    public class ActionResulting
    {
        public bool StatusAction { get; set; }
        public string MessageAction { get; set; }
        public string Winner { get; set; }
    }
}
