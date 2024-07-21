using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Truppe
{
    internal class Arciere : Truppa
    {

        public Arciere()
        {
            base.vita = 50;
            base.atk = 15;
            base.range = 10;
            base.costo = 25;
            base.nome = GetType().Name;
            base.costi_Potenziamento = new int[] { 100, 200, 350, 600 };
            base.costo_Potenziamento = costi_Potenziamento[livello - 1];
            base.conversione_blu = 5;
            base.tipologia = TT.Distanza;
        }



        public override void Potenzia()
        {
            livello = (livello + 1 > 5) ? 5 : livello + 1;

            int vitaTemp = base.vita;
            int atkTemp = base.atk;

            switch (livello)
            {
                case 2:
                    base.vita = 70;
                    base.atk = 35;
                    break;
                case 3:
                    base.vita = 90;
                    base.atk = 50;
                    break;
                case 4:
                    base.vita = 110;
                    base.atk = 75;
                    break;
                case 5:
                    base.vita = 130;
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
