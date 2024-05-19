using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_1
{
    internal class Veicolo
    {
        private string marca;
        private string modello;

        public Veicolo(string marca, string modello)
        {
            this.marca = marca;
            this.modello = modello;
        }

        // GET // SET

        public string Marca { get { return marca; } }

        public string Modello { get {  return modello; } }

        // Methods

        public virtual string Descrizione() {
            return $"Marca: {marca}, Modello: {modello}";
        }

    }
}
