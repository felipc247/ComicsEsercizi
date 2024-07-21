using Simple_RTS.Exceptions.Input;
using Simple_RTS.Exceptions;
using Simple_RTS.Truppe;
using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_RTS.Exceptions.Truppe;
using Simple_RTS.Exceptions.Frammenti;
using Simple_RTS.Eroi;
using Simple_RTS.Exceptions.Truppe_Elite;

namespace Simple_RTS.Base
{
    // Dove si assoldano Truppe
    internal class Arena
    {
        private Giocatore giocatore;

        public Arena()
        {

        }

        public void SetUp(Giocatore giocatore)
        {
            this.giocatore = giocatore;
        }

        public void PrintTruppeAssoldaTruppe(List<Truppa> truppas)
        {
            int i = 1;
            switch (truppas[0].Tipologia)
            {
                case Truppa.TT.Mischia:
                    CC.DarkYellowFr("Truppe Mischia:\n");
                    foreach (var t in truppas)
                    {
                        CC.YellowFr($"{i++}. {t.Nome}");
                        CC.BlueFr($" - {t.Costo} Frammenti");
                        CC.WhiteFr($" | Qt. {t.Quantita} ({t.Disponibili} disponibili, {t.Ferite} ferite)\n");
                    }
                    break;
                case Truppa.TT.Distanza:
                    CC.DarkCyanFr("Truppe Distanza:\n");
                    foreach (var t in truppas)
                    {
                        CC.CyanFr($"{i++}. {t.Nome}");
                        CC.BlueFr($" - {t.Costo} Frammenti");
                        CC.WhiteFr($" | Qt. {t.Quantita} ({t.Disponibili} disponibili, {t.Ferite} ferite)\n");
                    }
                    break;
                case Truppa.TT.Tank:
                    CC.DarkMagentaFr("Truppe Tank:\n");
                    foreach (var t in truppas)
                    {
                        CC.MagentaFr($"{i++}. {t.Nome}");
                        CC.BlueFr($" - {t.Costo} Frammenti");
                        CC.WhiteFr($" | Qt. {t.Quantita} ({t.Disponibili} disponibili, {t.Ferite} ferite)\n");
                    }
                    break;
            }
        }

