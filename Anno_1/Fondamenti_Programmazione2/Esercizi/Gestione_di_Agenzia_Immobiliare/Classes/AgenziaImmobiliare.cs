using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Agenzia_Immobiliare
{
    internal class AgenziaImmobiliare
    {
        private List<Immobile> immobiles;

        public AgenziaImmobiliare()
        {
            immobiles = new List<Immobile>();
        }

        // GET // SET

        public List<Immobile> Immobiles
        {
            get { return immobiles; }
        }

        // Methods

        public void Add_Immobile(Immobile immobile) { 
            immobiles.Add(immobile);
        }

        public List<Immobile> SearchImmobile(string key) {
            return immobiles.FindAll(i => i.Id.Equals(key));
        }

    }
}
