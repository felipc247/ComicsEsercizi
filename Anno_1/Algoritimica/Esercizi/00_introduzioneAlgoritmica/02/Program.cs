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
                for (int j = 0; j < vet.Length - i - 1; j++)
                {
                    if (vet[j] > vet[j + 1]) { 
                        int temp = vet[j];
                        vet[j] = vet[j + 1];
                        vet[j + 1] = temp;
                    }
                }
            }
        }

        static void Print_Int_Vet(int[] vet) {
            for (int i = 0; i < vet.Length; i++)
            {
                Console.WriteLine(vet[i]);
            }
        }

        static void Main(string[] args)
        {
            int[] vet = { 65, 13, 7, 8, 1 };  
            Bubble_Sort(vet);
            Print_Int_Vet(vet);
        }
    }
}
