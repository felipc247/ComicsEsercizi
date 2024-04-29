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
            stampaTesto("");

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
            finali[1] = "Tu:Il nostro contratto termina qui" +
                "\nGiocondo:si e come da esso stipulato ti riportero a casa tu, addio Briachella" +
                "\n** Finale Neutrale **";

            // Missione completata con affetto 100
            finali[2] = "(Giocondo ti racconta le sue vere intenzioni. Egli vuole uccidere Dioniso." +
                "\nIn nome della vostra profonda amicizia decidi di aiutarlo ad uccidere il dio." +
                "\nTornate sull'isola di Dioniso e uccidete il dio)" +
            "\n** Finale Plot twist **";
            // vettori per contenere i testi relativi ad ogni scenario

            // 4 scenari principali e 1 bonus (possibile)
            // 0 -> scenario1, 1 -> scenario2 a
            // 2 -> scenario2 b, 3 -> scenario3
            // 4 -> scenario 4, 5 -> bonus 1
            // bonus 2 non ha scelte
            int[] scenari_ScelteInt = new int[6];

            // lo uso per non dover scrivere ogni volta il numero scenari_ScelteInt = new int[numero]
            // in quanto il numero si ripete più volte per ogni scenario
            // posso semplicemente cambiarlo una sola volta con il cambio scenario
            int scenarioCor = 0;

            // scenario 1
            String[] scenario1 = new String[5];
            String[] scenario1_Scelte = new String[2];
            String[] scenario1_Risultato = new String[scenario1_Scelte.Length];

            scenario1[0] = "Raggiungi la tua prima tappa le Cime Argentee, una catena montuosa famosa per le sue vette ghiacciate e le cascate argentate che scendono lungo i versanti delle montagne,\n qiu risiedono i Gelidi Elfi Argentati, Una tribu di elfi con la pelle Argentea, adattati alla vita nelle terre fredde delle Cime Argentee,\n sono abili nella magia e conoscono tutti i segreti delle Montagne.";
            scenario1[1] = "Capo elfo:Benvenuto avventuriero, cosa ti porta qui da noi elfi?";
            scenario1[2] = "Tu:Grande capo elfo, sono giunto qui da voi per chiedere aiuto";
            scenario1[3] = "Capo elfo: Parla pure avventuriero,cerchero di aiutarti quanto possibile";
            scenario1[4] = "(pensa: riconosci che per quanto il capo elfo sia ospitale, non sei sicuro di ottenere la veste)\n";

            scenario1_Scelte[0] = "Sii diplomatico dicendo la verità ";
            scenario1_Scelte[1] = "Menti";


            scenario1_Risultato[0] = "Hai ottenuto una VESTE magica! Potrebbe tornare utile..." +
                "\nTu:Quello che le chiedo e di prestarmi una delle vostre Vesti, " +
                "\ncosi che Io possa ragiungere la mia destinazione senza il pericolo che il Miasma delle Ombre comporti." +
                "\nLe prometto che terminato il mio viagio la riportero da voi intatta." +
                "\nCapo Elfo:(sorpreso da la tua natura onorevole)Molto bene, guardie andatte a prendere una delle vesti per questo avventuriero," +
                "\nmi fido che manterrai la tua parola." + "\n(ora che sei in posseso della Veste procedi senza il timore del Miasma)" +
            "\n\n    _____\r\n    |/|\\|\r\n    / | \\\r\n   /  |  \\\r\n  /   |   \\     HAI OTTENUTO UNA VESTE MAGICA!\r\n /    |    \\\r\n/___  |  ___\\\r\n    \\_|_/";


            scenario1_Risultato[1] = "Tu:Sono in una avventura per ordine del Dio Dioniso e come suo Ambasciatore ti ordino di fornirmi una delle vostre Vesti." +
            "Capo Elfo:(Infastidito dalla tua caractere arrogante e bene a conoscenza del fatto che" +
            "\ndioniso non ha Abasciatori umani)" +
            "\nOsi mentire difronte a me misero umano, ti puoi scordare la Veste," +
            "\nritieniti fortunato che ti lascie adare via da questo villagio vivo." +
            "\n guardie portatelo via\n." +
            "(comprendi di aver' esagerato con la tua farsa e costretto a proseguire senza protezioni ti prepari per unlunfo e dificile viaggio)";

            // scenario 2
            String[] scenario2 = new String[3];
            String[] scenario2_Scelte = new String[3];
            String[] scenario2_Scelte2 = new String[3];
            String[] scenario2_Risultato = new String[scenario2_Scelte.Length];
            String[] scenario2_Risultato2 = new String[scenario2_Scelte2.Length];
            String scenario2_RisultatoIntro = "(Arrivi alle rovine ma ancora non sei al ricuro, vedi un gruppo di Orchi che sta venendo nella tua direzione)";

            scenario2[0] = "(Scendendo dalle Cime ti ritrovi nella Valle delle Ombre, un complesso di valli profonde e oscure circondato da alte vette rocciose,\n dove la lucie del sole filtra a malapena atraverso le creste delle montagne.\n Queste valli sono infestate dagli Orchi delle Gole Oscure, sono creature sotterranee con pelle scura e occhi luminescenti,\n maestri nell'arte di manipolare le ombre e intrinsecamente legati al mondo sotterraneo)";
            scenario2[1] = "(Sai che il tuo unico modo per sopravivere e superare la valle senza essere visto)";
            scenario2[2] = "(Senti un rumore, qualcosa si sta avicinando, vedi delle rovine in lontanaza)";


            scenario2_Scelte[0] = "Cerchi di correre fino alle rovine in lontanaza";
            scenario2_Scelte[1] = "Ti nascondi passando da dietro a vari massi e alberi per arivare alle rovine.";
            scenario2_Scelte[2] = "Usando la tua veste sei praticamente in visibile, ti incamini fino alla fine della valle.";

            scenario2_Scelte2[0] = "Continui a correre fino alla fine della Valle.";
            scenario2_Scelte2[1] = "Resta nascosto e aspetta un'opportunita per correre fino alla fine della Valle";
            scenario2_Scelte2[2] = "Entra nella porta";

            scenario2_Risultato[0] = "(Arrivi alle rovine ma ancora non sei al ricuro, vedi un gruppo di Orchi che sta venendo nella tua direzione)";
            scenario2_Risultato[1] = "(Arrivi alle rovine ma ancora non sei al ricuro, vedi un gruppo di Orchi che sta venendo nella tua direzione)";
            scenario2_Risultato[2] = "(Arrivi indenne alla prossima destinazione)";

            scenario2_Risultato2[0] = "(Inciampi e perdi una vita)";
            scenario2_Risultato2[1] = "(Arrivi indenne alla prossima destinazione)";
            scenario2_Risultato2[2] = "(Entri nella porta e ti avventuri nelle profondita delle rovine, noti in lontanaza un gruppo di figure incapucciate, sembrano star pregando intorno ad un fuoco)" +
                "\n(decidi di avvicinarti e quando ti notano uno di loro si avvicina per parlarti)" +
                "\nCultista Anziano:Viaggiatore cosa ti porta nelle rovine di Loth, signora delle ombre?" +
                "\nTu:Stavo scappando da un qruppo di Orchi e mi sono inbattuto nella porta di questo tempio." +
                "\nCultista Anziano:Ha, sono felice che tu sia riuscito ad arivare in un luogo sicuro." +
                "\nPerche non ti unisci a noi nella venerazione di Loth?" +
                "\nTu:No grazie, ho molto da fare, potresti indicarmi dove si trova l'uscita dal tempio?" +
                "\n(il Cultista ti mostra l'uscita e prosegui nel tuo viaggio)" +
                "\n\n AFFETTO + 40";

            // scenario 3
            String[] scenario3 = new String[6];
            String[] scenario3_Scelte = new String[4];
            String[] scenario3_Scelte2 = new String[4];
            String[] scenario3_Scelte3 = new String[4];
            String[] scenario3_Scelte4 = new String[4];
            String[] scenario3_Scelte5 = new String[4];
            int[] scenario3_RisposteCorrette = { 2, 4, 1, 1, 3 };
            int[] scenario3_ScelteRisposte = new int[5];
            String[] scenario3_Risultato = new String[2];


            scenario3[0] = "Finalmente esci dalla Valle delle Ombre e arrivai all'entrata dei Rifugi di Cristallo, un insieme di caverne con cristalli luminescenti di variabili sparsi per tutto esso.\n Dentro ci abitano dei Nani da cui dobiamo ottenere degli Ochiali di Cristallo d'Ombra.";
            scenario3[1] = "Guardia Nanica:Pellegrino, ben arrivato ai Cancelli, cosa ti porta nelle nostre grotte?";
            scenario3[2] = "Tu:Sono venuto qui per aquisire da voi degli Ochiali di Cristallo d'Ombra, potresti indicarmi dove trovarne un paio?";
            scenario3[3] = "(Ringrazi la guardia e vai verso la piazza centrale per partecipare all'evento.\n Stranamente nessun altro voleva partecipare quindi sei stato scelto.)";
            scenario3[4] = "Presentatore:Benvenuti alla centosettantotesima edizione di CHI VUOL'ESSERE NANICO, " +
                "\nil nostro partecipante di stavolta sarà questo pellegrino, che se dovesse rispondere correttamente a meno di tre domande morirà .Iniziamo?";
            scenario3[5] = "(Ti senti sotto pressione per la quantità di persone che ti guardano ma annuisci.)";

            scenario3_Scelte[0] = "Presentatore:Qual'è il nome della legendaria spada che fu estratta da una roccia?" +
                "\nAstraluce";
            scenario3_Scelte[1] = "Excalibur";
            scenario3_Scelte[2] = "Stellavvento";
            scenario3_Scelte[3] = "Kevin";


            scenario3_Scelte2[0] = "Presentatore:Qual è la creatura che quando muore risorge dalle proprie ceneri?" +
                "\nGlimmerFay";
            scenario3_Scelte2[1] = "Chimera";
            scenario3_Scelte2[2] = "Ippogrifo";
            scenario3_Scelte2[3] = "Fenice";

            scenario3_Scelte3[0] = "Presentatore:Quale razza magica è famosa per avere le orecchie a punta?" +
                "\nElfi";
            scenario3_Scelte3[1] = "Asini";
            scenario3_Scelte3[2] = "Drown";
            scenario3_Scelte3[3] = "Nani";

            scenario3_Scelte4[0] = "Presentatore:Quale dea e conosciuta come la Signora della Notte?" +
                "\nLoth";
            scenario3_Scelte4[1] = "Dioniso";
            scenario3_Scelte4[2] = "Shar";
            scenario3_Scelte4[3] = "Selun";
            scenario3_Scelte5[0] = "Presentatore:In cosa sono esperti i Nani" +
            "\n Arceria";
            scenario3_Scelte5[1] = "Pulizie";
            scenario3_Scelte5[2] = "Forgia";
            scenario3_Scelte5[3] = "Mineralogia";

            scenario3_Risultato[0] = "Presentatore:Complimenti ha risposto correttamente ad almeno 3 domande, come da stabilito il tuo premio saranno gli Occhiali di Cristallo d'Ombra." +
            "\n     _________      _________\r\n    /         \\    /         \\\r\n   |           |--|           |\r\n   |           |--|           |     HAI OTTENUTO GLI OCCHIALI !\r\n   |   _       |--|   _       |     \r\n    \\_________/    \\_________/";
            scenario3_Risultato[1] = "(I nani sono così schifati dalla tua performance che ti hanno condannato ad una morte atroce)";

            // scenario 4
            String[] scenario4 = new String[5];
            String[] scenario4_Scelte = new String[3];
            String[] scenario4_Risultato = new String[2];
            String[] scenario4_risposte = { "serpente", "luna", "enigma" };
            scenario4[0] = "Avendo ottenuto quello che cercavi procedi verso le Torri del Vento, delle torri di roccia che fanno da casa ai guardiano della Vela di Eolo";
            scenario4[1] = "(ti avicini all'entrata)";
            scenario4[2] = "Guardiano1:So il motivo del tuo arrivo, le preparazioni per la prova saranno finite a momenti";
            scenario4[3] = "(passa alcuni minuti ed un altro guardiano apre la porta)";
            scenario4[4] = "Guardiano2:Seguimi(il quardiano ti porta su una piattaforma circolare che inprovisamente viene sollevata dal vento portandoti all'ultimo piano della torre)\n Entra nella Sala dell Vento e affronta la prova";
            scenario4_Scelte[0] = "Sono lungo quanto un fiume, ma non sono d'acqua,\nnon ho occhi, ma posso vedere il mondo, \nmi muovo senza gambe, ma vado lontano, \ne se mi giri, divento invisibile all'istante.\nChi sono?";
            scenario4_Scelte[1] = "Nella notte sono illuminato, \nsenza fiamma, sono scaldato. \nNon ho corpo, ma ho un'ombra, \ne seguo sempre la tua traccia. \n\nChi sono?";
            scenario4_Scelte[2] = "Ho occhi che non vedono, \norecchie che non sentono, \ne una lingua che non parla. \nSono antica come il tempo, \nma sempre nuova.\nChi sono?";

            scenario4_Risultato[0] = "(Dal nulla appare davanti a te una vela con una decorazione argentata) \n Eolo:Risposta corretta, una barca e pronta per te al porto mettici questa vele e ti portera al tempio.\n(Scendi dalla torre e i guardiani ti portano ad una barca senza vele, gli aggiungi la Vela di Eolo e ti avvii verso l'isola)" +
            "\n    |\\\n    | \\\r\n    |  \\\r\n    |   \\\r\n    |    \\\r\n    |     \\     HAI OTTENUTO LA VELA DI EOLO!\r\n    |      \\    \r\n    |       \\\r\n     --------\r\n        ||\r\n    \\\"\"\"\"\"\"\"\"\"/\r\n     \"\"\"\"\"\"\"\"\"";
            scenario4_Risultato[1] = "Risposta errata, come punizione per la tua ingenuità dovrai remare fino a l'isola \n (Scendi dalla torre e i guardiani ti portano ad una barca senza vele, non avendo la vela sei costretto a remare fino all'isola " +
                "\n perdi due vite";
            //isolafinale
            String[] isolafinale = new string[5];
            isolafinale[0] = "Arrivi finalmente all'isola di Luminara, un'isola magica circondata da una luce perpetua, questa luce sembra provenire dall torre piu alta del tempio \n situato nel centro dell'isola";
            isolafinale[1] = "Giocondo:Ci siamo Briachella finalmente siamo arrivati sbrigriamoci ad entrare.";
            isolafinale[2] = "\n(Giocondo corre dentro al tempio, cerchi di stargli dietro fino a che non arrivate nella sala principale)";
            isolafinale[3] = "\n(Noti la spada sulle mani di una statua)";
            isolafinale[4] = "\nGiocondo:Finalmente C'è l'ho fatta!! Dopo cosi tanti anni." +
            "\n      ^\n     / \\    \n    /_ _\\\n    | | |\n    | | |\n    | | |   HAI OTTENUTO UNA SPADA!\n    | | |\n    | | |\n     \\|/\n    =====\n     |||\n    \"\"\"\"\"\n" +
            "Fine?";

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
            // bonus 2
            String[] bonus2 = new string[7];

            bonus2[0] = "(Entri nella porta e ti avventuri nelle profondita delle rovine, noti in lontanaza un gruppo di figure incapucciate, sembrano star pregando intorno ad un fuoco)";
            bonus2[1] = "(decidi di avvicinarti e quando ti notano uno di loro si avvicina per parlarti)";
            bonus2[2] = "Cultista Anziano:Viaggiatore cosa ti porta nelle rovine di Loth, signora delle ombre?";
            bonus2[3] = "Tu:Stavo scappando da un gruppo di Orchi e mi sono imbattuto nella porta di questo tempio.";
            bonus2[4] = "Cultista anziano:Ha, sono felice che tu sia riuscito ad arivare in un luogo sicuro.\n Perche non ti unisci a noi nella venerazione di Loth?";
            bonus2[5] = "Tu:No grazie, ho molto da fare, potresti indicarmi dove si trova l'uscita dal tempio?";
            bonus2[6] = "(il Cultista ti mostra l'uscita e prosegui nel tuo viaggio)";

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
                stampaTesto(scenario2_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
            }
            else if (scenari_ScelteInt[scenarioCor] == 2)
            {
                stampaTesto(scenario2_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
            }
            else
            {
                stampaTesto(scenario2_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
            }

            scenarioCor = 2;

            scenari_ScelteInt[scenarioCor] = scelta(scenario2_Scelte2);

            if (scenari_ScelteInt[scenarioCor] == 1)
            {
                perdiVita(ref vite);
                stampaTesto(scenario2_Risultato2[scenari_ScelteInt[scenarioCor] - 1]);
            }
            else if (scenari_ScelteInt[scenarioCor] == 2)
            {
                stampaTesto(scenario2_Risultato2[scenari_ScelteInt[scenarioCor] - 1]);
            }
            else
            {
                stampaTesto(scenario2_Risultato2[scenari_ScelteInt[scenarioCor] - 1]);
                incrementaAffetto(ref affetto, 40);
            }

            pausa(inventario, vite, affetto);

            spezzaTesto();

            // SCENARIO 3

            scenarioCor = 3;

            stampaScenario(scenario3, false);

            // QUIZ

            scenario3_ScelteRisposte[0] = scelta(scenario3_Scelte);
            scenario3_ScelteRisposte[1] = scelta(scenario3_Scelte2);
            scenario3_ScelteRisposte[2] = scelta(scenario3_Scelte3);
            scenario3_ScelteRisposte[3] = scelta(scenario3_Scelte4);
            scenario3_ScelteRisposte[4] = scelta(scenario3_Scelte5);

            int risposteCorrette = 0;
            for (int i = 0; i < scenario3_ScelteRisposte.Length; i++)
            {
                if (scenario3_ScelteRisposte[i] == scenario3_RisposteCorrette[i])
                {
                    risposteCorrette++;
                }
            }

            // setto scenari_ScelteInt

            if (risposteCorrette > 2)
            {
                scenari_ScelteInt[scenarioCor] = 1;
            }
            else
            {
                scenari_ScelteInt[scenarioCor] = 2;
            }

            if (scenari_ScelteInt[scenarioCor] == 1)
            {
                // Ottieni gli occhiali
                incrementaAffetto(ref affetto, 20);
                aggiungiOggetto(oggetti.OCCHIALI, ref inventario);
                stampaTesto(scenario3_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
            }
            else
            {
                stampaTesto(scenario3_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
                // perdi tutte le vite
                do
                {
                    if (!perdiVita(ref vite))
                    {
                        stampaTesto(finali[0]);
                        break;
                    }
                } while (true);
                stampaTesto("");
            }

            if (scenari_ScelteInt[scenarioCor] == 1)
            {
                pausa(inventario, vite, affetto);

                spezzaTesto();

                // SCENARIO 4

                scenarioCor = 4;

                stampaScenario(scenario4, false);

                String risposta = "";
                bool rispostaCorretta = false;

                // indovinello casuale
                switch (ra.Next(1, 3))
                {
                    case 1:
                        stampaTesto(scenario4_Scelte[0]);
                        risposta = Console.ReadLine();
                        if (risposta.Equals(scenario4_risposte[0])) rispostaCorretta = true;
                        break;
                    case 2:
                        stampaTesto(scenario4_Scelte[1]);
                        risposta = Console.ReadLine();
                        if (risposta.Equals(scenario4_risposte[1])) rispostaCorretta = true;
                        break;
                    case 3:
                        stampaTesto(scenario4_Scelte[2]);
                        risposta = Console.ReadLine();
                        if (risposta.Equals(scenario4_risposte[2])) rispostaCorretta = true;
                        break;
                }

                if (rispostaCorretta)
                {
                    scenari_ScelteInt[scenarioCor] = 1;
                }
                else
                {
                    scenari_ScelteInt[scenarioCor] = 2;
                }


                if (scenari_ScelteInt[scenarioCor] == 1)
                {
                    incrementaAffetto(ref affetto, affetto);
                    aggiungiOggetto(oggetti.VELA, ref inventario);
                    stampaTesto(scenario4_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
                }
                else
                {
                    perdiVita(ref vite);
                    perdiVita(ref vite);
                    // Fallimento, hai mentito e vieni cacciato
                    stampaTesto(scenario4_Risultato[scenari_ScelteInt[scenarioCor] - 1]);
                }

                if (vite == 0)
                {
                    // Sei morto
                    stampaTesto(finali[0]);
                }
                else
                {
                    pausa(inventario, vite, affetto);

                    spezzaTesto();

                    // ISOLA FINALE
                    stampaScenario(isolafinale, true);
                    aggiungiOggetto(oggetti.ARMA, ref inventario);

                    pausa(inventario, vite, affetto);

                    spezzaTesto();

                    // FINALE (in base ad affetto)

                    if (affetto < 100)
                    {
                        stampaTesto(finali[1]);
                    }
                    else
                    {
                        stampaTesto(finali[2]);
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
    }
}
