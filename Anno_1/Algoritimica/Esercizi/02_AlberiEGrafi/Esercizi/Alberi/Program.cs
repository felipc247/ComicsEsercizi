using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alberi_esempio
{
    internal class Program
    {
        // Esempio di implementazione di un albero binario di ricerca in C#
        public class NodoAlberoBinario
        {
            public int Valore;
            public NodoAlberoBinario Sinistro, Destro;

            public NodoAlberoBinario(int valore)
            {
                Valore = valore;
                Sinistro = Destro = null;
            }
        }

        /*
        1. Implementa l'inserimento in un albero binario di ricerca (BST):

        Scrivi una funzione che inserisca un nuovo nodo in un albero binario di ricerca.
        Assicurati di mantenere la proprietà di ordinamento del BST.

        2. Implementa l'algoritmo di ricerca in un albero binario di ricerca:

        Scrivi una funzione che cerca un valore specifico in un albero binario di ricerca.
        Restituisci il nodo se il valore è presente, altrimenti restituisci null.
        
        3. Implementa un attraversamento in-order di un albero binario:

        Scrivi una funzione che attraversi un albero binario in ordine e stampi i valori dei nodi visitati.
         */

        public class AlberoBinarioDiRicerca
        {
            public NodoAlberoBinario Radice;

            public AlberoBinarioDiRicerca()
            {
                Radice = null;
            }

            // stampa

            public void Print_Tree()
            {
                Print_Tree_Rec(Radice, 0);
            }

            private void Print_Tree_Rec(NodoAlberoBinario nodo, int livello)
            {
                if (nodo == null) { Console.WriteLine("Nessun nodo"); return; }
                Console.WriteLine($"livello => {livello}");
                Console.WriteLine($"{nodo.Valore}");
                if (nodo.Sinistro != null) Print_Tree_Rec(nodo.Sinistro, livello + 1);
                if (nodo.Destro != null) Print_Tree_Rec(nodo.Destro, livello + 1);
            }

            // Metodi di inserimento, ricerca e cancellazione possono essere implementati qui

            public void Inserisci_Valore(int value)
            {
                Inserisci_Valore_Rec(Radice, value);
            }
            private void Inserisci_Valore_Rec(NodoAlberoBinario nodo, int value)
            {
                if (nodo == null) { Radice = new NodoAlberoBinario(value); return; }
                if (value < nodo.Valore)
                {
                    if (nodo.Sinistro == null)
                    {
                        nodo.Sinistro = new NodoAlberoBinario(value);
                    }
                    else
                    {
                        Inserisci_Valore_Rec(nodo.Sinistro, value);
                    }
                }
                else if (value > Radice.Valore)
                {
                    if (nodo.Destro == null)
                    {
                        nodo.Destro = new NodoAlberoBinario(value);
                    }
                    else
                    {
                        Inserisci_Valore_Rec(nodo.Destro, value);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            AlberoBinarioDiRicerca abr = new AlberoBinarioDiRicerca();
            abr.Inserisci_Valore(50);
            abr.Inserisci_Valore(51);
            abr.Inserisci_Valore(53);
            abr.Inserisci_Valore(54);
            abr.Inserisci_Valore(52);
            abr.Inserisci_Valore(56);
            abr.Inserisci_Valore(55);
            abr.Inserisci_Valore(14);
            abr.Inserisci_Valore(564);
            abr.Inserisci_Valore(-1);
            abr.Inserisci_Valore(0);
            abr.Print_Tree();
        }
    }
}
