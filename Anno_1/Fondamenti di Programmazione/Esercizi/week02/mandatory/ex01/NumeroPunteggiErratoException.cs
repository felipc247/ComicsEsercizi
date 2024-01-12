using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfida_Algoritmica___Ordinamento
{
    internal class NumeroPunteggiErratoException : Exception
    {
        public NumeroPunteggiErratoException() {
            CC.RedFr("Il numero di punteggi inseriti non è adeguato\n");
        }
    }
}
