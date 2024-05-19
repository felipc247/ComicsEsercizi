using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Veicolo veicolo = new Veicolo("Bond", "Yz$");
            Veicolo veicolo1 = new Veicolo("Peanut", "Xw0");
            Veicolo veicolo2 = new Veicolo("Berlint", "Mk3");

            Auto auto = new Auto("Xeno", "Gu£", 3);
            Auto auto1 = new Auto("Peanut", "Xw0", 5);
            Auto auto2 = new Auto("Merry", "Az>", 7);

            Moto moto = new Moto("Berlint", "Lu$", "Black");
            Moto moto1 = new Moto("Bond", "Yn&", "White");
            Moto moto2 = new Moto("Kame", "Wq╞", "Magenta");

            Console.WriteLine(veicolo.Descrizione());
            Console.WriteLine(veicolo1.Descrizione());
            Console.WriteLine(veicolo2.Descrizione());

            Console.WriteLine(auto.Descrizione());
            Console.WriteLine(auto1.Descrizione());
            Console.WriteLine(auto2.Descrizione());

            Console.WriteLine(moto.Descrizione());
            Console.WriteLine(moto1.Descrizione());
            Console.WriteLine(moto2.Descrizione());
        }
    }
}
