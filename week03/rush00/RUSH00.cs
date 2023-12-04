using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RUSH00
{
    internal class Program
    {

        // stampa un testo fornito
        static void stampaTesto(String testo)
        {
            Console.WriteLine(testo);
        }

        // separatore di testi
        static void spezzaTesto()
        {
            Console.WriteLine("\n-----------------------------------------------------\n");
        }

        // rimuove una vita, se 0 ritorna false GAME OVER

        static bool perdiVita(ref int vite)
        {
            vite--;
            if (vite == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // incremento affetto di tot

        static void incrementaAffetto(ref int affetto, int incremento)
        {
            affetto += incremento;
            if (affetto > 100) affetto = 100;// non si può superare 100
        }

        static int scelta(String[] scelte)
        {
            int scelta = -1;
            // stampa scelte
            for (int i = 0; i < scelte.Length; i++)
            {
                stampaTesto($"{i + 1}." + scelte[i]);
            }

            // controllo sull'input, puoi inserire solo i numeri delle scelte
            do
            {
                stampaTesto("Cosa scegli?");
                try
                {
                    scelta = int.Parse(Console.ReadLine());
                    if (scelta > scelte.Length + 1 || scelta < 1) scelta = -1;
                    break;
                }
                catch
                {
                    stampaTesto("Input non idoneo");
                }
            } while (scelta == -1);

            return scelta;

        }

        // obbliga a cliccare un tasto per andare avanti
        static void pausa()
        {
            Console.ReadLine();
        }

        // ENUM per gli oggetti definiti
        enum oggetti
        {
            VUOTO = 0,
            VESTE = 1,
            OCCHIALI = 2,
            VELA = 3,
            ARMA = 4

        }

        // aggiungi l'oggetto all'inventario
        static void aggiungiOggetto(Enum oggetto, ref Enum[] inventario)
        {
            for (int i = 0; i < inventario.Length; i++)
            {
                if (inventario[i].Equals(oggetti.VUOTO))
                {
                    inventario[i] = oggetto;
                    stampaTesto("||| " + inventario[i]);
                    break;
                }
            }

            for (int i = 0; i < inventario.Length; i++)
            {
                stampaTesto("||| " + inventario[i]);
            }
        }


        static void Main(string[] args)
        {
            // inizializzazione
            int vite = 3;
            int affetto = 0; // MAX 100
            Enum[] inventario = new Enum[4];
            for (int i = 0; i < inventario.Length; i++)
            {
                inventario[i] = oggetti.VUOTO;
            }
            // testi

            // introduzione
            String[] introduzione = new string[4];
            introduzione[0] = "Il fuoco scoppiettava, e l'ebbrezza dell'amicizia mi spinse a una scommessa folle:\navventurarmi ubriaco nel bosco.\nTra le ombre, incontrai il Fauno Giocondo, e in quella notte di risate e scommesse, stringemmo un patto.\nDovevo aiutarlo a recuperare la spada magica Bacchusbane sull'Isola di Luminara.\nUn'epica avventura, un patto sigillato tra il vino e la promessa di un destino incerto.";
            introduzione[1] = "Tu: Quindi come proseguiamo da qui in poi?";
            introduzione[2] = "Giocondo: Allora Briachella, prima di tutto dovremo fare tappa dai Gelidi Elfi Argentati sulle Cime Argentate per ottenere la loro sacra veste, \npoi dobbiamo attraversare la Valle delle Ombre facendo attenzione agli Orchi delle Gole Oscure ,\nsuperata quella arriveremo ai Rifugi di Cristallo, casa dei Nani per ottenere degli occhiali di cristallo d'ombra,\ndopodichè dovremo affrontare l'indovinello della Torre del Vento dove i guardiani proteggono la sacra Vela di Eolo \n infine potremmo dirigerci all'isola di Luminara dove nel tempio si loca la sacra Spada Bacchusbane";
            introduzione[3] = "(annuisci e ti prepari ad affrontare questa fantastica avventura)";
            // vettori per contenere i testi relativi ad ogni scenario

            int[] scenari_ScelteInt = new int[6];

            // scenario 1
            String[] scenario1 = new string[4];
            String[] scenario1_Scelte = new string[2];
            String[] scenario1_Risultato = new string[scenario1_Scelte.Length];

            // 
            scenario1[0] = "Capo elfo:Benvenuto avventuriero, cosa ti porta qui da noi elfi?";
            scenario1[1] = "Tu:Grande capo elfo, sono giunto qui da voi per chiedere aiuto";
            scenario1[2] = "Capo elfo: Parla pure avventuriero,cerchero di aiutarti quanto possibile";
            scenario1[3] = "(pensa: riconosci che per quanto il capo elfo sia ospitale, non sei sicuro di ottenere la veste)\n";

            scenario1_Scelte[0] = "Sii diplomatico dicendo la verità ";
            scenario1_Scelte[1] = "Menti";

            scenario1_Risultato[0] = "Hai ottenuto una VESTE magica! Potrebbe tornare utile...";
            scenario1_Risultato[1] = "Mentire sembrava una buona idea, ma il Capo elfo non la pensava ugualmente.\nVieni cacciato dal villaggio e prosegui nell'avventura";

            // scenario 2
            String[] scenario2 = new string[3];
            String[] scenario3 = new string[3];
            String[] scenario4 = new string[3];

            // vettori per scenari bonus
            String[] bonus1 = new string[3];
            String[] bonus2 = new string[3];

            // INTRODUZIONE
            stampaTesto(introduzione[0]);

            pausa();

            stampaTesto(introduzione[1]);

            pausa();

            stampaTesto(introduzione[2]);

            pausa();

            stampaTesto(introduzione[3]);

            spezzaTesto();

            // PRIMO SCENARIO

            stampaTesto(scenario1[0]);

            pausa();

            stampaTesto(scenario1[1]);

            pausa();

            stampaTesto(scenario1[2]);

            pausa();

            stampaTesto(scenario1[3]);

            scenari_ScelteInt[0] = scelta(scenario1_Scelte);

            if (scenari_ScelteInt[0] == 1)
            {
                aggiungiOggetto(oggetti.VESTE, ref inventario);
                stampaTesto(scenario1_Risultato[scenari_ScelteInt[0] - 1]);
            }
            else
            {
                stampaTesto(scenario1_Risultato[scenari_ScelteInt[0] - 1]);
            }
        }
    }
}
