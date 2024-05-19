using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Inventario_del_Giocatore
{
    internal class Arma : Equipaggiabile
    {
        private int danno;

        public Arma(double peso, string nome, Tipologia tipo, int danno) : base(peso, nome, tipo)
        {
            this.danno = danno;
        }

        // GET // SET

        public int Danno { get { return danno; } }

        // Methods

        public override string ToString()
        {
            return base.ToString() + $", Danno: {danno}";
        }

    }
}
