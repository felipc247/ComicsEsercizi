using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricerca_Avanzata_Dictionary
{
    internal class PiattaformaEsistenteException : Exception
    {
        public PiattaformaEsistenteException() {
            CC.RedFr("La piattaforma è già presente");
        }
    }
}
