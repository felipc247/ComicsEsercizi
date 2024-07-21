using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Truppe
{

    internal class Mammut : Truppa
    {

        public Mammut()
        {
            base.vita = 1500;
            base.atk = 30;
            base.range = 1;
            base.costo = 100;
            base.nome = GetType().Name;
            base.costi_Potenziamento = new int[] { 200, 400, 700, 1000 };
            base.costo_Potenziamento = costi_Potenziamento[livello - 1];
            base.conversione_blu = 20;
            base.tipologia = TT.Tank;
        }



        public override void Potenzia()
        {
            livello = (livello + 1 > 5) ? 5 : livello + 1;

            int vitaTemp = base.vita;
            int atkTemp = base.atk;

            switch (livello)
            {
                case 2:
                    base.vita = 1750;
                    base.atk = 35;
                    break;
                case 3:
                    base.vita = 2100;
                    base.atk = 50;
                    break;
                case 4:
                    base.vita = 2500;
                    base.atk = 75;
                    break;
                case 5:
                    base.vita = 3000;
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
