using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricerca_binaria
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Algoritmo che ad ogni ciclo dimezza la ricerca in un vettore ordinato
            // 
            int[] ints = new int[(int) Math.Pow(2, 5)];

            int key = -1;
            int pos = -1; // non trovato, contiene la posizione nel vettore
            // p = pos iniziale di ricerca, u = pos finale di ricerca
            int p = 0, u = ints.Length - 1;
            int m = (u + p) / 2; // pos intermedia che ci permette di dividere la ricerca
            // se vet[m] < key, si ricerca la parte dx del vettore
            // se vet[m] > key, si ricerca la parte sx del vettore
            // se vet[m] = key, numero trovato


            // riempimento casuale crescente vet
            Random ra = new Random();

            key = ra.Next(0, 10);

            for (int j = 0; j < ints.Length; j++)
            {
                // primo ciclo non posso sommare il numero precedente con ra.Next
                // non esistendo l'indice
                if (j != 0)
                {
                    ints[j] = ints[j - 1] + ra.Next(1, 4);
                }
                else
                {
                    ints[j] = ra.Next(1, 4);
                }
                Console.WriteLine($"[{j}] = {ints[j]}");
            }

            int i = 0; // num cicli

            do
            {
                i++;

                if (ints[m] == key)
                {
                    pos = m;
                }
                else if (ints[m] < key)
                {
                    // ricerca parte dx vet
                    // p diventa m + 1, nuovo inizio ricerca vet
                    // u rimane invariato
                    p = m + 1;
                    m += (u - p) / 2;
                    Console.WriteLine($"p = {p}\nu = {u}\nm = {m}");
                }
                else if (ints[m] > key)
                {
                    // ricerca parte sx vet
                    // p rimane invariato
                    // u diventa m - 1, nuova fine ricerca vet
                    u = m - 1;
                    m = p + (u - p) / 2;
                    Console.WriteLine($"p = {p}\nu = {u}\nm = {m}");
                }
                m = (u + p) / 2;
                // numero ciclo
                Console.WriteLine($"Ciclo = {i}");
            } while (p <= u && pos == -1);

            // stampo key
            Console.WriteLine($"Chiave = {key}");

            // non trovato
            if (pos == -1)
            {
                Console.WriteLine("NON trovato");
                Console.WriteLine("Cicli = " + i);
            }
            else
            {
                // trovato
                Console.WriteLine("Trovato, pos = " + pos);
                Console.WriteLine("Cicli = " + i);
            }

        }
    }
}
