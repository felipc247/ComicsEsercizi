using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Inventario_del_Giocatore
{
    internal abstract class Equipaggiabile : Oggetto
    {
        private Tipologia tipo;

        public enum Tipologia { 
            arma,
            armatura
        }

        public Equipaggiabile(double peso, string nome, Tipologia tipo) : base(peso, nome)
        {
            this.tipo = tipo;
        }

        // GET // SET
        public Tipologia Tipo{ get { return tipo; } }

        // Methods

        public override string ToString()
        {
            return base.ToString() + $", Tipo: {tipo}";
        }
    }
}
