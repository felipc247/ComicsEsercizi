using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Agenzia_Immobiliare
{
    internal class Villa : Immobile
    {
        private double dim_giardino;
        public Villa(string id, string indirizzo, int cap, string citta, double superficie, double dim_giardino) : base(id, indirizzo, cap, citta, superficie)
        {
            this.dim_giardino = dim_giardino;
        }

        public override string ToString()
        {
            return base.ToString() + $" {dim_giardino}";
        }
    }
}
