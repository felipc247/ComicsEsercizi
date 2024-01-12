using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafi_esempio
{
    internal class Program
    {
        // Esempio di implementazione di BFS e DFS in C#
        public class Grafo
        {
            public int NumeroNodi;
            public List<int>[] Adiacenze;

            public Grafo(int numeroNodi)
            {
                NumeroNodi = numeroNodi;
                Adiacenze = new List<int>[numeroNodi];
                for (int i = 0; i < numeroNodi; ++i)
                    Adiacenze[i] = new List<int>();
            }

            // Metodi di aggiunta di archi e implementazione di BFS e DFS possono essere aggiunti qui
            
        }

        static void Main(string[] args)
        {

        }
    }
}
