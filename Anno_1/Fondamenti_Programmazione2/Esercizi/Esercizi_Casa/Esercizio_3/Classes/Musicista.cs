using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_3
{
    internal class Musicista
    {
        public Musicista() { }
        
        public void Esegui_Suonata(ISuonabile suonabile) {
            suonabile.Suona();
        }
    }
}
