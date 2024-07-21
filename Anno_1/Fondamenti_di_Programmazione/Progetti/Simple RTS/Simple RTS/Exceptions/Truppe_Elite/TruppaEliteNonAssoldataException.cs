using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Truppe_Elite
{
    internal class TruppaEliteNonAssoldataException : Exception
    {
        TruppaEliteNonAssoldataException() {
            CC.RedFr("Devi assoldare la Truppa Elite per attivare!\n");
        }
    }
}
