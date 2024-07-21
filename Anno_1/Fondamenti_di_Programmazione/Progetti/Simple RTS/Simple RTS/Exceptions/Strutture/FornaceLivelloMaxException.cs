using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Strutture
{
    internal class FornaceLivelloMaxException : Exception
    {
        public FornaceLivelloMaxException() {
            CC.GreenFr("Fornace al Livello MAX!\n");
        }
    }
}
