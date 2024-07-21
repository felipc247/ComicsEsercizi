using Simple_RTS.Truppe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Truppe_Elite.Guerra
{
    internal class Truppa_Elite_Guerra : Truppa_Elite
    {
        protected TT_Guerra tipologia_Guerra;

        public enum TT_Guerra
        {
            Mischia,
            Distanza,
            Tank
        }

        public Truppa_Elite_Guerra() { }

        public TT_Guerra Tipologia_Guerra { get { return tipologia_Guerra; } }
    }
}
