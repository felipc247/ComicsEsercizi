using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Truppe
{

    internal class Soldato_Semplice : Truppa
    {

        public Soldato_Semplice()
        {
            base.vita = 50;
            base.atk = 20;
            base.range = 2;
            base.costo = 10;
            base.nome = GetType().Name;
            base.costi_Potenziamento = new int[] { 100, 200, 350, 600 };
            base.costo_Potenziamento = costi_Potenziamento[livello - 1];
            base.conversione_blu = 5;
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
                    base.vita = 120;
                    base.atk = 25;
                    break;
                case 3:
                    base.vita = 150;
                    base.atk = 30;
                    break;
                case 4:
                    base.vita = 175;
                    base.atk = 40;
                    break;
                case 5:
                    base.vita = 250;
                    base.atk = 50;
                    break;
            }


            base.costo_Potenziamento = (livello < 5) ? costi_Potenziamento[livello - 1] : costi_Potenziamento[costi_Potenziamento.Length - 1];

            CC.GreenFr($"Successo!\n");
            CC.WhiteFr($"Vita {vitaTemp} => {base.vita}\n");
            CC.WhiteFr($"ATK {atkTemp} => {base.atk}\n");
        }
    }
}
