using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfida_Algoritmica___Ordinamento
{
    internal class PunteggioNonValidoException : Exception
    {
        public PunteggioNonValidoException() {
            CC.RedFr("Il punteggio non può essere negativo\n");
        }
    }
}
