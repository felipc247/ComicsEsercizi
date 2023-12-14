using Archivio_videogiochi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_Videogiochi
{
    internal class Program
    {
        static void printn(String mes)
        {
            Console.WriteLine(mes);
        }

        static void print(String mes)
        {
            Console.Write(mes);
        }

        // aggiunta videogioco con controlli
        static void aggiungiVideogioco()
        {
            String nome = "";
            String genere = "";
            int punteggio = -1;

            try
            {
                print("Inserisci Nome: ");
                nome = Console.ReadLine();
                if (nome.Equals("")) throw new StringaVuotaException();

                print("Inserisci Genere: ");
                genere = Console.ReadLine();
                if (genere.Equals("")) throw new StringaVuotaException();

                print("Inserisci Punteggio: ");
                if (!int.TryParse(Console.ReadLine(), out punteggio)) throw new NumeroNonValidoException();

                printn("");

                archivio.AggiungiVideogioco(new Videogioco(nome, genere, punteggio));
            }
            catch (StringaVuotaException) { }
            catch (PunteggioNonValidoException) { }
            catch (NumeroNonValidoException) { }
            finally { printn(""); }
        }

        // visualizza elenco, Completo o per criterio (punteggio, genere)
        static void visualizzaElenco()
        {
            CC.BlueFr("Cosa vuoi visualizzare?");
            printn("1. elenco completo\n" +
                "2. elenco by genere\n" +
                "3. elenco by punteggio");

            int scelta = -1;
            try
            {
                printn("");
                if (int.TryParse(Console.ReadLine(), out scelta))
                {
                    if (scelta < 1 || scelta > 3) throw new NumeroNonValidoException();
                    printn("");
                    switch (scelta)
                    {
                        case 1:
                            archivio.VisualizzaVideogiochi();
                            break;
                        case 2:
                            print("Inserisci genere: ");
                            String genere = "";
                            genere = Console.ReadLine();
                            if (genere.Equals("")) throw new StringaVuotaException();
                            printn("");
                            archivio.VisualizzaVideogiochi(genere);
                            break;
                        case 3:
                            print("Inserisci Punteggio: ");
                            int punteggio = -1;
                            if (!int.TryParse(Console.ReadLine(), out punteggio)) throw new NumeroNonValidoException();
                            if (punteggio < 1 || punteggio > 10) throw new PunteggioNonValidoException();
                            printn("");
                            archivio.VisualizzaVideogiochi(punteggio);
                            break;
                    }
                }
                else
                {
                    throw new NumeroNonValidoException();
                }

            }
            catch (NumeroNonValidoException) { }
            catch (StringaVuotaException) { }
            catch (PunteggioNonValidoException) { }
            finally { printn(""); }
        }

        static void visualizzaStatistiche()
        {
            CC.BlueFr("Cosa vuoi visualizzare?");
            printn("1. media punteggi by genere\n" +
                "2. numero di videogiochi by genere\n" +
                "3. numero di videogiochi (punteggio >= punteggioInserito)");

            int scelta = -1;
            try
            {
                if (scelta < 1 || scelta > 3) throw new NumeroNonValidoException();
                printn("");
                if (int.TryParse(Console.ReadLine(), out scelta))
                {
                    printn("");
                    switch (scelta)
                    {
                        case 1:
                            archivio.VisualizzaMediaPunteggio();
                            break;
                        case 2:
                            archivio.VisualizzaNumeroGiochiGenere();
                            break;
                        case 3:
                            print("Inserisci Punteggio: ");
                            int punteggio = -1;
                            if (!int.TryParse(Console.ReadLine(), out punteggio)) throw new PunteggioNonValidoException();
                            if (punteggio < 1 || punteggio > 10) throw new NumeroNonValidoException();
                            printn("");
                            archivio.VisualizzaNumGiochiPunteggio(punteggio);
                            break;
                    }
                }
                else
                {
                    throw new NumeroNonValidoException();
                }

            }
            catch (NumeroNonValidoException) { }
            catch (StringaVuotaException) { }
            catch (PunteggioNonValidoException) { }
            finally { printn(""); }
        }

        static void rimuoviVideogioco()
        {
            try
            {
                print("Inserisci nome videogioco: ");
                String nome = "";
                nome = Console.ReadLine();
                if (nome.Equals("")) throw new StringaVuotaException();

                printn("");

                archivio.RimuoviVideogioco(nome);

                printn("");
            }
            catch (StringaVuotaException) { }
        }

        // variabile globale per program cs
        private static Archivio archivio = new Archivio("Videogames");

        static void Main(string[] args)
        {
            //archivio.AggiungiVideogioco(new Videogioco("SpyAnya7", "Spy wars", 10));
            //archivio.AggiungiVideogioco(new Videogioco("SpyAnya13", "Spy peanuts", 9));
            //archivio.AggiungiVideogioco(new Videogioco("SpyAnya3", "Spy peanuts", 6));
            //archivio.AggiungiVideogioco(new Videogioco("SpyAnya17", "Spy wars", 7));
            //archivio.AggiungiVideogioco(new Videogioco("SpyAnya21", "Spy mission", 3));

            int scelta;

            do
            {
                scelta = -1;

                CC.GreenFr("Cosa vuoi fare?");
                printn("1. Inserisci videogioco\n" +
                "2. Visualizza elenco\n" +
                "3. Mostra statistiche\n" +
                "4. Rimuovi videogioco\n" +
                "0. Esci");
                printn("");
                try
                {
                    String sceltaStr = "";
                    sceltaStr = Console.ReadLine();
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();

                    if (!int.TryParse(sceltaStr, out scelta)) throw new NumeroNonValidoException();
                    if (scelta < 0 || scelta > 4) throw new NumeroNonValidoException();

                }
                // nonostante lanci l'errore se non si inserisce un numero intero, tryParse modifica scelta a 0 con esito negativo
                // perciò pongo direttamente scelta a -1
                catch (NumeroNonValidoException) { scelta = -1; }
                catch (StringaVuotaException) { }

                printn("");

                switch (scelta)
                {
                    case 1:
                        aggiungiVideogioco();
                        break;

                    case 2:
                        visualizzaElenco();
                        break;
                    case 3:
                        visualizzaStatistiche();
                        break;
                    case 4:
                        rimuoviVideogioco();
                        break;
                    default:
                        break;
                }
            } while (scelta != 0);

        }
    }
}
