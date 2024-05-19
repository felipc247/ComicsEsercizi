using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_2
{
    internal class Banca
    {
        private double saldo;

        public Banca()
        {
            saldo = 0;
        }

        // GET // SET

        // Methods

        public double Get_Saldo() {
            return saldo;
        }

        public void Versa(double value)
        {
            value = Math.Abs(value);
            saldo += value;
        }

        public void Preleva(double value)
        {
            value = -Math.Abs(value);
            saldo += (saldo + value >= 0) ? value : 0;
        }
    }
}
