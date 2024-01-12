using System;
using System.Collections.Generic;
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
													// divido il vettore in
            // e continuo fino a che la dimensione del sottovettore è < 3
            // merge dei sottovettore ordinati e li ordino
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

        static List<int> Quick_Sort(List<int> vet)
        {

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

        static void Print_Int_Vet(int[] vet)
        {
            for (int i = 0; i < vet.Length; i++)
            {
                Console.WriteLine(vet[i]);
            }
        }

        static void Main(string[] args)
        {
            int[] vet = new int[1000000];
            Random random = new Random();

            for (int i = 0; i < vet.Length; i++)
            {
                vet[i] = random.Next(1, 100000); // Assegna un numero casuale compreso tra 1 e 100
            }
            DateTime start = DateTime.Now;
            Insertion_Sort(vet);
            //vet = Merge_Sort(vet.ToList()).ToArray();
            DateTime end = DateTime.Now;
            Print_Int_Vet(vet);
            Console.WriteLine(end - start);

        }
    }
}
