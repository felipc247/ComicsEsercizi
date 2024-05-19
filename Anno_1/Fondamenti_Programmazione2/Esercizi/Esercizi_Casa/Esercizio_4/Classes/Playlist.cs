using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_4
{
    internal class Playlist
    {
        private List<Brano> branos;

        public Playlist() { 
            branos = new List<Brano>(); 
        }

        // GET // SET

        // Methods

        public void Add_Playlist(Brano brano)
        {
            branos.Add(brano);
        }

        public List<string> Get_Descrizioni() { 
            List<string> ret = new List<string>();
            foreach (Brano brano in branos)
                ret.Add(brano.Descrizione());
            return ret;

        }
    }
}
