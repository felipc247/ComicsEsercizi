using Simple_RTS.Combattimento;
using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Eroi.TipDifesa
{
    internal class Angelo : Eroe
    {
        private Attacco attacco;

        public Angelo(Attacco attacco) {
            base.nome = GetType().Name;
            base.nome_abilita = "Protezione Angelica";

            base.descrizione = "Un angelo caduto";

            base.descrizione_abilita = $"Le tue Truppe Distanza non possono subire danni questo turno";

            base.descrizione_uso_abilita = $"UwU! Truppe Distanza immuni ai danni questo turno";

            base.costo = 100;
            base.costo_abilita = 100;
            base.tipologia = TT.Difesa;
            base.cooldown = 1;
            base.cooldown_Rimanente = 0;

            this.attacco = attacco; 
        }

        public override void Attiva()
        {
            base.Attiva();
            CC.BlueFr($"{base.nome_abilita} | {base.descrizione_uso_abilita}\n");
            attacco.Protezione_Angelica = true;
        }
    }
}
