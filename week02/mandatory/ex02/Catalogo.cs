using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricerca_Avanzata_Dictionary
{
    internal class Catalogo
    {
        private Dictionary<string, List<String>> catalogo = new Dictionary<string, List<string>>();

        public Catalogo() { }

        public void AggiungiGiocoCatalogo(String nome, List<String> piattaforme)
        {
            catalogo.Add(nome, piattaforme);
        }

        public bool CheckGiocoEsistente(String nome)
        {
            return catalogo.ContainsKey(nome);
        }

        public void stampaCatalogo()
        {
            foreach (var item in catalogo)
            {
                CC.printColor(" " + item.Key + " ", ConsoleColor.White, ConsoleColor.DarkCyan);
                Console.WriteLine();
                List<String> list = item.Value;
                for (int i = 0; i < list.Count; i++)
                {
                    // Formattazione per migliore lettura
                    Console.Write("  ");
                    if (i % 2 == 0)
                    {
                        CC.DarkYellowFr(list[i]);
                    }
                    else
                    {
                        CC.YellowFr(list[i]);
                    }
                    Console.WriteLine();
                }
            }
        }

        public List<String> RicercaGiochiPiattaforma(String piattaforma)
        {
            List<String> listGiochi = new List<String>();
            foreach (var item in catalogo)
            {
                // se value contiene la piattaforma allora posso
                // aggiungere il gioco alla lista
                if (item.Value.Contains(piattaforma))
                {
                    listGiochi.Add(item.Key);
                }
            }

            return listGiochi;
        }

        public void StampaStatistiche()
        {
            CC.DarkCyanFr($"{catalogo.Count} giochi disponibili nel catalogo\n");

            // calcolo media piattaforme per gioco
            int sommaPiattaforme = 0;

            foreach (var item in catalogo)
            {
                sommaPiattaforme += item.Value.Count;
            }

            double mediaPiattaforme = (double) sommaPiattaforme / catalogo.Count;

            CC.DarkYellowFr($"Ogni gioco è mediamente disponibile in {mediaPiattaforme.ToString("N2")} piattaforme\n");
        }
    }
}
