using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricerca_Avanzata_Dictionary
{
    internal class StringaVuotaException : Exception
    {
        public StringaVuotaException() {
            CC.RedFr("INSERISCI QUALCOSA");
        }  
    }
}
