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
        static void printVet(int[] vet, int rowLength)
        {
            bool ciclo = false;
            for (int i = 0; i < vet.Length; i++)
            {
                if (i == 0 || (i - 1 % rowLength == 0 && !ciclo))
                {
                    Console.Write("{");
                    ciclo = true;
                }
                else if ((i - 1 % rowLength == 0 && ciclo) || i == vet.Length - 1)
                {
                    Console.WriteLine("}");
                    ciclo = false;
                }

                Console.Write($"{vet[i]}");
            }
        }


        static void Main(string[] args)

        {
            int[] vet3 = new int[3];
            int[] vet5 = new int[5];
            int[] vet100 = new int[100];

            ArrayList vet1 = new ArrayList();

            Random ra = new Random();

            // riempimento vettori casuale
            for (int i = 0; i < vet3.Length; i++)
            {
                vet3[i] = ra.Next(0, 1000);
                for (int j = 0; j < vet3.Length; j++)
                {
                    if (vet3[i] == vet3[j] && i != j)
                    {
                        // il numero esiste già, ripeto il ciclo
                        i--;
                        break;
                    }
                }
            }

            for (int i = 0; i < vet5.Length; i++)
            {
                vet5[i] = ra.Next(0, 1000);
                for (int j = 0; j < vet5.Length; j++)
                {
                    if (vet5[i] == vet5[j] && i != j)
                    {
                        // il numero esiste già, ripeto il ciclo
                        i--;
                        break;
                    }
                }
            }

            for (int i = 0; i < vet100.Length; i++)
            {
                vet100[i] = ra.Next(0, 1000);
                for (int j = 0; j < vet100.Length; j++)
                {
                    if (vet100[i] == vet100[j] && i != j)
                    {
                        // il numero esiste già, ripeto il ciclo
                        i--;
                        break;
                    }
                }
            }

            printVet(vet3, 3);
            printVet(vet5, 5);
            printVet(vet100, 10);


        }
    }
}
