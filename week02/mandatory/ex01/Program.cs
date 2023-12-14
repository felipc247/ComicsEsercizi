using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfida_Algoritmica___Ordinamento
{
    internal class Program
    {
        static List<int> getPunteggi(String punteggi) {
            List<String> listPunteggiStr = new List<String>();
            List<int> listPunteggi = new List<int>();
            listPunteggiStr = punteggi.Split(' ').ToList();
            int value = -1;
            foreach (var c in listPunteggiStr)
            {
                // se l'utente inserisce qualcosa di errato si annulla l'operazione
                try
                {
                    if (!int.TryParse(c, out value)) throw new NonUnNumeroException();
                    if (value < 0) throw new PunteggioNonValidoException();
                    listPunteggi.Add(value);
                }
                catch (NonUnNumeroException)
                {
                    listPunteggiStr.Clear();
                    break;
                }
                catch (PunteggioNonValidoException) {
                    listPunteggiStr.Clear();
                    break;
                }
            }

            return listPunteggi;
        }
        
        private static Classifica classifica = new Classifica();

        static void Main(string[] args)
        {

            int numGiocatori = 15;

            // generazione random nomi da 3 char

            Random ra = new Random();

            for (int i = 0; i < numGiocatori; i++)
            {
                char[] nome = new char[3];
                nome[0] = (char)ra.Next(65, 91);
                nome[1] = (char)ra.Next(65, 91);
                nome[2] = (char)ra.Next(65, 91);
                classifica.AggiungiGiocatore(nome);
            }

            // stampo giocatori prima del torneo
            CC.BlueFr("Giocatori: \n");
            classifica.StampaClassifica();

            CC.DarkYellowFr("Numero giocatori = " + classifica.getCountGiocatori() + "\n");

            // inserimento punteggi giocatori, ordine inserimento uguale all'ordine dei giocatori
            // ripeto inserimento finchè non viene inserito il numero corretto di punteggi

            List<int> listPunteggi = new List<int>();
            do
            {
                CC.MagentaFr("Inserire i punteggi giocatori separati da uno spazio: ");
                try
                {
                    String punteggi = Console.ReadLine();
                    if (punteggi == "")
                    {
                        throw new StringVuotaException();
                    }
                    listPunteggi = getPunteggi(punteggi);
                    if (listPunteggi.Count != classifica.getCountGiocatori()) throw new NumeroPunteggiErratoException();
                    break;
                }
                catch (StringVuotaException) { }
                catch (NumeroPunteggiErratoException) { }

            } while (true);

            // inserisco i punteggi nella classifica 

            List<char[]> giocatori = classifica.GetGiocatori();

            for (int i = 0;i < giocatori.Count;i++)
            {
                classifica.AggiornaPunteggio(listPunteggi[i], giocatori[i]);
            }

            // ordino la classifica e stampo

            classifica.BubbleSort();

            CC.BlueFr("Classifica: \n");
            classifica.StampaClassifica();

        }
    }
}
