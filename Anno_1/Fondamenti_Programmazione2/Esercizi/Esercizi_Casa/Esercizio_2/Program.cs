using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Banca banca = new Banca();

            banca.Preleva(10000);
            Console.WriteLine("saldo: "+ banca.Get_Saldo());

            banca.Versa(100000);
            Console.WriteLine("saldo: " + banca.Get_Saldo());

            banca.Preleva(10000);
            Console.WriteLine("saldo: " + banca.Get_Saldo());
        }
    }
}
