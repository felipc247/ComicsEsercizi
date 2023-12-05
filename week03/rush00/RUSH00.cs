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

        // passo bool come parametro poiché in alcuni casi non ho bisogno dell'ultima pausa
        // in quanto avendo la possibilità di chiedere cosa si vuole fare alla fine
        // l'ultima pausa deve essere sostituita da quella con parametri
        static void stampaScenario(String[] scenario, bool ultimaPausa)
        {
            for (int i = 0; i < scenario.Length; i++)
            {
                stampaTesto(scenario[i]);
                if (i == scenario.Length - 1)
                {
                    if (ultimaPausa) pausa();
                }
                else
                {
                    pausa();
                }
            }
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

        // stampa le scelte disponibili, prende input la risposta e la restituisce 

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
                    if (scelta > scelte.Length || scelta < 1)
                    {
                        scelta = -1;
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    stampaTesto("Input non idoneo");
                }
            } while (scelta == -1);

            return scelta;

        }

        static void pausa()
        {
            Console.ReadKey();
            stampaTesto("");
        }

        // obbliga a cliccare un tasto per andare avanti
        static void pausa(Enum[] inventario, int vite, int affetto)
        {
            stampaTesto("\nCosa vuoi fare:" +
                "\n'INVIO' continua" +
                "\n'i' mostra inventario" +
                "\n'v' mostra vite" +
                "\n'a' mostra affetto" +
                "\n's' mostra tutto");
            // prendo char da input
            char inputChar = Console.ReadKey().KeyChar;
            stampaTesto("\n");
            // ottengo l'ASCII corrispondente
            int asciiValue = (int)inputChar;
            // in base all'input si esegue un'azione
            switch (asciiValue)
            {
                case 13:
                    // "invio" si prosegue
                    break;
                case 115:
                    // "s" mostra tutto
                    stampaTesto("Inventario:");
                    mostraInventario(inventario);
                    stampaTesto("Vite:");
                    switch (vite)
                    {
                        case 1:
                            stampaTesto(" __  __ \r\n/  \\/  \\\r\n\\      /\r\n \\    / \r\n  ''''  ");
                            break;
                        case 2:
                            stampaTesto(" __  __       __  __ \r\n/  \\/  \\     /  \\/  \\\r\n\\      /     \\      /\r\n \\    /       \\    / \r\n  ''''         ''''  ");
                            break;
                        case 3:
                            stampaTesto(" __  __       __  __       __  __\r\n/  \\/  \\     /  \\/  \\     /  \\/  \\\r\n\\      /     \\      /     \\      /\r\n \\    /       \\    /       \\    /\r\n  ''''         ''''         ''''");
                            break;
                    }
                    stampaTesto("Affetto = " + affetto);
                    break;
                case 105:
                    // "i"
                    stampaTesto("Inventario:");
                    mostraInventario(inventario);
                    break;
                case 118:
                    // "v" mostra vite
                    stampaTesto("Vite:");
                    switch (vite)
                    {
                        case 1:
                            stampaTesto(" __  __ \r\n/  \\/  \\\r\n\\      /\r\n \\    / \r\n  ''''  ");
                            break;
                        case 2:
                            stampaTesto(" __  __       __  __ \r\n/  \\/  \\     /  \\/  \\\r\n\\      /     \\      /\r\n \\    /       \\    / \r\n  ''''         ''''  ");
                            break;
                        case 3:
                            stampaTesto(" __  __       __  __       __  __\r\n/  \\/  \\     /  \\/  \\     /  \\/  \\\r\n\\      /     \\      /     \\      /\r\n \\    /       \\    /       \\    /\r\n  ''''         ''''         ''''");
                            break;
                    }
                    break;
                case 97:
                    // "a" mostra affetto
                    stampaTesto("Affetto = " + affetto);
                    break;

            }

            // se ho premuto invio vado avanti, altrimenti
            if (asciiValue != 13) pausa(inventario, vite, affetto);

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
                    break;
                }
            }
        }

        static void mostraInventario(Enum[] inventario)
        {
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
            Random ra = new Random();

            // TESTI

            // introduzione
            String[] introduzione = new string[4];
            introduzione[0] = "Il fuoco scoppiettava, e l'ebbrezza dell'amicizia mi spinse a una scommessa folle:" +
                "\navventurarmi ubriaco nel bosco." +
                "\nTra le ombre, incontrai il Fauno Giocondo, e in quella notte di risate e scommesse, stringemmo un patto." +
                "\nDovevo aiutarlo a recuperare la spada magica Bacchusbane sull'Isola di Luminara." +
                "\nUn'epica avventura, un patto sigillato tra il vino e la promessa di un destino incerto.";

            introduzione[1] = "Tu: Quindi come proseguiamo da qui in poi?";

            introduzione[2] = "Giocondo: Allora Briachella, " +
                "prima di tutto dovremo fare tappa dai Gelidi Elfi Argentati" +
                "\nsulle Cime Argentate per ottenere la loro sacra veste," +
                "\npoi dobbiamo attraversare la Valle delle Ombre facendo attenzione agli Orchi delle Gole Oscure ," +
                "\nsuperata quella arriveremo ai Rifugi di Cristallo, casa dei Nani per ottenere degli occhiali di cristallo d'ombra," +
                "\ndopodichè dovremo affrontare l'indovinello della Torre del Vento dove i guardiani proteggono la sacra Vela di Eolo" +
                "\ninfine potremmo dirigerci all'isola di Luminara dove nel tempio si loca la sacra Spada Bacchusbane";

            introduzione[3] = "(annuisci e ti prepari ad affrontare questa fantastica avventura)";

            // finali
            String[] finali = new String[3];

            // vite finite
            finali[0] = "Le forze ti hanno abbandonato, forse è il momento di andare in palestra?\n" +
                "** Vatti ad allenare, pirla **";

            // Missione completata con affetto 60 
            finali[1] = "Tu:Il nostro contratto termina qui \n" +
                "Giocondo:si e come da esso stipulato ti riportero a casa tu, addio Briachella \n" +
               "** Finale Neutrale **";

            // Missione completata con affetto 100
            finali[2] = "Tu:Sembi fin'troitato, hai un piano in mento vero?\nppo ec" +
                "Giocondo:Ormai mi conosci, se devo essere onesto con te questa spada non la voleva Dioniso,\n" +
                "la stavo cercando io per poter uccidere Dioniso e prendere il suo posto vuoi aiutarmi?\n";

            // vettori per contenere i testi relativi ad ogni scenario

            // 4 scenari principali e 1 bonus (possibile)
            // 0 -> scenario1, 1 -> scenario2
            // 2 -> scenario3, 3 -> scenario4
            // 4 -> bonus1, 5 -> bonus 2
            int[] scenari_ScelteInt = new int[6];

            // lo uso per non dover scrivere ogni volta il numero scenari_ScelteInt = new int[numero]
            // in quanto il numero si ripete più volte per ogni scenario
            // posso semplicemente cambiarlo una sola volta con il cambio scenario
            int scenarioCor = 0;

            // scenario 1
            String[] scenario1 = new String[4];
            String[] scenario1_Scelte = new String[2];
            String[] scenario1_Risultato = new String[scenario1_Scelte.Length];


            scenario1[0] = "Capo elfo:Benvenuto avventuriero, cosa ti porta qui da noi elfi?";
            scenario1[1] = "Tu:Grande capo elfo, sono giunto qui da voi per chiedere aiuto";
            scenario1[2] = "Capo elfo: Parla pure avventuriero,cerchero di aiutarti quanto possibile";
            scenario1[3] = "(pensa: riconosci che per quanto il capo elfo sia ospitale, non sei sicuro di ottenere la veste)\n";

            scenario1_Scelte[0] = "Sii diplomatico dicendo la verità ";
            scenario1_Scelte[1] = "Menti";


            scenario1_Risultato[0] = "Hai otteto.nuto una VESTE magica! Potrebbe tornare utile..." +
                "\nTu:Quello che le chiedo e di prestarmi Una delle vostre Vesti, " +
                "\ncosi che Io possa ragiungere la mia destinazione senza il pericolo che il Miasma delle Ombre comporti." +
                "\nLe prometto che terminato il mio viagio la riportero da voi intatta." +
                "\nCapo Elfo:(sorpreso da la tua natura onorevole)Molto bene, guardie andatte a prendere una delle vesti per questo avventuriero,\n mi fido che manterrai la tua parola." + "\n(ora che sei in posseso della Veste procedi senza il timore del Miasma)";
            scenario1_Risultato[1] = "Tu:Sono in una avventura per ordine del Dio Dioniso e come suo Ambasciatore ti ordino di fornirmi una delle vostre +\nVesti." +
            "Capo Elfo:(Infastidito dalla tua caractere arrogante e bene a conoscenza del fatto che" +
            "\ndioniso non ha Abasciatori umani)" +
            "\nOsi mentire difronte a me misero umano, ti puoi scordare la Veste," +
            "\nritieniti fortunato che ti lascie adare via da questo villagio vivo." +
            "\n guardie portatelo via\n." +
            "(comprendi di aver' esagerato con la tua farsa e costretto a proseguire senza protezioni ti prepari per unlunfo e dificile viaggio)";

            // scenario 2
            String[] scenario2 = new String[3];
            String[] scenario2_Scelte = new String[3];
            String[] scenario2_Risultato = new String[scenario1_Scelte.Length];

            scenario2[0] = "(Scendendo dalle Cime ti ritrovi nella Valle delle Ombre, un complesso di valli profonde e oscure circondato da alte vette rocciose,\n dove la lucie del sole filtra a malapena atraverso le creste delle montagne.\n Queste valli sono infestate dagli Orchi delle Gole Oscure, sono creature sotterranee con pelle scura e occhi luminescenti,\n maestri nell'arte di manipolare le ombre e intrinsecamente legati al mondo sotterraneo)";
            scenario2[1] = "(Sai che il tuo unico modo per sopravivere e superare la valle senza essere visto)";
            scenario2[2] = "(Senti un rumore, qualcosa si sta avicinando, vedi delle rovine in lontanaza)";


            scenario2_Scelte[0] = "Cerchi di correre fino alle rovine in lontanaza";
            scenario2_Scelte[1] = "Ti nascondi passando da dietro a vari massi e alberi per arivare alle rovine.";
            scenario2_Risultato[2] = "Usando la tua veste sei praticamente in visibile, ti incamini fino alla fine della valle";


            // scenario 3
            String[] scenario3 = new String[3];
            String[] scenario3_Scelte = new String[3];
            String[] scenario3_Risultato = new String[scenario1_Scelte.Length];

            scenario3[0] = "";
            scenario3[1] = "";
            scenario3[2] = "";

            scenario3_Scelte[0] = "";
            scenario3_Scelte[1] = "";

            scenario3_Risultato[0] = "";
            scenario3_Risultato[1] = "";

            // scenario 4
            String[] scenario4 = new String[3];
            String[] scenario4_Scelte = new String[3];
            String[] scenario4_Risultato = new String[scenario1_Scelte.Length];

            scenario4[0] = "";
            scenario4[1] = "";
            scenario4[2] = "";

            scenario4_Scelte[0] = "";
            scenario4_Scelte[1] = "";

            scenario4_Risultato[0] = "";
            scenario4_Risultato[1] = "";

            // scenari bonus

            // bonus 1
            String[] bonus1 = new string[1];
            String[] bonus1_Scelte = new String[2];
            String[] bonus1_Risultato = new String[bonus1_Scelte.Length];

            bonus1[0] = "(notì una ragazza molto carina che cattura la tua attenzione," +
                "\nti distrae al punto che non ti accorgi che ormai la stai fissando da parechio tempo.)";

            bonus1_Scelte[0] = "Avvicinati";
            bonus1_Scelte[1] = "Meglio lasciar perdere";

            bonus1_Risultato[0] = "Ragaza elfica:Guarda cosa abiamo qui, un avventuriro un po troppo curioso. (Procede col prendere una padella e ti colpiscie in testa con essa)" +
            "\n(ti risvegli dopo qualche minutio confuso e continui nella col tuo obbietivo)";
            bonus1_Risultato[1] = "(Quando torni in te decidi di non avicinarti)";

            // bonus 2
            String[] bonus2 = new string[3];
            String[] bonus2_Scelte = new String[2];
            String[] bonus2_Risultato = new String[bonus1_Scelte.Length];

            bonus2[0] = "";

            bonus2_Scelte[0] = "";
            bonus2_Scelte[1] = "";

            bonus2_Risultato[0] = "";
            bonus2_Risultato[1] = "";

            // INTRODUZIONE

            stampaScenario(introduzione, true);

            spezzaTesto();

            //SCENARIO 1

            scenarioCor = 0;

            stampaScenario(scenario1, false);

            scenari_ScelteInt[scenarioCor] = scelta(scenario1_Scelte);

            if (scenari_ScelteInt[scenarioCor] == 1)
            {
                // Successo, la diplomazia convince il capo elfo che ti aiuta dandoti la Veste
                aggiungiOggetto(oggetti.VESTE, ref inventario);
                // ottieni 20 affetto
                incrementaAffetto(ref affetto, 20);
                stampaTesto(scenario1_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
                stampaTesto($"\nAffetto + 20");
            }
            else
            {
                // Fallimento, hai mentito e vieni cacciato
                stampaTesto(scenario1_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
            }

            pausa(inventario, vite, affetto);

            spezzaTesto();

            // BONUS 1 (casuale)

            // 33% di possibilità di incontrare l'elfa
            if (ra.Next(1, 3) == 1)
            {
                scenarioCor = 4;

                stampaScenario(bonus1, true);

                scenari_ScelteInt[scenarioCor] = scelta(bonus1_Scelte);

                if (scenari_ScelteInt[scenarioCor] == 1)
                {
                    // Fallimento, vieni padellato e perdi 1 vita (ahia)
                    perdiVita(ref vite);
                    stampaTesto(bonus1_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
                }
                else
                {
                    // Successo, l'indifferenza fa la differenza a volte, ignori l'elfa
                    stampaTesto(bonus1_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
                }
                pausa(inventario, vite, affetto);

                spezzaTesto();
            }



            //DONE invece di cliccare qualsiasi tasto
            // mettiamo dei pulsanti per controllare le vite, oggetti etc...
            // si prosegue con invio o space

            //TODO le scelte e gli scenari(?) cambiano in base agli oggetti nell'inventario
            // sistema per cambiare scenari in base all'inventario

            // SCENARIO 2

            scenarioCor = 1;

            stampaScenario(scenario2, false);

            scenari_ScelteInt[scenarioCor] = scelta(scenario2_Scelte);

            if (scenari_ScelteInt[scenarioCor] == 1)
            {

                // ottieni 20 affetto
                incrementaAffetto(ref affetto, 20);
                stampaTesto(scenario2_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
                stampaTesto($"\nAffetto + 20");
            }
            else
            {
                // Fallimento, hai mentito e vieni cacciato
                stampaTesto(scenario2_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
            }

            pausa(inventario, vite, affetto);

            spezzaTesto();

            // SCENARIO 3

            scenarioCor = 2;

            stampaScenario(scenario3, false);

            scenari_ScelteInt[scenarioCor] = scelta(scenario3_Scelte);

            if (scenari_ScelteInt[scenarioCor] == 1)
            {

                // ottieni 20 affetto
                incrementaAffetto(ref affetto, 20);
                stampaTesto(scenario3_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
                stampaTesto($"\nAffetto + 20");
            }
            else
            {
                // Fallimento, hai mentito e vieni cacciato
                stampaTesto(scenario3_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
            }

            pausa(inventario, vite, affetto);

            spezzaTesto();

            // SCENARIO 4

            scenarioCor = 3;

            stampaScenario(scenario4, false);

            scenari_ScelteInt[scenarioCor] = scelta(scenario4_Scelte);

            if (scenari_ScelteInt[scenarioCor] == 1)
            {

                // ottieni 20 affetto
                incrementaAffetto(ref affetto, 20);
                stampaTesto(scenario4_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
                stampaTesto($"\nAffetto + 20");
            }
            else
            {
                // Fallimento, hai mentito e vieni cacciato
                stampaTesto(scenario4_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
            }

            pausa(inventario, vite, affetto);

            spezzaTesto();

            // FINALE (in base ad affetto)

            switch (affetto)
            {
                case 60:
                    stampaTesto(finali[1]);
                    break;
                case 100:
                    stampaTesto(finali[2]);
                    break;
            }

            // RIASSUNTO

            // Scenario 1
            stampaTesto("");
            if (scenari_ScelteInt[0] == 1)
            {
                stampaTesto("");
            }
            else
            {

            }



            // BONUS 2



        }
    }
}

