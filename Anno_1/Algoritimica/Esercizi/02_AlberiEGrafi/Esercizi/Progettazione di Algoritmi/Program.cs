using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettazioneDiAlgoritmi
{
    internal class Program
    {
        /*
         Esercizio 1 (Divide et Impera):
         Problema: Scrivi una funzione che implementi l'algoritmo di ricerca binaria.
         L'algoritmo deve accettare un array ordinato e restituire l'indice dell'elemento cercato, se presente.
         
        */

        public static void Ricerca_Binaria() {
            int[] vet = { 1, 3, 6, 10, 11, 13, 15, 16, 17 };
            int value = 17;
            int pos = Ricerca_Binaria_Rec(vet, 0, vet.Length - 1, value);
            if (pos == -1)
            {
                Console.WriteLine("Numero non trovato");
            }
            else
            {
                Console.WriteLine($"Value: {value} trovato a pos: {pos}");
            }
        }

        // ritorna la posizione del numero se trovato
        static int Ricerca_Binaria_Rec(int[] vet, int sx, int dx, int value)
        {
            int pos = -1;
            int mid = sx + ((dx - sx)/ 2);

            // se sx > dx, significa che il numero non è stato trovato
            if (sx > dx) return pos;

            Console.WriteLine($"sx: {sx}\ndx: {dx}");

            if (vet[mid] == value)
            {
                // valore trovato
                pos = mid;
            }
            else if (vet[mid] < value)
            {
                // value > di vet[mid], cerco nella metà a dx
                pos = Ricerca_Binaria_Rec(vet, mid + 1, dx, value);
            }
            else
            {
                // value < di vet[mid], cerco nella metà a sx
                pos = Ricerca_Binaria_Rec(vet, sx, mid - 1, value);
            }

            return pos;
        }


        /*
         Esercizio 3 (Algoritmi Greedy):
         Problema: Implementa una funzione che risolva 
         il problema del cambio utilizzando monete con tagli di 1, 3 e 4 centesimi.   
         
         */

        public static int Cambio_Minimo() { 
            
        }


        static void Main(string[] args)
        {
            // Ex 1
            Ricerca_Binaria();
            // Ex 2

        }
    }
}
