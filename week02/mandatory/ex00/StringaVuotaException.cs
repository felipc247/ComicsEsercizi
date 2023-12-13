using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_Videogiochi
{
    internal class StringaVuotaException : Exception
    {
        public StringaVuotaException() {
            Console.WriteLine("INSERISCI QUALCOSA");
        }
    }
}
