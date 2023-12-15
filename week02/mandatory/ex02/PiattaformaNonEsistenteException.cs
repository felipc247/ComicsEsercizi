using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricerca_Avanzata_Dictionary
{
    internal class PiattaformaNonEsistenteException : Exception
    {
        public PiattaformaNonEsistenteException() {
            CC.RedFr("Piattaforma non presente");
        }
    }
}
