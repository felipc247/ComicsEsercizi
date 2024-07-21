using Simple_RTS.Base;
using Simple_RTS.Truppe;
using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Truppe_Elite.Guerra.Mischia
{
    internal class Vex : Truppa_Elite_Guerra
    {
        private List<Truppa> truppe_Mischia;
        private int truppeUccise = 0;

        public Vex(List<Truppa> truppe_Mischia)
        {
            base.nome = GetType().Name;
            base.nome_abilita = "Vortice di Asce";

            base.descrizione = "Un boscaiolo professionista, cerca vendetta da quando la sua famiglia è stata uccisa";

            base.descrizione_abilita = "Falcia il 30% delle Truppe Mischia schierate avversarie";

            base.descrizione_uso_abilita = $"Cccc! Tremate davanti alla mia Ascia!";

            base.costo = 2000;
            base.costo_abilita = 3000;
            base.tipologia = TT.Guerra;
            base.tipologia_Guerra = TT_Guerra.Mischia;
            base.cooldown = 3;
            base.cooldown_Rimanente = 0;

            this.truppe_Mischia = truppe_Mischia;
        }

        public override void Attiva()
        {
            base.Attiva();
            truppeUccise = 0; 
            foreach (var truppa in truppe_Mischia)
            {
                int tempQuantita = truppa.Schierate;
                truppa.Schierate = -(int)(truppa.Schierate * 0.3);
                truppeUccise += tempQuantita - truppa.Schierate;
            }
            descrizione_uso_abilita += $" {-truppeUccise} Truppe Mischia Uccise";
            CC.RedFr($"{base.nome_abilita} | {base.descrizione_uso_abilita}\n");

        }
    }
}
