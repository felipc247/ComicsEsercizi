using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simple_RTS.Truppe.Truppa;

namespace Simple_RTS.Truppe
{
    internal class Truppa_Elite
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
        protected bool assoldata = false;

        public enum TT
        {
            Supporto,
            Guerra
        }

        public Truppa_Elite() { }

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

        public bool Assoldata { get { return assoldata; } }

        // METODI

        public virtual void Attiva() { 
            CC.MagentaFr($"{this.nome}"); CC.WhiteFr(" | Attivazione abilita'\n");
            cooldown_Rimanente = cooldown;
        }

        public void Assolda() { 
            assoldata = true;
        }

        public void CoolDownAbilita() {
            cooldown_Rimanente = (cooldown_Rimanente - 1 < 0) ? 0 : cooldown_Rimanente - 1;
        }
    }
}
