using Simple_RTS.Base;
using Simple_RTS.Combattimento;
using Simple_RTS.Truppe;
using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Truppe_Elite.Guerra.Tank
{
    internal class Colosso : Truppa_Elite_Guerra
    {
        private int vita_scudo = 3000;
        private Attacco attacco;
        private Giocatore giocatore;

        public Colosso(Attacco attacco, Giocatore giocatore)
        {
            base.nome = GetType().Name;
            base.nome_abilita = "Scudo di Ghiaccio";

            base.descrizione = "Un gigante nato nei freddi ghiacchi nordici, ogni nemico che ha affrontato" +
                "è letteralmente caduto ai suoi piedi";

            base.descrizione_abilita = $"Evoca un enorme scudo ghiacciato che ti ripara da {vita_scudo} danni";

            base.descrizione_uso_abilita = $"Uno scudo ghiacciato ti protegge";

            base.costo = 3000;
            base.costo_abilita = 1000;
            base.tipologia = TT.Guerra;
            base.tipologia_Guerra = TT_Guerra.Tank;
            base.cooldown = 2;
            base.cooldown_Rimanente = 0;

            this.attacco = attacco;
            this.giocatore = giocatore;
        }

        public override void Attiva()
        {
            base.Attiva();
            CC.BlueFr($"{base.nome_abilita} | {base.descrizione_uso_abilita}\n");

            if (giocatore.Nome.Equals(attacco.Giocatore1.Nome))
            {
                attacco.Vita_Scudo1 = +vita_scudo;
            }
            else
            {
                attacco.Vita_Scudo2 = +vita_scudo;
            }
                
        }


    }
}
