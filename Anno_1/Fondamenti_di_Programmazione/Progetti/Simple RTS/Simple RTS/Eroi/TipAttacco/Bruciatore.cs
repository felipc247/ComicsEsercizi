using Simple_RTS.Combattimento;
using Simple_RTS.Truppe;
using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Eroi.TipAttacco
{
    internal class Bruciatore : Eroe
    {
        private int danniPerMovimento = 50;
        private int numMovimenti = 5;
        private Attacco attacco; 

        public Bruciatore(Attacco attacco) {
            base.nome = GetType().Name;
            base.nome_abilita = "Pioggia di Fuoco";

            base.descrizione = "Un dio della distruzione nato dai fuochi ardenti del nostro Sole\n" +
                "Brucia ogni cosa in mezzo al suo cammino";

            base.descrizione_abilita = $"Evoca un enorme nube di fuoco e magma\n" +
                $"che provoca {danniPerMovimento} danni ad ogni Truppa per Movimento ({numMovimenti})";

            base.descrizione_uso_abilita = $"BRUCIAHAHA! {danniPerMovimento} per movimento alle Truppe avversarie";

            base.costo = 100;
            base.costo_abilita = 500;
            base.tipologia = TT.Attacco;
            base.cooldown = 3;
            base.cooldown_Rimanente = 0;
            
            this.attacco = attacco;
        }

        public int Danni_Per_Movimento { get { return danniPerMovimento; } }

        public override void Attiva()
        {
            base.Attiva();
            CC.BlueFr($"{base.nome_abilita} | {base.descrizione_uso_abilita}\n");
            attacco.Movimenti_Rimanenti = numMovimenti;
        }
    }
}
