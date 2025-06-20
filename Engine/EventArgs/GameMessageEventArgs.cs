using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.EventArgs
{
    public class GameMessageEventArgs : System.EventArgs
    {
        public string Message { get; private set; }

        public static System.EventArgs GameMessageEventArgsNull = Empty;

        public GameMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
