using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Agenzia_Immobiliare
{
    internal class Appartamento : Immobile
    {
        private int numero_vani;
        private int numero_bagni;
        public Appartamento(string id, string indirizzo, int cap, string citta, double superficie, int numero_vani, int numero_bagni) : base(id, indirizzo, cap, citta, superficie)
        {
            this.numero_vani = numero_vani;
            this.numero_bagni = numero_bagni;
        }

        public override string ToString()
        {
            return base.ToString() + $" {numero_vani} {numero_bagni}";
        }
    }
}
