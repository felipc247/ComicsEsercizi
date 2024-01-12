using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordinamento_Array_con_Stack
{
    internal class Program
    {
        static void printList(List<int> vet, int rowLength)
        {
            bool ciclo = false;
            for (int i = 0; i < vet.Count; i++)
            {
                // stampa { all'inizio di ogni raggruppamento
                if (i == 0 || (i % rowLength == 0 && !ciclo))
                {
                    Console.Write("{");
                    ciclo = true;
                }

                // stampa numero
                Console.Write($"{vet[i]}");

                // spazia i vari numeri
                if (!((i + 1) % rowLength == 0 && ciclo)) Console.Write(" ");

                // stampa } alla fine del raggruppamento e alla fine del vettore
                if (((i + 1) % rowLength == 0 && ciclo) || i == vet.Count - 1)
                {
                    Console.WriteLine("}");
                    ciclo = false;
                }
            }
        }

        // shift a dx lista
        static void rotate(ref List<int> intList, int shift)
        {
            // lista di supporto per memorizzare temporaneamente gli elementi da shiftare
            List<int> supportList = new List<int>();
            // copio gli elementi tranne gli ultimi (shift)
            supportList.AddRange(intList.GetRange(0, intList.Count - shift));

            // elimino il range copiato
            intList.RemoveRange(0, intList.Count - shift);
            // lo inserisco dopo gli elementi non eliminati
            intList.InsertRange(shift, supportList);
        }

        // shift sx list
        static void rotateReverse(ref List<int> intList, int shift)
        {
            // lista di supporto per memorizzare temporaneamente gli elementi da shiftare
            List<int> supportList = new List<int>();
            // copio gli elementi tranne i primi (shift)
            supportList.AddRange(intList.GetRange(shift, intList.Count - shift));

            // elimino il range copiato
            intList.RemoveRange(shift, intList.Count - shift);
            // lo inserisco prima degli elementi non eliminati
            intList.InsertRange(0, supportList);
        }

        // scambia il primo elemento di uno stack con il primo elemento dell'altro

        static void swap(ref List<int> intList, ref List<int> supportList)
        {
            // salvo il valore in cima di intList
            int temp = intList[intList.Count - 1];

            // rimuovo primo valore e aggiungo primo valore di supportList
            intList.RemoveAt(intList.Count - 1);
            intList.Add(supportList[supportList.Count - 1]);
            // rimuovo primo valore e aggiungo primo valore di intList (temp)
            supportList.RemoveAt(supportList.Count - 1);
            supportList.Add(temp);
        }

        // sposta il primo elemento di uno stack in cima all'altro

        static void push(ref List<int> intList, ref List<int> supportList, int where)
        {
            if (where == 0)
            {
                // push in intList
                if (supportList.Count > 0)
                {
                    intList.Add(supportList[supportList.Count - 1]);
                    supportList.RemoveAt(supportList.Count - 1);
                    Console.WriteLine("Push in intList eseguito");
                }

            }
            else if (where == 1)
            {
                // push in supportList
                if (intList.Count > 0)
                {
                    supportList.Add(intList[intList.Count - 1]);
                    intList.RemoveAt(intList.Count - 1);
                    Console.WriteLine("Push in supportList eseguito");
                }

            }

        }

        // algoritmo di ordinamento per array di piccole dimensioni
        static void insertionSort(ref List<int> intList, ref List<int> supportList)
        {
            int n = intList.Count;

            // porto l'ultimo elemento in cima allo stack (.Count - 1)
            rotateReverse(ref intList, 1);

            push(ref intList, ref supportList, 1);

            // per ogni elemento rimanente della lista trovo la posizione corretta nella stessa
            for (int i = 0; i < intList.Count - 1;)
            {
                // prendo l'ultimo elemento di intList e trovo la posizione corretta in supportList
                for (int j = 0; j < supportList.Count; j++)
                {
                    if (intList[i] < supportList[j])
                    {
                        // IL NUMERO È MINORE 
                        // preparo lo stack di supporto per accogliere il nuovo numero
                        // shifto a dx di un numero di posizioni pari ai numeri > del numero da pushare
                        // cioè .Count - numeri minori del numero (j + 1)
                        int shift = supportList.Count - j;
                        for (int k = 0; k < shift; k++)
                        {
                            rotate(ref supportList, 1);
                        }
                        // ultimo elemento a primo, così posso pusharlo nell'altro stack
                        rotateReverse(ref intList, 1);
                        push(ref intList, ref supportList, 1);
                        if (j == 0)
                        {
                            // intList[i] è minore del numero più piccolo di supportList
                            // perciò shiftare a sx non funziona, in quanto non ho numeri sia minori che maggiori dello stesso
                            rotate(ref supportList, 1);
                        }
                        else
                        {
                            // shifto a sx dello stesso numero di posizioni
                            for (int k = 0; k < shift; k++)
                            {
                                rotate(ref supportList, 1);
                            }
                        }
                        // inserimento completato
                        break;

                    }
                    else if (j == supportList.Count - 1)
                    {
                        // neanche supportList[supportList.Count - 1] è maggiore di intList[i]
                        // perciò va semplicemente in cima allo stack
                        push(ref intList, ref supportList, 1);
                        break;
                    }

                }
            }

        }

        static void Main(string[] args)

        {
            List<int> vet3 = new List<int>();
            List<int> vet5 = new List<int>();
            List<int> vet100 = new List<int>();

            List<int> vet3Sup = new List<int>();
            List<int> vet5Sup = new List<int>();
            List<int> vet100Sup = new List<int>();

            int int3 = 3, int5 = 5, int100 = 100;
            Random ra = new Random();

            // riempimento vettori casuale
            for (int i = 0; i < int3; i++)
            {
                vet3.Add(ra.Next(0, 1000));
                for (int j = 0; j < vet3.Count; j++)
                {
                    if (vet3[i] == vet3[j] && i != j)
                    {
                        // il numero esiste già, ripeto il ciclo
                        vet3.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            for (int i = 0; i < int5; i++)
            {
                vet5.Add(ra.Next(0, 1000));
                for (int j = 0; j < vet5.Count; j++)
                {
                    if (vet5[i] == vet5[j] && i != j)
                    {
                        // il numero esiste già, ripeto il ciclo
                        vet5.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            for (int i = 0; i < int100; i++)
            {
                vet100.Add(ra.Next(0, 1000));
                for (int j = 0; j < vet100.Count; j++)
                {
                    if (vet100[i] == vet100[j] && i != j)
                    {
                        // il numero esiste già, ripeto il ciclo
                        vet100.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            Console.WriteLine("vet100");
            printList(vet100, 10);

            Console.WriteLine("vet100Sup");
            printList(vet100Sup, 10);

            insertionSort(ref vet100, ref vet100Sup);

            Console.WriteLine("vet100");
            printList(vet100, 10);

            Console.WriteLine("vet100Sup");
            printList(vet100Sup, 10);





        }
    }
}
