using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Studente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Studente studente = new Studente("Umaru","Himouto", 901);
            studente.Studiare();
            studente.VisualizzaDati();
        }
    }
}
