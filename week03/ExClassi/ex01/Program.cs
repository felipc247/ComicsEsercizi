using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Libro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Libro libro = new Libro("Spy wars", "Anya Forger", 2023);

            libro.VisualizzaInfo();

        }
    }
}
