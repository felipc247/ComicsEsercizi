using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            public NodoAVL Ricerca(int value)
            {
                return Ricerca_Rec(Radice, value);
            }

            private NodoAVL Ricerca_Rec(NodoAVL nodo, int value)
            {
                NodoAVL nodo_b = null;
                // Albero vuoto
                if (nodo == null) { return nodo_b; }
                // Valore trovato, return Nodo
                if (nodo.Valore == value) { return nodo; }
                // Controllo se devo cercare nel ramo SX o ramo DX
                if (value < nodo.Valore)
                {
                    // Se figlio SX esiste posso cercare tra i suoi figli
                    if (nodo.Sinistro != null)
                    {
                        nodo_b = Ricerca_Rec(nodo.Sinistro, value);
                        // Se trovato return Nodo
                        if (nodo != null) return nodo_b;
                    }
                }
                else if (value > nodo.Valore)
                {
                    // Se figlio DX esiste posso cercare tra i suoi figli
                    if (nodo.Destro != null)
                    {
                        nodo_b = Ricerca_Rec(nodo.Destro, value);
                        // Se trovato return Nodo
                        if (nodo != null) return nodo_b;
                    }
                }
                return nodo_b;
            }

            private NodoAVL Ricerca_Rec_Father(NodoAVL nodo, int value)
            {
                NodoAVL nodo_b = null;
                // Albero vuoto
                if (nodo == null) { return nodo_b; }
                // Valore trovato, return Nodo
                if (nodo.Valore == value) { return nodo; }
                // Controllo se devo cercare nel ramo SX o ramo DX
                if (value < nodo.Valore)
                {
                    // Se figlio SX esiste posso cercare tra i suoi figli
                    if (nodo.Sinistro != null)
                    {
                        nodo_b = Ricerca_Rec_Father(nodo.Sinistro, value);
                        // Se trovato return Nodo
                        if (nodo_b != null)
                        {
                            if (nodo_b.Valore == value)
                                return nodo;
                            else
                                return nodo_b;
                        }
                    }
                }
                else if (value > nodo.Valore)
                {
                    // Se figlio DX esiste posso cercare tra i suoi figli
                    if (nodo.Destro != null)
                    {
                        nodo_b = Ricerca_Rec_Father(nodo.Destro, value);
                        // Se trovato return Nodo
                        if (nodo_b != null)
                        {
                            if (nodo_b.Valore == value)
                                return nodo;
                            else
                                return nodo_b;
                        }
                    }
                }
                return nodo_b;
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
                Update_Heights(Radice);
                Check_Rotation(Radice, null);
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
                else if (value > nodo.Valore)
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

            // Rimozione Nodo

            public void Rimuovi_Nodo(int value)
            {
                // Trovo il nodo da rimuovere nell'albero
                NodoAVL nodo = Ricerca_Rec(Radice, value);
                // Trovo il padre
                NodoAVL padre = Ricerca_Rec_Father(Radice, value);
                if (nodo.Sinistro != null && nodo.Destro != null)
                { // nodo è una foglia, posso rimuoverlo direttamente
                    nodo = null;
                }
                else if (nodo.Sinistro != null)
                { // nodo ha child a sx, prendo nel subtree il nodo con valore maggiore
                    // e lo piazzo al posto del nodo da eliminare

                    if (nodo.Sinistro.Destro != null)
                    {
                        // abbiamo figli a dx
                        // trovo il maggiore
                        NodoAVL largest_sx_dx = Find_Largest_Node_Left_Subtree(nodo.Sinistro);
                    }
                    else { 
                        // non abbiamo figli a dx, nodo.Sinistro prende il posto di nodo
                        nodo = nodo.Sinistro;
                        if (padre != null)
                        {
                            if (nodo.Valore < padre.Valore)
                                padre.Sinistro = nodo;
                            else if (nodo.Valore > padre.Valore)
                                padre.Destro = nodo;
                        }
                        else
                        {
                            Radice = nodo;
                        }

                    }
                }
                Update_Heights(Radice);
                Check_Rotation(Radice, null);
            }

            private NodoAVL Find_Largest_Node_Left_Subtree(NodoAVL nodo)
            {
                if (nodo.Destro != null)
                {
                    return Find_Largest_Node_Left_Subtree(nodo.Destro);
                }
                else
                {
                    return nodo;
                }
            }

            // Aggiornamento Altezze
            private void Update_Heights(NodoAVL nodo)
            {
                if (nodo.Sinistro == null && nodo.Destro == null)
                {
                    // se altezza != 1, cambio
                    nodo.Altezza = 1;
                    return;
                }

                if (nodo.Sinistro != null)
                {
                    // Se Sinistro non è null vado a controllare il figlio a SX
                    Update_Heights(nodo.Sinistro);
                    // Aggiorno l'altezza
                    // Se il nodo ha entrambi i figli, posso fare il confronto tra SX e DX
                    // Altrimenti altezza = SX.Altezza + 1
                    if (nodo.Destro != null)
                        nodo.Altezza = (nodo.Sinistro.Altezza > nodo.Destro.Altezza) ? nodo.Sinistro.Altezza + 1 : nodo.Destro.Altezza + 1;
                    else
                        nodo.Altezza = nodo.Sinistro.Altezza + 1;
                }

                if (nodo.Destro != null)
                {
                    // Stessa cosa per figlio DX
                    Update_Heights(nodo.Destro);
                    if (nodo.Sinistro != null)
                        nodo.Altezza = (nodo.Destro.Altezza > nodo.Sinistro.Altezza) ? nodo.Destro.Altezza + 1 : nodo.Sinistro.Altezza + 1;
                    else
                        nodo.Altezza = nodo.Destro.Altezza + 1;
                }
            }

            // Controllo la presenza di eventuali sbilanciamenti
            private void Check_Rotation(NodoAVL nodo, NodoAVL padre)
            {
                if (nodo.Destro == null && nodo.Sinistro == null) return;

                if (nodo.Sinistro != null)
                {
                    // Se Sinistro non è null vado a controllare il figlio a SX
                    Check_Rotation(nodo.Sinistro, nodo);
                    // Se il nodo ha entrambi i figli, posso fare Sx.Altezza - Dx.Altezza per ottenere 
                    // Il bf (balance factor)
                    // Se bf != 1,0,-1, devo ruotare

                    int bf = 0, sx_bf = 0;
                    // Calcolo bf nodo.Sinistro
                    if (nodo.Sinistro.Sinistro != null && nodo.Sinistro.Destro != null)
                        sx_bf = nodo.Sinistro.Sinistro.Altezza - nodo.Sinistro.Destro.Altezza;
                    else if (nodo.Sinistro.Sinistro != null)
                        sx_bf = nodo.Sinistro.Sinistro.Altezza - 0;
                    else if (nodo.Sinistro.Destro != null)
                        sx_bf = 0 - nodo.Sinistro.Destro.Altezza;

                    // Calocolo bf nodo attuale
                    if (nodo.Destro != null)
                        bf = nodo.Sinistro.Altezza - nodo.Destro.Altezza;
                    else
                        bf = nodo.Sinistro.Altezza - 0;

                    if (bf > 1)
                    {
                        // Right rotation
                        if (sx_bf > 0)
                        {
                            Console.WriteLine("Right Rotation");
                            Right_Rotation(nodo, padre);
                            Update_Heights(Radice);
                        }
                        // Left Right rotation
                        if (sx_bf < 0)
                        {
                            Console.WriteLine("Left Right Rotation");
                            // Left Rotation part
                            // Salvo un eventuale nodo/subtree di nodo.Sinistro.Destro.Sinistro
                            NodoAVL Sx_swap_save = (nodo.Sinistro.Destro.Sinistro != null) ? nodo.Sinistro.Destro.Sinistro : null;
                            // swappo nodo.Sinistro con nodo.Sinistro.Destro
                            NodoAVL temp = nodo.Sinistro;
                            nodo.Sinistro = nodo.Sinistro.Destro;
                            nodo.Sinistro.Sinistro = temp;
                            // Se Sx_swap_save null, annullo il subtree a dx, altrimenti aggiungo il subtree salvato di nodo.Sinistro.Destro.Sinistro
                            nodo.Sinistro.Sinistro.Destro = Sx_swap_save;
                            // Adesso posso fare una Right Rotation
                            Right_Rotation(nodo, padre);
                            Update_Heights(Radice);
                        }
                    }

                }

                if (nodo.Destro != null)
                {
                    // Se Destro non è null vado a controllare il figlio a DX
                    Check_Rotation(nodo.Destro, nodo);
                    // Se il nodo ha entrambi i figli, posso fare Sx.Altezza - Dx.Altezza per ottenere 
                    // Il bf (balance factor)
                    // Se bf != 1,0,-1, devo ruotare

                    int bf = 0, dx_bf = 0;
                    // Calcolo bf nodo.Destro
                    if (nodo.Destro.Sinistro != null && nodo.Destro.Destro != null)
                        dx_bf = nodo.Destro.Sinistro.Altezza - nodo.Destro.Destro.Altezza;
                    else if (nodo.Destro.Sinistro != null)
                        dx_bf = nodo.Destro.Sinistro.Altezza - 0;
                    else if (nodo.Destro.Destro != null)
                        dx_bf = 0 - nodo.Destro.Destro.Altezza;

                    // Calocolo bf nodo attuale
                    if (nodo.Sinistro != null)
                        bf = nodo.Sinistro.Altezza - nodo.Destro.Altezza;
                    else
                        bf = 0 - nodo.Destro.Altezza;

                    if (bf < -1)
                    {
                        if (dx_bf < 0)
                        {
                            Console.WriteLine("Left Rotation");
                            Left_Rotation(nodo, padre);
                            Update_Heights(Radice);
                        }

                        if (dx_bf > 0)
                        {
                            Console.WriteLine("Right Left Rotation");
                            // Right Rotation part
                            // Salvo un eventuale subtree in nodo.Destro.Sinistro.Destro
                            NodoAVL Dx_swap_save = (nodo.Destro.Sinistro.Destro != null) ? nodo.Destro.Sinistro.Destro : null;
                            // swappo nodo.Destro con nodo.Destro.Sinistro
                            NodoAVL temp = nodo.Destro;
                            nodo.Destro = nodo.Destro.Sinistro;
                            nodo.Destro.Destro = temp;

                            // Se Dx_swap_save null, annullo il subtree a sx, altrimenti aggiungo il subtree salvato di nodo.Destro.Sinistro.Destro
                            nodo.Destro.Destro.Sinistro = Dx_swap_save;
                            // Adesso posso fare una Left Rotation
                            Left_Rotation(nodo, padre);
                            Update_Heights(Radice);
                        }
                    }
                }
            }

            private void Right_Rotation(NodoAVL nodo, NodoAVL padre)
            {
                if (padre != null)
                {
                    if (nodo.Valore < padre.Valore)
                        padre.Sinistro = nodo.Sinistro;
                    else if (nodo.Valore > padre.Valore)
                        padre.Destro = nodo.Sinistro;
                }
                else
                {
                    Radice = nodo.Sinistro;
                }

                // Devo salvare un eventuale Nodo in nodo.Sinistro.Destro prima di sostituirlo con nodo
                NodoAVL temp = (nodo.Sinistro.Destro != null) ? nodo.Sinistro.Destro : null;
                nodo.Sinistro.Destro = nodo;

                // Se c'era un valore, vado a reinserirlo nell'albero, altrimenti annullo il subtree a sx
                // Se non lo faccio vado in stackoverflow, perché il subtree a sx avrà un collegamento con dei
                // nodi a livelli superiori e perciò va in loop
                nodo.Sinistro.Destro.Sinistro = temp;
                int e = 0;
            }

            private void Left_Rotation(NodoAVL nodo, NodoAVL padre)
            {
                if (padre != null)
                {
                    if (nodo.Valore < padre.Valore)
                        padre.Sinistro = nodo.Destro;
                    else if (nodo.Valore > padre.Valore)
                        padre.Destro = nodo.Destro;
                }
                else
                {
                    Radice = nodo.Destro;
                }
                NodoAVL temp = (nodo.Destro.Sinistro != null) ? nodo.Destro.Sinistro : null;
                nodo.Destro.Sinistro = nodo;

                // Se c'era un nodo o un subtree, vado a reinserirlo nell'albero, altrimenti annullo il subtree a dx
                nodo.Destro.Sinistro.Destro = temp;

            }

        }

        static void Main(string[] args)
        {
            AlberoAVL alberoAVL = new AlberoAVL();
            alberoAVL.Inserisci_Valore(1);
            alberoAVL.Inserisci_Valore(2);
            alberoAVL.Inserisci_Valore(3);
            alberoAVL.Inserisci_Valore(4);
            alberoAVL.Inserisci_Valore(5);
            alberoAVL.Inserisci_Valore(6);

            alberoAVL.Print_Tree();

            alberoAVL.Rimuovi_Nodo(1);

            int stocaz = 0;
        }
    }
}
