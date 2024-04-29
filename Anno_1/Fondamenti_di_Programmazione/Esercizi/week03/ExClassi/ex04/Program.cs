using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca("Strix");
            Libro libro = new Libro("Vol.1", "Anya forger", 2023);
            Libro libro1 = new Libro("Vol.2", "Anya forger", 2023);
            Libro libro2 = new Libro("Vol.3", "Anya forger", 2023);

            biblioteca.AggiungiLibro(libro);
            biblioteca.AggiungiLibro(libro1);
            biblioteca.AggiungiLibro(libro2);

            biblioteca.VisualizzaCatalogo();

            if (biblioteca.CercaLibro("Vol.1"))
            {
                // libro trovato
                Console.WriteLine("Waku waku");
            }
            else
            {
                // libro non trovato
                Console.WriteLine("Duck");
            }

        }
    }
}
