using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funzioni_Matematiche_e_di_Ricerca_in_Vettore1
{
    internal class Program
    {
        static int CalcolaDoppio(int numero)
        {
            return numero * 2;
        }

        static int CalcolaProdotto(int a, int b)
        {
            return a * b;
        }

        static int SommaElementiVettore(int[] vettore)
        {
            int sommaVet = 0;
            for (int i = 0; i < vettore.Length; i++)
            {
                sommaVet += vettore[i];
            }
            return sommaVet;
        }

        static int TrovaMassimo(int a, int b)
        {
            //return Math.Max(a, b);
            return (a > b) ? a : b;
        }

        static bool VerificaPresenza(int[] vettore, int numero)
        {
            bool presenza = false;
            //return vettore.Contains(numero);
            foreach (int num in vettore)
            {
                if (num == numero)
                {
                    presenza = true;
                    break;
                }
            }
            return presenza;
        }

        static void Main(string[] args)
        {
            // Esegui e stampa i risultati delle funzioni
            int numero = 5;
            Console.WriteLine($"Il doppio di {numero} è: {CalcolaDoppio(numero)}");

            int prodotto = CalcolaProdotto(3, 7);
            Console.WriteLine($"Il prodotto di 3 e 7 è: {prodotto}");

            int[] vettore = { 2, 4, 6, 8 };
            Console.WriteLine($"La somma degli elementi nel vettore è: {SommaElementiVettore(vettore)}");

            int maggiore = TrovaMassimo(15, 8);
            Console.WriteLine($"Il numero maggiore tra 15 e 8 è: {maggiore}");

            bool presente = VerificaPresenza(vettore, 6);
            Console.WriteLine($"Il numero 6 è presente nel vettore: {presente}");
        }
    }
}
