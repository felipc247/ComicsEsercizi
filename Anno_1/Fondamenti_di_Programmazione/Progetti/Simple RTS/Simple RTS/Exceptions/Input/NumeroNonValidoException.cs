using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Input
{
    internal class NumeroNonValidoException : Exception
    {
        public NumeroNonValidoException() {
            CC.RedFr("Il numero inserito non è valido\n");
        }
    }
}
