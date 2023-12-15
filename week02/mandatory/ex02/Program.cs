using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricerca_Avanzata_Dictionary
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

        static void aggiungiVideogiocoPiattaforme()
        {
            CC.BlueFr("Inserisci nome videogioco: ");

            try
            {
                String videogioco = Console.ReadLine();
                if (videogioco.Equals("")) throw new StringaVuotaException();
                if (catalogo.CheckGiocoEsistente(videogioco)) throw new GiocoEsistenteException();

                List<String> listPiattaforme = new List<string>();
                CC.CyanFr("\nInserisci piattaforme:\n");
                CC.BlueFr("(invio senza niente per terminare)\n");

                String piattaforma = "";
                do
                {
                    piattaforma = Console.ReadLine();
                    if (piattaforma.Equals("")) break;
                    if (listPiattaforme.Contains(piattaforma)) { printn(""); throw new PiattaformaEsistenteException(); }
                    listPiattaforme.Add(piattaforma);
                } while (true);

                // se non si inserisce neanche una piattaforma lancio errore
                if (listPiattaforme.Count == 0) throw new StringaVuotaException();

                catalogo.AggiungiGiocoCatalogo(videogioco, listPiattaforme);

                CC.GreenFr("Hai aggiunto il gioco: ");
                printn(videogioco);

                CC.DarkYellowFr("Hai aggiunto le seguenti piattaforme:\n");


                for (int i = 0; i < listPiattaforme.Count; i++)
                {
                    // Formattazione per migliore lettura
                    print("  ");
                    if (i % 2 == 0)
                    {
                        CC.DarkCyanFr(listPiattaforme[i]);
                    }
                    else
                    {
                        CC.CyanFr(listPiattaforme[i]);
                    }
                    printn("");
                }
            }
            catch (PiattaformaNonEsistenteException) { }
            catch (PiattaformaEsistenteException) { }
            catch (GiocoEsistenteException) { }
            catch (StringaVuotaException) { }
            finally { printn(""); }

        }

        static void ricercaPiattaforma()
        {
            CC.BlueFr("Inserisci nome piattaforma: ");

            try
            {
                String piattaforma = Console.ReadLine();
                if (piattaforma.Equals("")) throw new StringaVuotaException();

                List<String> listGiochi = catalogo.RicercaGiochiPiattaforma(piattaforma);
                if (listGiochi.Count == 0) throw new PiattaformaNonEsistenteException();

                printn("");
                CC.printColor(" " + piattaforma + " ", ConsoleColor.Magenta, ConsoleColor.White);
                printn("");

                for (int i = 0; i < listGiochi.Count; i++)
                {
                    // Formattazione per migliore lettura
                    print("  ");
                    if (i % 2 == 0)
                    {
                        CC.DarkCyanFr(listGiochi[i]);
                    }
                    else
                    {
                        CC.CyanFr(listGiochi[i]);
                    }
                    printn("");
                }
            }
            catch (PiattaformaNonEsistenteException) { }
            catch (StringaVuotaException) { }
            finally { printn(""); }
        }

        static void stampaCatalogo() {
            catalogo.stampaCatalogo();
        }

        static void stampaStatistiche() {
            catalogo.StampaStatistiche();
        }

        private static Catalogo catalogo = new Catalogo();

        static void Main(string[] args)
        {
            // Piattaforme totali
            List<string> listPiattaforme = new List<string>();
            listPiattaforme.Add("PixelOne");
            listPiattaforme.Add("GameForge");
            listPiattaforme.Add("QuantumEntertainment");
            listPiattaforme.Add("DreamGaming");
            listPiattaforme.Add("EpicDynasty");
            listPiattaforme.Add("InfiniteInteractive");
            listPiattaforme.Add("MegaGamerStudios");
            listPiattaforme.Add("NebulaGaming");

            // Piattaforme per i giochi inzialmente presenti nel catalogo
            // Il numero è scelto Random
            List<List<String>> piattaformeGioco = new List<List<String>>();

            Random ra = new Random();
            int numGiochi = 7;

            // inserimento casuale delle piattaforme

            for (int i = 0; i < numGiochi; i++)
            {
                // ogni gioco inzialmente presente è disponibile
                // in almeno 3 piattaforme su 8
                List<String> listPiattaformeTemp = new List<String>();
                for (int j = 0; j < ra.Next(3, 9); j++)
                {
                    // se la piattaforma è già presento riprovo
                    String piattaforma = listPiattaforme[ra.Next(0, listPiattaforme.Count)];
                    while (listPiattaformeTemp.Contains(piattaforma))
                    {
                        piattaforma = listPiattaforme[ra.Next(0, listPiattaforme.Count)];
                    }
                    listPiattaformeTemp.Add(piattaforma);
                }
                piattaformeGioco.Add(listPiattaformeTemp);
            }

            List<String> nomiGiochi = new List<String>();

            nomiGiochi.Add("Quantum Rift");
            nomiGiochi.Add("Stellar Odyssey");
            nomiGiochi.Add("Cyber Nexus");
            nomiGiochi.Add("Shadow Veil");
            nomiGiochi.Add("Galactic Conquest");
            nomiGiochi.Add("Elemental Ascension");
            nomiGiochi.Add("Elysian Legends");

            for (int i = 0; i < nomiGiochi.Count; i++)
            {
                catalogo.AggiungiGiocoCatalogo(nomiGiochi[i], piattaformeGioco[i]);
            }

            catalogo.stampaCatalogo();

            int scelta;

            do
            {
                scelta = -1;

                CC.CyanFr("\nCosa vuoi fare?\n");
                printn("1. Mostra giochi piattaforma\n" +
                    "2. Aggiungi un gioco e le sue piattaforme\n" +
                    "3. Mostra intero catalogo\n" +
                    "4. Mostra statistiche\n" +
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
                        ricercaPiattaforma();
                        break;
                    case 2:
                        aggiungiVideogiocoPiattaforme();
                        break;
                    case 3:
                        stampaCatalogo();
                        break;
                    case 4:
                        stampaStatistiche();
                        break;
                    default:
                        break;
                }
            } while (scelta != 0);


        }
    }
}
