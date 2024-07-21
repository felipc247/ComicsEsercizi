using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions
{
    internal class StringaVuotaException : Exception
    {
        public StringaVuotaException() {
            CC.RedFr("INSERISCI QUALCOSA\n");
        }
    }
}
