using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Agenzia_Immobiliare
{
    internal class Box : Immobile
    {

        public Box(string id, string indirizzo, int cap, string citta, double superficie) : base(id, indirizzo, cap, citta, superficie)
        {
        }
    }
}
