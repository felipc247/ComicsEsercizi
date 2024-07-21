using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Base
{
    // La Torre fornisce un bonus ATK a tutte le Truppe ed Eroi
    // in difesa, assorbe i danni in caso non ci siano
    // più Truppe a difesa

    internal class Torre
    {
        private int vita = 10000;
        private int bonus_Atk_Distanza = 20; // percentuale

        public Torre() { }

        public int Vita
        {
            get { return vita; }
            set
            {
                if (vita - value < 0)
                {
                    CC.RedFr("La Torre è caduta!\n");
                    vita = 0;
                }
                else
                {
                    CC.DarkYellowFr($"La Torre assorbe {value} danni avversari\n");
                    vita -= value;
                }
            }
        }

        public int Bonus_Atk_Distanza { get { return bonus_Atk_Distanza; } }

        public void Danneggia(int value) { 
            Vita = value;
        }


    }
}
