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

            // Aggiunta arco
            public void Add_Arch(int a, int b)
            {
                try
                {
                    Adiacenze[a].Add(b);
                    Adiacenze[b].Add(a);
                }
                catch (Exception)
                {
                    Console.WriteLine("Nodo out of bounds");
                }
            }

            // DFS
            public void DFS(bool[] visited, int nodo)
            {
                // va in profondità fino a quando è possibile
                // se il nodo figlio ha nipoti va direttamente a quelli
                Console.WriteLine(nodo);

                // Ciclo i figli del nodo corrente
                for (int i = 0; i < Adiacenze[nodo].Count; i++)
                {
                    int nodo_i = Adiacenze[nodo][i];
                    // se il nodo non è già stato visitato
                    if (!visited[nodo_i])
                    {
                        // imposto a visitato
                        // e lo aggiungo alla queue
                        visited[nodo_i] = true;
                        DFS(visited, nodo_i);

                    }
                }

            }

            // BFS
            public void BFS(bool[] visited, Queue<int> queue)
            {
                // visito prima i figli e poi i nipoti
                // sfrutto una queue per "mantenere" l'ordine
                // di visita
                int now_Node = queue.Dequeue();
                Console.WriteLine(now_Node);

                for (int i = 0; i < Adiacenze[now_Node].Count; i++)
                {
                    int nodo_i = Adiacenze[now_Node][i];
                    if (!visited[nodo_i])
                    {
                        visited[nodo_i] = true;
                        queue.Enqueue(nodo_i);

                    }
                }
                // se ci sono altri nodi da visitare si continua
                if (queue.Any()) BFS(visited, queue);

            }


        }

        static void Main(string[] args)
        {
            Grafo grafo = new Grafo(10);
            Random ra = new Random();
            grafo.Add_Arch(0, 1);
            grafo.Add_Arch(0, 2);
            grafo.Add_Arch(0, 7);

            grafo.Add_Arch(1, 4);
            grafo.Add_Arch(2, 4);
            grafo.Add_Arch(2, 3);
            grafo.Add_Arch(7, 6);
            grafo.Add_Arch(3, 5);
            grafo.Add_Arch(3, 6);

            // DFS
            int radice = 0;
            bool[] visited_DFS = new bool[grafo.Adiacenze.Length];
            visited_DFS[radice] = true;
            grafo.DFS(visited_DFS, radice);
            // line
            Console.WriteLine("--------------------------------------");
            // BFS
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(radice);
            bool[] visited_BFS = new bool[grafo.Adiacenze.Length];
            visited_BFS[radice] = true;
            grafo.BFS(visited_BFS, queue);
        }
    }
}
