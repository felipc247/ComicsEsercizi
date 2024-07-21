using Simple_RTS.Base;
using Simple_RTS.Truppe;
using Simple_RTS.Utilities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Truppe_Elite.Guerra
{
    internal class Bombardiere_Pazzo : Truppa_Elite_Guerra
    {

        private Torre torre;
        private int dannoTorre;

        public Bombardiere_Pazzo(Torre torre) { 
            base.nome = GetType().Name;
            base.nome_abilita = "Bombardamento Tattico";
            
            this.dannoTorre = 2000;

            base.descrizione = "Un preciso e letale aviatore, ha distrutto con successo più di 100 obiettivi\n" +
            "ma tutte quelle esplosioni gli hanno dato un po' alla testa...";

            base.descrizione_abilita = "Vola sopra le linee nemiche e riduce la vita della Torre avversaria di 2000!";

            base.descrizione_uso_abilita = $"Boom! {dannoTorre} danni inflitti alla Torre avversaria!";

            base.costo = 3000;
            base.costo_abilita = 2000;
            base.tipologia = TT.Guerra;
            base.tipologia_Guerra = TT_Guerra.Distanza;
            base.cooldown = 3;
            base.cooldown_Rimanente = 0;

            this.torre = torre;
        }

        public override void Attiva()
        {
            base.Attiva();
            CC.RedFr($"{base.nome_abilita} | {base.descrizione_uso_abilita}\n");
            torre.Danneggia(this.dannoTorre);
        }
    }
}
