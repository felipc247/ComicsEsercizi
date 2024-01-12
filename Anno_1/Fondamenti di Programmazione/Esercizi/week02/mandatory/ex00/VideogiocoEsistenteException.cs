using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_videogiochi
{
    internal class VideogiocoEsistenteException : Exception
    {
        public VideogiocoEsistenteException() {
            CC.RedFr("Il videogioco esiste già");
        }
    }
}
