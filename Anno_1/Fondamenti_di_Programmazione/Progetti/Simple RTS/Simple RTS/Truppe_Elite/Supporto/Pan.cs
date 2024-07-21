using Simple_RTS.Base;
using Simple_RTS.Truppe;
using Simple_RTS.Utilities;

namespace Simple_RTS.Truppe_Elite.Supporto
{
    internal class Pan : Truppa_Elite
    {
        private int bonus_Luce_BLU = 50;
        private bool abilita_attiva = false;
        private Altare altare;

        public Pan(Altare altare)
        {
            base.nome = GetType().Name;
            base.nome_abilita = "Benedizione";

            base.descrizione = "Una ragazza misteriosa le cui origini sono ignote\n" +
                "sembra avere dei legami divini";

            base.descrizione_abilita = $"Una strana danza che sembra avere il favore divino, +{bonus_Luce_BLU} Luce Blu nel tuo turno";

            base.descrizione_uso_abilita = $"AHOHUH! Ottieni un bonus permanente di {bonus_Luce_BLU} Luce Blu";

            base.costo = 5000;
            base.costo_abilita = 0;
            base.tipologia = TT.Supporto;
            base.cooldown = 0;
            base.cooldown_Rimanente = 0;

            this.altare = altare;
        }

        public override void Attiva()
        {
            if (!abilita_attiva)
            {
                base.Attiva();
                CC.BlueFr($"{base.nome_abilita} | {base.descrizione_uso_abilita}\n");
                altare.Produzione_Bonus = bonus_Luce_BLU;
                abilita_attiva = true;
            }
            else
            {
                CC.GreenFr($"Abilità già attiva, non puoi attivare {nome_abilita} più di una volta\n");
            }
        }
    }
}
