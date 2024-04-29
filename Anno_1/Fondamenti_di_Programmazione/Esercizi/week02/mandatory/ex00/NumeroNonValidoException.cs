using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_videogiochi
{
    internal class NumeroNonValidoException : Exception
    {
        public NumeroNonValidoException() {
            CC.RedFr("Inserisci un numero valido");
        }

    }
}
