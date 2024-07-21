using Simple_RTS.Truppe;
using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Base
{
    // Produce Frammenti ogni tuo turno
    internal class Fornace
    {
        private int frammenti = 50000;
        private int produzione = 200;
        private int livello = 1;
        private int[] costi_Potenziamento = { 1000, 2500, 5000, 15000 };
        private int costo_Potenziamento;

        public Fornace()
        {
            costo_Potenziamento = costi_Potenziamento[livello - 1];
        }

        // GET // SET

        public int Livello
        {
            get { return livello; }
        }

        public int Produzione
        {
            get { return produzione; }
        }

        public int Costo_Potenziamento
        {
            get { return costo_Potenziamento; }
        }

        public int[] Costi_Potenziamento
        {
            get { return costi_Potenziamento; }
        }

        public int Frammenti
        {
            get { return frammenti; }
            set { frammenti = (frammenti + value < 0) ? 0 : frammenti + value; }
        }

        // METODI

        // Potenziamento
        public void Potenzia()
        {
            // MAX livello 5
            livello = (livello + 1 > 5) ? 5 : livello + 1;

            int produzioneTemp = produzione;

            switch (livello)
            {
                case 2:
                    produzione = 500;
                    break;
                case 3:
                    produzione = 800;
                    break;
                case 4:
                    produzione = 1500;
                    break;
                case 5:
                    produzione = 3000;
                    break;
            }

            costo_Potenziamento = (livello < 5) ? costi_Potenziamento[livello - 1] : costi_Potenziamento[costi_Potenziamento.Length - 1];

            CC.GreenFr($"Successo!\n");
            CC.WhiteFr($"Produzione {produzioneTemp} => {produzione}\n");
        }

        public void Fusione()
        {
            int frammentiTemp = frammenti;
            frammenti += produzione;
            CC.YellowFr("Fusione avvenuta: "); CC.WhiteFr($"+ {produzione} Frammenti\n");
            CC.WhiteFr($"Frammenti {frammentiTemp} => {frammenti}\n");
        }
    }
}
