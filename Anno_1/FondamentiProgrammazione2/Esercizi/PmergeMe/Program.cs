using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmergeMe
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

        static void Insertion_Sort(Queue<int> vet)
        {
            Queue<int> ordered = new Queue<int>();
            Queue<int> support = new Queue<int>();

            for (int i = 0; i < vet.Count - 1; i++)
            {
                // creo un subarray di elementi ordinati
                // sposto l'elemento (j + 1) key al posto di j
                // se minore, altrimenti esco perchè l'elemento
                // è nella posizione corretta
                // se minore continuo a controllare la key
                // con l'elemento che la precede

                int n1 = vet.Dequeue();
                int n2;
                if (ordered.Count == 0)
                {
                    n2 = vet.Dequeue();
                }
                else
                {
                    support.Clear();
                    n2 = ordered.Peek();
                }

                do
                {
                    if (n2 < n1)
                    {
                        int temp = n1;
                        n1 = n2;
                        n2 = n1;
                    }

                } while (true);

                int ordered_length = ordered.Count;

                for (int j = 0; j < ordered_length; j++)
                {
                    support.Enqueue(ordered.Dequeue());
                    if (n2 < n1)
                    {
                        int temp = n1;
                        n1 = n2;
                        n2 = temp;

                    }
                    else
                    {
                        break;
                    }
                }
            }
        }


        static List<int> Merge_Insertion_Sort(List<int> vet, int K)
        {
            // se non ci sono solo 2 elementi
            if (vet.Count > 2)
            {
                // Divido la Lista in K sottoliste
                List<List<int>> Lists = new List<List<int>>();
                int group_size = vet.Count / K;
                for (int i = 0; i < K; i++)
                {
                    if (i == 0)
                    {
                        Lists.Add(vet.GetRange(0, group_size));
                        continue;
                    }
                    if (i == K - 1)
                    {
                        Lists.Add(vet.GetRange(i * group_size, group_size + (vet.Count - (i + 1) * group_size)));
                    }
                    else
                    {
                        Lists.Add(vet.GetRange(i * group_size, group_size));
                    }

                }

                // Ordino ogni sublist con insertion sort
                for (int i = 0; i < Lists.Count; i++)
                {
                    Insertion_Sort(Lists[i]);
                }

                // Merge e ordinamento, fino a che la dimensione di Lists è 1

                do
                {
                    // Merge delle sublist

                    int list_length = Lists.Count;
                    if (Lists.Count % 2 == 0)
                    {
                        for (int i = 0; true; i++)
                        {
                            Lists[i].AddRange(Lists[i + 1]);
                            Lists.RemoveAt(i + 1);
                            if (Lists.Count == list_length / 2)
                                break;
                        }
                    }
                    else
                    {
                        for (int i = 0; true; i++)
                        {
                            if (Lists.Count == list_length / 2 + 1)
                            {
                                break;
                            }
                            else
                            {
                                Lists[i].AddRange(Lists[i + 1]);
                                Lists.RemoveAt(i + 1);
                            }
                        }
                    }

                    // Ordino ogni sublist con insertion sort
                    for (int i = 0; i < Lists.Count; i++)
                    {
                        Insertion_Sort(Lists[i]);
                    }

                } while (Lists.Count != 1);

                //List<int> thread1 = Merge_Sort(vet.GetRange(0, vet.Count / 2), K);
                //List<int> thread2 = Merge_Sort(vet.GetRange(vet.Count / 2, vet.Count - (vet.Count / 2)), K);
                //thread1.AddRange(thread2);
                //Insertion_Sort(thread1);
                return Lists[0];
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

        //static Queue<int> Merge_Insertion_Sort(Queue<int> vet, int K)
        //{
        //    // se non ci sono solo 2 elementi
        //    if (vet.Count > 2)
        //    {
        //        // Divido la Lista in K sottoliste
        //        Queue<List<int>> queues = new Queue<List<int>>();
        //        int group_size = vet.Count / K;
        //        for (int i = 0; i < K; i++)
        //        {
        //            Queue<int> queue = new Queue<int>();
        //            if (i == 0)
        //            {
        //                for (int j = 0; j < group_size; j++)
        //                {
        //                    queue.Enqueue(vet.Dequeue());
        //                }
        //                continue;
        //            }
        //            if (i == K - 1)
        //            {
        //                for (int j = 0; j < group_size + (vet.Count - (i + 1) * group_size); j++)
        //                {
        //                    queue.Enqueue(vet.Dequeue());
        //                }
        //            }
        //            else
        //            {
        //                for (int j = 0; j < group_size; j++)
        //                {
        //                    queue.Enqueue(vet.Dequeue());
        //                }
        //            }

        //        }

        //        // Ordino ogni sublist con insertion sort
        //        for (int i = 0; i < queues.Count; i++)
        //        {
        //            Insertion_Sort(queues[i]);
        //        }

        //        // Merge e ordinamento, fino a che la dimensione di Lists è 1

        //        do
        //        {
        //            // Merge delle sublist

        //            int list_length = queues.Count;
        //            if (queues.Count % 2 == 0)
        //            {
        //                for (int i = 0; true; i++)
        //                {
        //                    queues[i].AddRange(queues[i + 1]);
        //                    queues.RemoveAt(i + 1);
        //                    if (queues.Count == list_length / 2)
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                for (int i = 0; true; i++)
        //                {
        //                    if (queues.Count == list_length / 2 + 1)
        //                    {
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        queues[i].AddRange(queues[i + 1]);
        //                        queues.RemoveAt(i + 1);
        //                    }
        //                }
        //            }

        //            // Ordino ogni sublist con insertion sort
        //            for (int i = 0; i < queues.Count; i++)
        //            {
        //                Insertion_Sort(queues[i]);
        //            }

        //        } while (queues.Count != 1);

        //        //List<int> thread1 = Merge_Sort(vet.GetRange(0, vet.Count / 2), K);
        //        //List<int> thread2 = Merge_Sort(vet.GetRange(vet.Count / 2, vet.Count - (vet.Count / 2)), K);
        //        //thread1.AddRange(thread2);
        //        //Insertion_Sort(thread1);
        //        return queues[0];
        //    }
        //    else
        //    {
        //        if (vet.Count > 1)
        //        {
        //            if (vet[0] > vet[1])
        //            {
        //                int temp = vet[0];
        //                vet[0] = vet[1];
        //                vet[1] = temp;
        //            }
        //        }
        //        return vet;
        //    }

        //}

        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            Random ra = new Random();
            //for (int i = 0; i < 3000; i++)
            //{
            //    list.Add(ra.Next(1, 30000));
            //}

            list.Add(10);
            list.Add(5);
            list.Add(1);

            DateTime start_lists = DateTime.Now;
            list = Merge_Insertion_Sort(list, 3000);
            DateTime end_lists = DateTime.Now;

            Console.WriteLine("Liste: " + (end_lists - start_lists));

            //for (int i = 0; i < list.Count; i++)
            //{
            //    Console.WriteLine(list[i]);
            //}
        }
    }
}
