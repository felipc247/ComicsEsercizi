using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_Videogiochi
{
    internal class PunteggioNonValidoException : Exception
    {
        public PunteggioNonValidoException() {
            Console.WriteLine("Inserisci un intero tra 1 e 10");
        }

        public PunteggioNonValidoException(string message) {
            Console.WriteLine(message);
        }
    }
}
