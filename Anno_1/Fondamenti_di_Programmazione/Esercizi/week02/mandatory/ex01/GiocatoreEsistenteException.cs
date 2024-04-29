using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfida_Algoritmica___Ordinamento
{
    internal class GiocatoreEsistenteException : Exception
    {
        public GiocatoreEsistenteException() {
            CC.RedFr("Giocatore già esistente");
        }
    }
}
