using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_1
{
    internal class Auto : Veicolo
    {
        private int num_finestrini;

        public Auto(string marca, string modello, int num_finestrini) : base(marca, modello)
        {
            this.num_finestrini = num_finestrini;
        }

        // GET // SET

        public int Num_finestrini { get { return num_finestrini; } }

        // Methods

        public override string Descrizione()
        {
            return base.Descrizione() + $" , Num finestrini: {num_finestrini}";
        }
    }
}