        public int AssoldaTruppe(List<Truppa> truppe)
        {
            int scelta;
            int truppeCount = truppe.Count;
            do
            {
                scelta = -1;
                try
                {
                    // Stampa Classi Truppe della Tipologia scelta
                    PrintTruppeAssoldaTruppe(truppe);
                    CC.BlueFr("0. Indietro\n");
                    CC.BlueFr($"{truppeCount + 1}. Frammenti\n");

                    CC.CyanFr("Inserisci 'numTruppa'.'Quantita'\n" +
                        "Invio e ripeti per altre Truppe\n" +
                        "Invio senza nulla per terminare\n");

                    int qt = 0;
                    // per salvare le truppe e le quantita scelta dall'utente
                    Dictionary<int, int> qts = new Dictionary<int, int>();

                    String sceltaStr;

                    bool first = true;

                    do
                    {
                        // lettura scelta
                        sceltaStr = Console.ReadLine();

                        if (first)
                        {
                            // se al primo input non si inserisce nulla
                            // allora exception
                            first = false;
                            if (sceltaStr.Equals("")) throw new StringaVuotaException();
                        }
                        else
                        {
                            // se non si inserisce nulla dopo, allora fine input
                            if (sceltaStr.Equals("")) break;
                        }

                        // Tentativo di split
                        String[] input = sceltaStr.Split('.');

                        // gestione eccezioni input

                        if (!int.TryParse(input[0], out scelta)) throw new InputNonValidoException();
                        if (scelta < 0 || scelta > truppeCount + 1) throw new NumeroNonValidoException();
                        if (scelta == 0) break;
                        if (scelta == truppeCount + 1) break;
                        if (input.Length != 2) throw new FormattazioneNonValidaException();
                        if (!int.TryParse(input[1], out qt)) throw new InputNonValidoException();

                        // se tutto va bene aggiungo al Dictionary
                        if (qts.ContainsKey(scelta))
                        {
                            qts[scelta] += qt;
                        }
                        else
                        {
                            qts.Add(scelta, qt);
                        }

                    } while (true);

                    // indietro scelta == 0
                    if (scelta == 0) break;

                    // Mostro Frammenti e ripeto il ciclo
                    if (scelta == truppeCount + 1)
                    {
                        CC.GreenFr("Frammenti " + giocatore.Fornace.Frammenti + "\n");
                        scelta = -1;
                    }
                    else
                    {
                        // Stampa riepilogo assoldamento
                        int totale = 0;
                        int totale_Frammenti = 0;

                        CC.BlueFr("Assoldare queste Truppe?\n");
                        foreach (var item in qts)
                        {
                            CC.CyanFr($"{truppe[item.Key - 1].Nome}"); CC.WhiteFr($" => {item.Value}\n");
                            totale += item.Value;
                            totale_Frammenti += truppe[item.Key - 1].Costo * item.Value;
                        }

                        if (totale_Frammenti < 0) throw new NumeroNonValidoException();
                        CC.DarkYellowFr($"Totale: {totale} Truppe => ");
                        CC.BlueFr($"{totale_Frammenti} Frammenti\n");

                        CC.WhiteFr("1. Si\n" +
                            "2. No\n");

                        sceltaStr = Console.ReadLine();
                        if (sceltaStr.Equals("")) throw new StringaVuotaException();
                        if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                        if (scelta < 1 || scelta > 2) throw new NumeroNonValidoException();

                        // Si
                        if (scelta == 1)
                        {
                            // Frammenti insufficienti
                            if (giocatore.Fornace.Frammenti < totale_Frammenti) throw new FrammentiInsufficientiException();

                            int frammenti = giocatore.Fornace.Frammenti;
                            giocatore.Fornace.Frammenti = -totale_Frammenti;
                            CC.YellowFr($"Frammenti {frammenti} => {giocatore.Fornace.Frammenti}\n");

                            foreach (var item in qts)
                            {
                                truppe[item.Key - 1].Disponibili = +item.Value;
                            }
                        }
                    }
                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FormattazioneNonValidaException) { scelta = -1; }
                catch (TruppeInsufficientiException) { }
                catch (FrammentiInsufficientiException) { }
                finally { if (scelta < 0 || scelta > 2) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        private void PrintTipologieTruppa()
        {
            int i = 1;

            // TRUPPE MISCHIA
            CC.DarkYellowFr($"{i++}. {giocatore.Truppe_Mischia[0].Tipologia}\n");

            // TRUPPE DISTANZA
            CC.DarkCyanFr($"{i++}. {giocatore.Truppe_Distanza[0].Tipologia}\n");

            // TRUPPE TANK
            CC.DarkMagentaFr($"{i++}. {giocatore.Truppe_Tank[0].Tipologia}\n");
        }

        public void PrintAssoldaTruppe()
        {
            CC.DarkGreenFr("||Assolda Truppe||\n");
            int scelta;
            int numTipologie = 3;
            do
            {
                scelta = -1;
                try
                {

                    // Stampa Truppe
                    CC.CyanFr("Cosa vuoi assoldare?\n");
                    PrintTipologieTruppa();
                    CC.BlueFr($"{numTipologie + 1}. Avanti\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numTipologie + 1) throw new NumeroNonValidoException();

                    // Avanti
                    if (scelta == numTipologie + 1) break;

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            scelta = AssoldaTruppe(giocatore.Truppe_Mischia);
                            break;
                        case 2:
                            scelta = AssoldaTruppe(giocatore.Truppe_Distanza);
                            break;
                        case 3:
                            scelta = AssoldaTruppe(giocatore.Truppe_Tank);
                            break;

                    }
                    // se non si è scelto Avanti, si ripete il ciclo
                    scelta = -1;

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta < 0 || scelta > numTipologie + 1) scelta = -1; }

            } while (scelta == -1);


        }

        private void PrintAbilitaTruppeEliteSupporto(List<Truppa_Elite> truppeElite)
        {
            int i = 0;
            foreach (var elite in truppeElite)
            {
                i++;
                CC.CyanFr($"{i}. {elite.Nome}");

                CC.WhiteFr(" | ");
                if (elite.Cooldown_Rimanente == 0)
                {
                    CC.WhiteFr($"{elite.Nome_Abilita} - Attivazione {elite.Costo_Abilita} Frammenti\n");
                }
                else
                {
                    CC.RedFr($"{elite.Nome_Abilita} - Attivazione {elite.Costo_Abilita} Frammenti\n");
                }
            }
        }

        public void PrintSceltaAbilitaEliteSupporto()
        {
            CC.DarkGreenFr("||Attiva abilità supporto Truppe Elite||\n");
            int scelta;
            int numTruppeElite = 0;
            // Associa all'index la truppa elite, così posso stampare 1., 2., 3., senza dover includere anche gli Elite non assoldati
            Dictionary<int, Truppa_Elite> indexTruppaElite = new Dictionary<int, Truppa_Elite>();
            foreach (var elite in giocatore.Truppe_Elite_Supporto)
            {
                if (elite.Assoldata)
                {
                    numTruppeElite++;
                    indexTruppaElite.Add(numTruppeElite, elite);
                }
            }

            do
            {
                scelta = -1;
                try
                {

                    // Stampa Truppe
                    CC.CyanFr("Quale Truppa Elite Supporto vuoi attivare?\n");
                    PrintAbilitaTruppeEliteSupporto(indexTruppaElite.Values.ToList());
                    CC.BlueFr($"{numTruppeElite + 1}. Avanti\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numTruppeElite + 1) throw new NumeroNonValidoException();

                    // Avanti
                    if (scelta == numTruppeElite + 1) break;

                    AttivazioneAbilitaSupportoElite(indexTruppaElite[scelta]);

                    // se non si è scelto Avanti, si ripete il ciclo
                    scelta = -1;

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta < 0 || scelta > numTruppeElite + 1) scelta = -1; }

            } while (scelta == -1);
        }

        private void AttivazioneAbilitaSupportoElite(Truppa_Elite elite)
        {
            int scelta;
            int numScelte = 2;
            do
            {
                scelta = -1;
                try
                {

                    // Stampa Scelte
                    CC.CyanFr("1. Dettagli\n");
                    CC.CyanFr("2. Attiva\n");
                    CC.BlueFr("0. Indietro\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numScelte) throw new NumeroNonValidoException();

                    switch (scelta)
                    {
                        case 1:
                            CC.GreenFr(elite.Descrizione_Abilita + "\n");
                            scelta = -1; break;
                        case 2:
                            // Controlli di fattibilità
                            if (elite.Cooldown_Rimanente > 0) throw new TruppaEliteCoolDownNonTerminatoException();
                            if (giocatore.Fornace.Frammenti < elite.Costo_Abilita) throw new FrammentiInsufficientiException();

                            // Salvo i frammenti precedenti
                            int frammenti = giocatore.Fornace.Frammenti;
                            // Attivo abilità
                            elite.Attiva();
                            break;
                    }

                    // se si è scelto Dettagli, si ripete il ciclo
                    if(scelta == 1) scelta = -1;

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (TruppaEliteCoolDownNonTerminatoException) { }
                catch (FrammentiInsufficientiException) { }
                finally { if (scelta < 0 || scelta > numScelte) scelta = -1; }

            } while (scelta == -1);
        }

    }


}
