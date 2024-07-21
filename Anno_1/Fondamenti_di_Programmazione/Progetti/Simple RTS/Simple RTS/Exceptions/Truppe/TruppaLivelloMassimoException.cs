using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Truppe
{
    internal class TruppaLivelloMassimoException : Exception
    {
        public TruppaLivelloMassimoException() {
            CC.GreenFr("TRUPPA GIA' AL LIVELLO MAX\n");
        }
    }
}
