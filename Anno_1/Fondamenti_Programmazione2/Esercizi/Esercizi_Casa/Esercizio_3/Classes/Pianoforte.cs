using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_3
{
    internal class Pianoforte : ISuonabile
    {
        public Pianoforte() { }

        public void Suona()
        {
            Console.WriteLine("La di da");
        }
    }
}
