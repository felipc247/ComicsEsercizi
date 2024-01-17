using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    internal class Program
    {

        public class NodoAVL
        {
            public int Valore;
            public NodoAVL Sinistro;
            public NodoAVL Destro;
            public int Altezza;

            public NodoAVL(int valore)
            {
                Valore = valore;
                Sinistro = null;
                Destro = null;
                Altezza = 1; // Inizialmente, un nuovo nodo ha altezza 1
            }
        }

        public class AlberoAVL
        {
            public NodoAVL Radice;

            public AlberoAVL()
            {
                Radice = null;
            }

            // ricerca

            public void Ricerca(int value)
            {
                if (Ricerca_Rec(Radice, value))
                {
                    Console.WriteLine("Found");
                }
                else
                {
                    Console.WriteLine("NOT Found");
                }
            }

            private bool Ricerca_Rec(NodoAVL nodo, int value)
            {
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

            private void Print_Tree_Rec(NodoAVL nodo, int livello)
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

            private void Inserisci_Valore_Rec(NodoAVL nodo, int value)
            {
                if (nodo == null) { Radice = new NodoAVL(value); return; }
                if (value < nodo.Valore)
                {
                    if (nodo.Sinistro == null)
                    {
                        nodo.Sinistro = new NodoAVL(value);
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
                        nodo.Destro = new NodoAVL(value);
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

        }
    }
}
