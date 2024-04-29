using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricerca_Avanzata_Dictionary
{
    internal class GiocoEsistenteException : Exception
    { 
        public GiocoEsistenteException() {
            CC.RedFr("Il gioco esiste già");
        }
    }
}
