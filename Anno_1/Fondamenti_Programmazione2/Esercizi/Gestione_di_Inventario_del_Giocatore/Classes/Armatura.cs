using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Inventario_del_Giocatore
{
    internal class Armatura : Equipaggiabile
    {
        private int difesa;

        public Armatura(double peso, string nome, Tipologia tipo, int difesa) : base(peso, nome, tipo)
        {
            this.difesa = difesa;
        }

        // GET // SET

        public int Difesa { get { return difesa; } }

        // Methods

        public override string ToString()
        {
            return base.ToString() + $", Difesa: {difesa}";
        }
    }
}
