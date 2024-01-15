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

            // ricerca

            public void Ricerca(int value) {
                if (Ricerca_Rec(Radice, value))
                {
                    Console.WriteLine("Found");
                }
                else {
                    Console.WriteLine("NOT Found");
                }
            }

            private bool Ricerca_Rec(NodoAlberoBinario nodo, int value) {
                bool found = false;
                // Albero vuoto
                if (nodo == null) { return found; }
                // Valore trovato, return true
                if (nodo.Valore == value) { found = true; return found; }
                // Controllo se devo cercare nel ramo SX o ramo DX
                if (value < nodo.Valore)
                {
                    // Se figlio SX esiste posso cercare tra i suoi figli
                    if (nodo.Sinistro != null)
                    {
                        found = Ricerca_Rec(nodo.Sinistro, value);
                        // Se trovato return true
                        if (found) return found;
                    }                   
                }
                else if (value > Radice.Valore)
                {
                    // Se figlio DX esiste posso cercare tra i suoi figli
                    if (nodo.Destro != null)
                    {
                        found = Ricerca_Rec(nodo.Destro, value);
                        // Se trovato return true
                        if (found) return found;
                    }
                }
                return found;
            }

            // stampa in-order

            public void Print_Tree()
            {
                Print_Tree_Rec(Radice, 0);
            }

            private void Print_Tree_Rec(NodoAlberoBinario nodo, int livello)
            {
                if (nodo == null) { Console.WriteLine("Nessun nodo"); return; }
                if (nodo.Sinistro != null) Print_Tree_Rec(nodo.Sinistro, livello + 1);
                Console.WriteLine($"livello => {livello}");
                Console.WriteLine($"{nodo.Valore}");
                if (nodo.Destro != null) Print_Tree_Rec(nodo.Destro, livello + 1);
            }

            // Inserimento valore

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
            // Popolamento abr
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
            // Stampa
            abr.Print_Tree();
            // Ricerca
            abr.Ricerca(564);
        }
    }
}
