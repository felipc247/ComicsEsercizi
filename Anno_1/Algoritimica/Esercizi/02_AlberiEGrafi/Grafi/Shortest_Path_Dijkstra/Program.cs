using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafo_pesato
{
    internal class Program
    {
        public class GrafoPesato
        {
            public Dictionary<int, List<(int, int)>> Archi; // Dizionario: nodo -> lista di archi collegati (nodo destinazione, peso)
            public Dictionary<int, int> Predecessori; // Indica il predecessore del nodo, permette di ricostruire il percorso <nodo, predecessore>
            public Dictionary<int, int> Distanza_Totale; // Memorizza la distanza totale dal nodo di partenza a questo nodo <nodo, distanza>

            public GrafoPesato()
            {
                Archi = new Dictionary<int, List<(int, int)>>();
                Predecessori = new Dictionary<int, int>();
                Distanza_Totale = new Dictionary<int, int>();
            }

            public void AggiungiArco(int nodoPartenza, int nodoDestinazione, int peso)
            {
                if (!Archi.ContainsKey(nodoPartenza))
                    Archi[nodoPartenza] = new List<(int, int)>();

                Archi[nodoPartenza].Add((nodoDestinazione, peso));

                if (!Archi.ContainsKey(nodoDestinazione))
                    Archi[nodoDestinazione] = new List<(int, int)>();

                Archi[nodoDestinazione].Add((nodoPartenza, peso));
            }

            public String[] Shortest_Path(int nodoPartenza, int nodoDestinazione)
            {
                Dictionary<int, bool> visitati = new Dictionary<int, bool>();
                Setup(nodoPartenza, visitati);
                Dijkstra_rec(nodoPartenza, visitati);
                return Shortest_Path_Priv(nodoPartenza, nodoDestinazione);
            }

            // Stampa la tabella di routing
            public void Print_table()
            {
                Console.WriteLine("Nodo | Distanza | Predecessore\n");
                foreach (var nodo in Archi)
                {
                    int num_nodo = nodo.Key;
                    int distance = Distanza_Totale[nodo.Key];
                    int predecessore;
                    String predecessore_string = "null";

                    try
                    {
                        predecessore = Predecessori[nodo.Key];
                        predecessore_string = predecessore + "";
                    }
                    catch (Exception)
                    {

                    }
                    Console.WriteLine($"{nodo.Key} | {Distanza_Totale[nodo.Key]} | {predecessore_string}");
                }
            }

            // Ritorna il percorso più breve 
            private String[] Shortest_Path_Priv(int nodoPartenza, int nodoDestinazione)
            {
                List<String> path = new List<String>();
                int predecessore = nodoDestinazione;
                path.Add(predecessore + "");
                while (predecessore != nodoPartenza)
                {
                    predecessore = Predecessori[predecessore];
                    path.Insert(0, predecessore + "");
                }
                return path.ToArray();
            }

            // Prepara la tabella per l'algoritmo di Dijkstra e il Dictionary di Visitati
            private void Setup(int nodoPartenza, Dictionary<int, bool> visitati)
            {
                // SETUP Distanze

                // nodo partenza
                Distanza_Totale[nodoPartenza] = 0;
                // altri nodi
                foreach (var nodo in Archi)
                {
                    if (nodo.Key != nodoPartenza)
                    {
                        Distanza_Totale[nodo.Key] = int.MaxValue;
                    }
                }

                // SETUP Visitati

                foreach (var nodo in Archi)
                {
                    visitati[nodo.Key] = false;
                }
            }

            private void Dijkstra_rec(int nodoAttuale, Dictionary<int, bool> visitati)
            {
                List<(int, int)> adiacenze = Archi[nodoAttuale].ToList();
                // controllo quali sono le adiacenze non visitate
                // List di supporto, perché non posso modificare la dimensione della Lista durante un foreach
                List<(int, int)> da_rimuovere = new List<(int, int)>();

                foreach (var nodo in adiacenze)
                {
                    if (visitati[nodo.Item1] == true)
                        da_rimuovere.Add(nodo);
                }

                for (int i = 0; i < da_rimuovere.Count; i++)
                {
                    adiacenze.Remove(da_rimuovere[i]);
                }

                // vado ad inserire distanza_totale e predecessore ad ogni adiacenza
                foreach (var nodo in adiacenze)
                {
                    // Se la distanza totale a nodo è minore tramite nodoAttuale
                    // Aggiorno i valori
                    if (Distanza_Totale[nodoAttuale] + nodo.Item2 < Distanza_Totale[nodo.Item1])
                    {
                        Distanza_Totale[nodo.Item1] = Distanza_Totale[nodoAttuale] + nodo.Item2;
                        Predecessori[nodo.Item1] = nodoAttuale;
                    }
                }
                visitati[nodoAttuale] = true;

                // non ci sono più nodi da visitare
                if (!Any_Unvisited(visitati)) return;

                // controllo quale sarà il prossimo nodo non visitato
                // con la distanza totale più piccola conosciuta ( != int.MaxValue)
                // e ripeto fino a quando i nodi non sono stati visitati tutti

                int next_node = 0;
                foreach (var item in visitati)
                {
                    if (item.Value == true) continue;
                    if (Distanza_Totale[item.Key] == int.MaxValue) continue;

                    if (next_node == 0)
                    {
                        next_node = item.Key;
                    }
                    else if (Distanza_Totale[item.Key] < Distanza_Totale[next_node])
                    {
                        next_node = item.Key;
                    }
                }

                Dijkstra_rec(next_node, visitati);

            }

            // return true if there unvisited nodes, false if not
            private bool Any_Unvisited(Dictionary<int, bool> visitati)
            {
                bool any_unvisited = false;
                foreach (var item in visitati)
                {
                    if (!item.Value)
                    {
                        any_unvisited = true;
                        break;
                    }
                }
                return any_unvisited;
            }         

        }

        static void Main(string[] args)
        {
            GrafoPesato grafoPesato = new GrafoPesato();
            grafoPesato.AggiungiArco(0, 2, 5);
            grafoPesato.AggiungiArco(0, 3, 2);
            grafoPesato.AggiungiArco(3, 7, 1);
            grafoPesato.AggiungiArco(2, 4, 2);
            grafoPesato.AggiungiArco(4, 7, 1);

            grafoPesato.Shortest_Path(0, 3);

            grafoPesato.Print_table();

            String[] shortest_path = grafoPesato.Shortest_Path(0, 4);
            Console.WriteLine("\nShortest Path:");
            for (int i = 0; i < shortest_path.Length; i++)
            {
                Console.WriteLine("  " + shortest_path[i]);
            }
        }
    }
}
