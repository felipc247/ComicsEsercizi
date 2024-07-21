using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Truppe
{
    internal class Spadaccino : Truppa
    {

        public Spadaccino()
        {
            base.vita = 150;
            base.atk = 30;
            base.range = 3;
            base.costo = 60;
            base.nome = GetType().Name;
            base.costi_Potenziamento = new int[] { 200, 400, 700, 1000 };
            base.costo_Potenziamento = costi_Potenziamento[livello - 1];
            base.conversione_blu = 10;
            base.tipologia = TT.Mischia;
        }



        public override void Potenzia()
        {
            livello = (livello + 1 > 5) ? 5 : livello + 1;

            int vitaTemp = base.vita;
            int atkTemp = base.atk;

            switch (livello)
            {
                case 2:
                    base.vita = 200;
                    base.atk = 35;
                    break;
                case 3:
                    base.vita = 250;
                    base.atk = 50;
                    break;
                case 4:
                    base.vita = 350;
                    base.atk = 75;
                    break;
                case 5:
                    base.vita = 500;
                    base.atk = 125;
                    break;
            }

            base.costo_Potenziamento = (livello < 5) ? costi_Potenziamento[livello - 1] : costi_Potenziamento[costi_Potenziamento.Length - 1];


            CC.GreenFr($"Successo!\n");
            CC.WhiteFr($"Vita {vitaTemp} => {base.vita}\n");
            CC.WhiteFr($"ATK {atkTemp} => {base.atk}\n");
        }
    }
}
