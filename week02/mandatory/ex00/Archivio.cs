using Archivio_videogiochi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_Videogiochi
{
    internal class Archivio
    {
        private String nome;
        private List<Videogioco> videogiocos = new List<Videogioco>();

        public Archivio(String nome)
        {
            this.nome = nome;
        }

        // GET // SET
        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public List<Videogioco> Videogiocos
        {
            get { return videogiocos; }
        }

        // METODI
        public void spezzaTesto()
        {
            CC.MagentaFr("---------------------------");
        }

        // Aggiunta videogioco con controllo sul nome
        // deve essere univoco (ID)
        public void AggiungiVideogioco(Videogioco videogioco)
        {
            Videogioco check = videogiocos.Find(v => v.Nome == videogioco.Nome);

            try
            {
                if (check == null)
                {
                    if (videogioco.Punteggio < 1 || videogioco.Punteggio > 10) throw new PunteggioNonValidoException();
                    videogiocos.Add(videogioco);
                    CC.CyanFr("Videogioco aggiunto");
                }
                else
                {
                    throw new VideogiocoEsistenteException();
                }
            }
            catch (VideogiocoEsistenteException) { }
            catch (PunteggioNonValidoException) { }

        }

        public void RimuoviVideogioco(String nome)
        {
            if (videogiocos.Count == 0) { CC.DarkYellowFr("Hai zero giochi :("); return; };

            try
            {
                Videogioco videogioco = videogiocos.Find(v => v.Nome.Equals(nome));

                if (videogioco == null)
                {
                    throw new VideogiocoNonEsistenteException();
                }
                else
                {
                    videogiocos.Remove(videogioco);
                    CC.DarkYellowFr("Videogioco rimosso");
                }

            }
            catch (VideogiocoNonEsistenteException) { }
        }

        // Visualizzazione videogiochi

        public void VisualizzaVideogiochi()
        {
            if (videogiocos.Count == 0) { CC.DarkYellowFr("Hai zero giochi :("); return; }
            spezzaTesto();
            int i = 0;
            foreach (var videogioco in videogiocos)
            {
                videogioco.VisualizzaInfo();
                if (videogiocos.IndexOf(videogioco) != videogiocos.Count - 1)
                {
                    CC.RedFr("-------------");
                }
            }
            spezzaTesto();
        }

        public void VisualizzaVideogiochi(String genere)
        {
            List<Videogioco> videogiocosGenere = videogiocos.FindAll(v => v.Genere.ToLower() == genere.ToLower());
            if (videogiocos.Count == 0) { CC.DarkYellowFr("Hai zero giochi :("); return; }
            if (videogiocosGenere.Count == 0) { CC.RedFr("Il genere non esiste"); return; }

            spezzaTesto();
            foreach (var videogioco in videogiocosGenere)
            {
                videogioco.VisualizzaInfo();
                if (videogiocosGenere.IndexOf(videogioco) != videogiocosGenere.Count - 1)
                {
                    CC.RedFr("--------");
                }
            }
            spezzaTesto();
        }

        public void VisualizzaVideogiochi(int punteggio)
        {
            List<Videogioco> videogiocosPunteggio = videogiocos.FindAll(v => v.Punteggio == punteggio);
            if (videogiocos.Count == 0) { CC.DarkYellowFr("Hai zero giochi :("); return; };
            if (videogiocosPunteggio.Count == 0) { CC.RedFr("Nessun gioco con questo punteggio"); return; }

            spezzaTesto();
            foreach (var videogioco in videogiocosPunteggio)
            {
                videogioco.VisualizzaInfo();
                if (videogiocosPunteggio.IndexOf(videogioco) != videogiocosPunteggio.Count - 1)
                {
                    CC.RedFr("--------");
                }
            }
            spezzaTesto();
        }

        // Statistiche

        // mostra per ogni genere la media punteggio
        public void VisualizzaMediaPunteggio()
        {
            List<String> generi = new List<String>();
            Dictionary<String, int> sommaPunteggi = new Dictionary<string, int>();
            Dictionary<String, double> mediaPunteggi = new Dictionary<string, double>();

            // ricerca generi e somma punteggi
            foreach (var v in videogiocos)
            {
                if (!generi.Contains(v.Genere))
                {
                    // il genere non esiste ancora
                    generi.Add(v.Genere);
                    // creo associazione Genere, Somma punteggi
                    sommaPunteggi.Add(v.Genere, 0);
                }
                // aggiorno somma per il genere
                sommaPunteggi[v.Genere] += v.Punteggio;
            }

            // calcolo media punteggi
            foreach (var g in generi)
            {
                List<Videogioco> count = videogiocos.FindAll(v => v.Genere == g);
                mediaPunteggi.Add(g, (double)sommaPunteggi[g] / count.Count);
            }

            // stampa punteggi
            foreach (var media in mediaPunteggi)
            {
                Console.WriteLine($"Genere: {media.Key} | Punteggio medio: {media.Value}");
            }

        }

        // mostra per ogni genere il numero di videogiochi
        public void VisualizzaNumeroGiochiGenere()
        {
            List<String> generi = new List<String>();
            Dictionary<String, int> occorrenzeGenere = new Dictionary<string, int>();

            // ricerca generi e somma occorrenze
            foreach (var v in videogiocos)
            {
                if (!generi.Contains(v.Genere))
                {
                    // il genere non esiste ancora
                    generi.Add(v.Genere);
                    // creo associazione Genere, Somma punteggi
                    occorrenzeGenere.Add(v.Genere, 0);
                }
                // aggiorno somma per il genere
                occorrenzeGenere[v.Genere] += 1;
            }


            // stampa punteggi
            foreach (var occ in occorrenzeGenere)
            {
                Console.WriteLine($"Genere: {occ.Key} | N. Giochi: {occ.Value}");
            }

        }

        // mostra numero giochi con punteggio maggiore di 
        public void VisualizzaNumGiochiPunteggio(int punteggio)
        {
            List<Videogioco> videogiohi = videogiocos.FindAll(v => v.Punteggio >= punteggio);

            Console.WriteLine($"{videogiohi.Count} giochi trovati con punteggio >= {punteggio}");

        }


    }
}
