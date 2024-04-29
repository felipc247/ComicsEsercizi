using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_videogiochi
{
    internal class VideogiocoNonEsistenteException : Exception
    {
        public VideogiocoNonEsistenteException() {
            CC.RedFr("Il videogioco non esiste");
        }
    }
}
