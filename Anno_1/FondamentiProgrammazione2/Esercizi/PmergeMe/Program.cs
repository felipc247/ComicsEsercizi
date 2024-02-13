using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmergeMe
{
    internal class Program
    {
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

        static Queue<int> Insertion_Sort(Queue<int> vet)
        {
            if (vet.Count == 1) return vet;

            // contiene la subqueue ordinata, alla fine conterrà tutti gli elementi ordinati
            Queue<int> ordered = new Queue<int>();
            // mantiene l'ordine dei numeri contenuti in ordered nel momento in cui uso dequeue per fare 
            // il confronto tra n1 e n2
            Queue<int> support = new Queue<int>();

            int vet_length = vet.Count;

            for (int i = 0; i < vet_length - 1; i++)
            {
                // creo una subqueue di elementi ordinati
                // confronto il primo elemento di vet
                // con gli elementi in ordered
                // quando trovo la sua posizione
                // ricostruisco tramite support 
                // ordered con n1 nella posizione corretta

                // al primo giro ordered è vuoto, perciò devo prendere
                // 2 elementi da vet invece che 1
                int n1 = vet.Dequeue();
                int n2;
                if (ordered.Count == 0)
                {
                    n2 = vet.Dequeue();
                    ordered.Enqueue(n2);
                }
                else
                {
                    support.Enqueue(ordered.Peek());
                    n2 = ordered.Dequeue();
                }

                // mantengo la size di ordered
                int ordered_length = ordered.Count;
                // posizione di n1 nel subarray
                int index = -1;

                // Trovo la posizione di n1 nel subarray ordinato

                // devo fare un giro in più in quanto
                // altrimenti un numero rimarebbe sempre in ordered

                // e.g. 5 numeri in ordered
                // 1 viene rimosso precedentemente
                // perciò 4
                // all'ultimo giro, se non è stato trovato nulla prima
                // index diventa automaticamente ordered_length
                // però non avviene il confronto con l'ultimo numero rimasto in ordered


                for (int j = 0; j < ordered_length + 1; j++)
                {
                    if (n1 < n2)
                    {
                        // se siamo all'ultimo giro, index non può essere j
                        // poiché noi facciamo un giro in più e questo causerebbe
                        // il dequeue in support di un numero che non c'è
                        index = (j == ordered_length + 1) ? j - 1 : j;
                        break;
                    }

                    // all'ultimo ciclo non ci sono più elementi in ordered
                    if (j == ordered_length)
                    {
                        // n1 è il numero più grande nel subarray ordinato
                        index = ordered_length + 1;
                    }
                    else
                    {
                        // n2 diventa il prossimo numero in ordered
                        // salvo il numero in support
                        support.Enqueue(ordered.Peek());
                        n2 = ordered.Dequeue();
                    }
                }

                // finisco di svuotare ordered in support se ci sono
                // elementi rimanenti

                int ordered_length_left = ordered.Count;
                for (int j = 0; j < ordered_length_left; j++)
                {
                    support.Enqueue(ordered.Dequeue());
                }

                // ricostruisco ordered con l'aggiunta di n1

                int support_length = support.Count;

                // se n1 è più grande di tutti i numeri in ordered
                // allora lo posizionerò alla sua fine
                // uso support_length in quanto rappresenta
                // sempre il numero di elementi che saranno presenti
                // alla fine di ogni ciclo, in genere support_length = ordered_length + 1
                // ma al primo giro sono uguali
                // perciò usare ordered_length + 1 porterebbe ad un'eccezione
                // di coda vuota

                index = (index == ordered_length + 1) ? support_length : index;

                for (int j = 0; j < support_length + 1; j++)
                {
                    if (j == index)
                        ordered.Enqueue(n1);
                    else
                        ordered.Enqueue(support.Dequeue());
                }

            }

            return ordered;
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

        static Queue<int> Merge_Insertion_Sort(Queue<int> vet, int k)
        {
            // se non ci sono solo 2 elementi
            if (vet.Count > 2)
            {
                // divido la queue in k subqueues
                Queue<Queue<int>> queues = new Queue<Queue<int>>();
                int group_size = vet.Count / k;
                int vetCount = vet.Count;
                // inserisco in ogni queue group_size elementi
                // diverso per l'ultima queue
                // se vet.Count % k != 0
                for (int i = 0; i < k; i++)
                {
                    Queue<int> queue = new Queue<int>();
                    if (i == 0)
                    {
                        for (int j = 0; j < group_size; j++)
                        {
                            queue.Enqueue(vet.Dequeue());
                        }
                        queues.Enqueue(queue);
                        continue;
                    }
                    if (i == k - 1)
                    {
                        for (int j = 0; j < group_size + (vetCount - (i + 1) * group_size); j++)
                        {
                            queue.Enqueue(vet.Dequeue());
                        }
                    }
                    else
                    {
                        for (int j = 0; j < group_size; j++)
                        {
                            queue.Enqueue(vet.Dequeue());
                        }
                    }
                    queues.Enqueue(queue);

                }

                // ordino ogni subqueue con insertion sort
                for (int i = 0; i < queues.Count; i++)
                {
                    queues.Enqueue(Insertion_Sort(queues.Dequeue()));
                }

                // merge e ordinamento, fino a che la dimensione di queues è 1

                do
                {
                    // merge delle sublist

                    int queue_length = queues.Count;
                    if (queues.Count % 2 == 0)
                    {
                        for (int i = 0; true; i++)
                        {
                            Queue<int> queue1 = queues.Dequeue();
                            Queue<int> queue2 = queues.Dequeue();

                            // merge delle due queue
                            int queue2_length = queue2.Count;
                            for (int j = 0; j < queue2_length; j++)
                                queue1.Enqueue(queue2.Dequeue());

                            // reinserisco la merged queue
                            queues.Enqueue(queue1);

                            if (queues.Count == queue_length / 2)
                                break;
                        }
                    }
                    else
                    {
                        for (int i = 0; true; i++)
                        {
                            if (queues.Count == queue_length / 2 + 1)
                            {
                                break;
                            }
                            else
                            {
                                Queue<int> queue1 = queues.Dequeue();
                                Queue<int> queue2 = queues.Dequeue();

                                // merge delle due queue
                                int queue2_length = queue2.Count;
                                for (int j = 0; j < queue2_length; j++)
                                    queue1.Enqueue(queue2.Dequeue());

                                // reinserisco la merged queue
                                queues.Enqueue(queue1);
                            }
                        }
                    }

                    // ordino ogni subqueue con insertion sort
                    for (int i = 0; i < queues.Count; i++)
                    {
                        queues.Enqueue(Insertion_Sort(queues.Dequeue()));
                    }

                } while (queues.Count != 1);

                return queues.Dequeue();
            }
            else
            {
                if (vet.Count > 1)
                {
                    int vet0 = vet.Dequeue();
                    int vet1 = vet.Dequeue();
                    if (vet0 > vet1)
                    {
                        vet.Enqueue(vet1);
                        vet.Enqueue(vet0);
                    }
                    else
                    {
                        vet.Enqueue(vet0);
                        vet.Enqueue(vet1);
                    }
                }
                return vet;
            }

        }

        static List<int> getInts(string[] args)
        {
            List<int> list = new List<int>();
            foreach (var s in args)
            {
                int value = -1;
                if (int.TryParse(s, out _))
                {
                    value = int.Parse(s);
                    list.Add(value);
                }
            }
            return list;
        }

        static void Main(string[] args)
        {
            // parso args
            List<int> list = getInts(args);
            // stampo sequenza non ordinata
            Console.Write("Prima: ");
            for (int i = 0; i < list.Count; i++)
            {
                if (i != list.Count - 1)
                    Console.Write(list[i] + " ");
                else
                    Console.WriteLine(list[i]);
            }
            Random ra = new Random();

            // ordinamento con List
            DateTime start_lists = DateTime.Now;
            list = Merge_Insertion_Sort(list, 100);
            DateTime end_lists = DateTime.Now;

            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < list.Count; i++)
            {
                queue.Enqueue(list[i]);
            }

            // ordinamento con Queue
            DateTime start_queues = DateTime.Now;
            queue = Merge_Insertion_Sort(queue, 100);
            DateTime end_queues = DateTime.Now;

            // Stampo sequenza ordinata
            Console.Write("Dopo: ");

            for (int i = 0; i < queue.Count; i++)
            {
                if (i != queue.Count - 1)
                    Console.Write(queue.ElementAt(i) + " ");
                else
                    Console.WriteLine(queue.ElementAt(i));
            }

            Console.WriteLine();

            // stampo tempo esecuzione Liste e Queues
            Console.WriteLine("Tempo per elaborare " + list.Count + " elementi\nListe: " + (end_lists - start_lists));

            Console.WriteLine("Queue: " + (end_queues - start_queues));
        }
    }
}
