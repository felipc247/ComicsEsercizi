using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmica_00_2
{
    internal class Program
    {
        static void Bubble_Sort(int[] vet)
        {
            for (int i = 0; i < vet.Length - 1; i++)
            {
                // Confronto j con il successivo, se j > j + 1
                // allora scambio
                // j++
                // e continuo il confronto
                // questo crea un subarray che contiene gli elementi
                // più grandi già ordinati
                for (int j = 0; j < vet.Length - i - 1; j++)
                {
                    if (vet[j] > vet[j + 1])
                    {
                        int temp = vet[j];
                        vet[j] = vet[j + 1];
                        vet[j + 1] = temp;
                    }
                }
            }
        }

        static void Insertion_Sort(int[] vet)
        {
            for (int i = 0; i < vet.Length - 1; i++)
            {
                // creo un subarray di elementi ordinati
                // sposto l'elemento (j + 1) key al posto di j
                // se minore, altrimenti esco perchè l'elemento
                // è nella posizione corretta
                // se minore continuo a controllare la key
                // con l'elemento che la precede
                for (int j = i; j >= 0; j--)
                {
                    if (vet[j + 1] < vet[j])
                    {
                        int temp = vet[j];
                        vet[j] = vet[j + 1];
                        vet[j + 1] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        static void Insertion_Sort(List<int> vet)
        {
            for (int i = 0; i < vet.Count - 1; i++)
            {
                // creo un subarray di elementi ordinati
                // sposto l'elemento (j + 1) key al posto di j
                // se minore, altrimenti esco perchè l'elemento
                // è nella posizione corretta
                // se minore continuo a controllare la key
                // con l'elemento che la precede
                for (int j = i; j >= 0; j--)
                {
                    if (vet[j + 1] < vet[j])
                    {
                        int temp = vet[j];
                        vet[j] = vet[j + 1];
                        vet[j + 1] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }


        static List<int> Merge_Sort(List<int> vet)
        {
            // divido il vettore in 2
            // e continuo fino a che la dimensione del sottovettore è < 3
            // swappo se vet[0] > vet[1]
            // merge dei sottovettori ordinati e li ordino
            if (vet.Count > 2)
            {
                List<int> thread1 = Merge_Sort(vet.GetRange(0, vet.Count / 2));
                List<int> thread2 = Merge_Sort(vet.GetRange(vet.Count / 2, vet.Count - (vet.Count / 2)));
                thread1.AddRange(thread2);
                Insertion_Sort(thread1);
                return thread1;
            }
            else
            {
                if (vet.Count > 1)
                {
                    if (vet[0] > vet[1])
                    {
                        int temp = vet[0];
                        vet[0] = vet[1];
                        vet[1] = temp;
                    }
                }
                return vet;
            }

        }

        static void Quick_Sort(int[] vet, int sx, int dx)
        {
            // Si sceglie un pivot vet[0]
            // si controllano tutti gli altri elementi
            // trovo quelli < e quelli >= a pivot
            // inserisco pivot alla posizione corretta
            // ripeto il processo con i due subarray
            // finché non ordinato

            if (!vet.Any() || vet.Length < 2) return;

            // Array di supporto per salvare i dati senza modificare l'array originale
            int[] vet_spt = new int[dx - sx - 1];

            int pivot = vet[sx];
            int minori_Pivot = 0;

            for (int i = sx + 1; i < dx; i++)
            {
                if (vet[i] < pivot)
                {
                    vet_spt[minori_Pivot] = vet[i];
                    minori_Pivot++;
                }
            }

            int maggiori = 0;
            for (int i = sx + 1; i < dx; i++)
            {
                if (vet[i] >= pivot)
                {
                    maggiori++;
                    vet_spt[minori_Pivot + maggiori] = vet[i];
                }
            }

            // inserisco Pivot nella posizione corretta
            vet[sx + minori_Pivot] = pivot;

            // se Pivot numero minore, non ho un vet SX
            if (minori_Pivot != 0)
            {
                int j = 0;
                for (int i = sx; i < minori_Pivot; i++)
                {
                    vet[i] = vet_spt[j++];
                }
                // passo vet SX
                Quick_Sort(vet, sx, minori_Pivot);
            }

            // se Pivot numero maggiore, non ho un vet DX
            if (minori_Pivot != vet.Length - 1)
            {
                int j = 0;
                for (int i = sx + minori_Pivot + 1; i < dx; i++)
                {
                    vet[i] = vet_spt[minori_Pivot + j++];
                }
                // passo vet DX
                Quick_Sort(vet, minori_Pivot + 1, vet.Length);
            }

        }

        static void Print_Int_Vet(int[] vet)
        {
            for (int i = 0; i < vet.Length; i++)
            {
                Console.WriteLine(vet[i]);
            }
        }

        static void Main(string[] args)
        {
            int[] vet = new int[10];
            Random random = new Random();

            for (int i = 0; i < vet.Length; i++)
            {
                vet[i] = random.Next(1, 15); // Assegna un numero casuale compreso tra 1 e 100
            }
            DateTime start = DateTime.Now;
            Quick_Sort(vet, 0, vet.Length);
            //vet = Merge_Sort(vet.ToList()).ToArray();
            DateTime end = DateTime.Now;
            Print_Int_Vet(vet);
            Console.WriteLine(end - start);

        }
    }
}
