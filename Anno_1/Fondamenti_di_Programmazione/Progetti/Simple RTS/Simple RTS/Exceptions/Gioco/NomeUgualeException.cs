using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Gioco
{
    internal class NomeUgualeException : Exception
    {
        public NomeUgualeException() {
            CC.RedFr("Non puoi inserire un nome già scelto!\n");
        }
    }
}
