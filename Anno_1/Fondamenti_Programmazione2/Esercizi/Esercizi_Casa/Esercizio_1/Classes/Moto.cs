using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_1
{
    internal class Moto : Veicolo
    {
        private string colore;

        public Moto(string marca, string modello, string colore) : base(marca, modello)
        {
            this.colore = colore;
        }

        // GET // SET

        public string Colore { get { return colore; } }

        // Methods

        public override string Descrizione()
        {
            return base.Descrizione() + $" , Colore: {colore}";
        }
    }
}
