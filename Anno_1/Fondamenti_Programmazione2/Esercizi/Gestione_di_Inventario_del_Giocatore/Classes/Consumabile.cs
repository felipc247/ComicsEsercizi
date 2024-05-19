using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Inventario_del_Giocatore
{
    internal class Consumabile : Oggetto
    {
        private int times_usable = 0;

        public Consumabile(double peso, string nome, int times_usable) : base(peso, nome)
        {
            this.times_usable = (times_usable > 0) ? times_usable : this.times_usable;
        }

        // GET // SET

        public int Times_usable { get { return times_usable; } }

        // Methods

        public void Use() {

            if (times_usable - 1 < 0)
            {
                Console.WriteLine("Non hai più usi disponibili");
            }
            else
            {
                times_usable--;
                Console.WriteLine($"Usi -1, Usi rimanenti {times_usable}");
            }

        }

        public override string ToString()
        {
            return base.ToString() + $", Usi rimanenti: {times_usable}";
        }
    }
}
