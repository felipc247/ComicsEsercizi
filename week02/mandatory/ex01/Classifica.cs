using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfida_Algoritmica___Ordinamento
{
    internal class Classifica
    {
        private Dictionary<char[], int> giocatoriPunteggi = new Dictionary<char[], int>();

        public Classifica()
        {

        }

        public void AggiungiGiocatore(char[] nome)
        {
            try
            {
                if (giocatoriPunteggi.ContainsKey(nome)) throw new GiocatoreEsistenteException();
                giocatoriPunteggi.Add(nome, 0);
                CC.MagentaFr("Giocatore aggiunto\n");

            }
            catch (GiocatoreEsistenteException) { }
        }

        public int getCountGiocatori() {
            return giocatoriPunteggi.Count;
        }

        public List<char[]> GetGiocatori()
        {
            List<char[]> nomi = new List<char[]>();
            foreach ( var g in giocatoriPunteggi)
            {
                nomi.Add(g.Key);
            }
            return nomi;
        }

        public void AggiornaPunteggio(int punteggio, char[] nome)
        {
            giocatoriPunteggi[nome] = punteggio;
        }

        public void StampaClassifica()
        {
            foreach (var g in giocatoriPunteggi)
            {
                CC.CyanFr(new String(g.Key) + " ");
                Console.WriteLine(g.Value);
            }
        }

        public void BubbleSort()
        {
            // trasformo il dizionario in una lista, così da poter usare degli indici
            // per fare i confronti e gli spostamenti
            List<KeyValuePair<char[], int>> support = giocatoriPunteggi.ToList();
            for (int i = 0; i < support.Count - 1; i++)
            {
                for (int j = 0; j < support.Count - i - 1; j++)
                {
                    if (support[j].Value < support[j + 1].Value)
                    {
                        KeyValuePair<char[], int> temp = support[j];
                        support[j] = support[j + 1];
                        support[j + 1] = temp;
                    }
                }
            }

            // aggiorno i valori nel dizionario
            giocatoriPunteggi.Clear();
            foreach (var s in support)
            {
                giocatoriPunteggi.Add(s.Key, s.Value);
            }
        }


    }
}
