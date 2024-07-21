using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Eroi
{
    internal class Eroe
    {
        protected String nome;
        protected String descrizione;
        protected String nome_abilita;
        protected String descrizione_abilita;
        protected String descrizione_uso_abilita;
        protected int costo;
        protected int costo_abilita;
        protected int cooldown;
        protected int cooldown_Rimanente;
        protected TT tipologia;
        protected int stato = -1; // 0 = disponibile, 1 = schierato, 2 = imprigionato

        public enum TT
        {
            Attacco,
            Difesa
        }

        public Eroe() { }

        // GET // SET
        public String Nome { get { return nome; } }

        public String Descrizione { get { return descrizione; } }

        public String Nome_Abilita { get { return nome_abilita; } }

        public String Descrizione_Abilita { get { return descrizione_abilita; } }

        public String Descrizione_Uso_Abilita { get { return descrizione_uso_abilita; } }

        public int Costo { get { return costo; } }

        public int Costo_Abilita { get { return costo_abilita; } }

        public int Cooldown { get { return cooldown; } }

        public int Cooldown_Rimanente { get { return cooldown_Rimanente; } }

        public TT Tipologia { get { return tipologia; } }

        public bool Evocato { get { return (stato != -1); } }

        public int Stato { get { return stato; } }

        // METODI

        public virtual void Attiva()
        {
            CC.MagentaFr($"{this.nome}"); CC.WhiteFr(" | Attivazione abilita'\n");
            cooldown_Rimanente = cooldown;
        }

        public void Evoca()
        {
            Disponibile();
        }

        public void CoolDownAbilita()
        {
            cooldown_Rimanente = (cooldown_Rimanente - 1 < 0) ? 0 : cooldown_Rimanente - 1;
        }

        public void Disponibile()
        {
            stato = 0;
        }

        public void Schierato()
        {
            stato = 1;
        }

        public void Imprigionato()
        {
            stato = 2;
        }
    }
}
