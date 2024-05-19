using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Agenzia_Immobiliare
{
    internal class Immobile
    {
        // attributi generali
        private string id;
        private string indirizzo;
        private int cap;
        private string citta;
        private double superficie;

        public Immobile(string id, string indirizzo, int cap, string citta, double superficie) { 
            this.id = id;
            this.indirizzo = indirizzo;
            this.cap = cap;
            this.citta = citta;
            this.superficie = superficie;
        }

        // GET
        public string Id { get { return id; } }

        public string Indirizzo { get {  return indirizzo; } }

        public int Cap { get { return cap; } }

        public string Citta { get {  return citta; } }

        public double Superficie { get { return superficie; } }

        // Methods

        public virtual string ToString() {
            return $"{id} {indirizzo} {cap} {citta} {superficie}";
        }
    }
}
