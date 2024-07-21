using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Truppe_Elite
{
    internal class TruppaEliteGiaAssoldataException : Exception
    { 
        public TruppaEliteGiaAssoldataException() {
            CC.GreenFr("La Truppa Elite è già nel tuo esercito!\n");
        }
    }
}
