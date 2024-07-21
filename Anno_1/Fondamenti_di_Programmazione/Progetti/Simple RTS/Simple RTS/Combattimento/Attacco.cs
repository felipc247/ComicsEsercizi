using Simple_RTS.Base;
using Simple_RTS.Eroi;
using Simple_RTS.Eroi.TipAttacco;
using Simple_RTS.Eroi.TipDifesa;
using Simple_RTS.Exceptions;
using Simple_RTS.Exceptions.Eroi;
using Simple_RTS.Exceptions.Frammenti;
using Simple_RTS.Exceptions.Input;
using Simple_RTS.Exceptions.Risorse;
using Simple_RTS.Exceptions.Truppe;
using Simple_RTS.Exceptions.Truppe_Elite;
using Simple_RTS.Truppe;
using Simple_RTS.Truppe_Elite.Guerra;
using Simple_RTS.Truppe_Elite.Guerra.Tank;
using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;

namespace Simple_RTS.Combattimento
{
    internal class Attacco
    {
        // Giocatori
        private Giocatore giocatore1, giocatore2;
        // Eroi
        private Bruciatore bruciatore;
        private Angelo angelo;
        // Truppe Elite
        private Colosso colosso1, colosso2;

        public Attacco() { }

        public void SetUp(Giocatore giocatore1, Giocatore giocatore2)
        {
            bruciatore = new Bruciatore(this);
            angelo = new Angelo(this);
            colosso1 = new Colosso(this, giocatore1);
            colosso2 = new Colosso(this, giocatore2);
            this.giocatore1 = giocatore1;
            this.giocatore2 = giocatore2;
        }

        // Reset Abilita per nuovo Turno di Attacco
        public void Reset()
        {
            // Abilità Eroi
            // Bruciatore
            movimenti_Rimanenti = 0;
            // Angelo
            protezione_Angelica = false;
            // Abilità Truppe Elite
            // Colosso
            vita_Scudo1 = 0;
            vita_Scudo2 = 0;
            // mov
            mov = 16;

        }

        // GET // SET

        public Giocatore Giocatore1 { get { return giocatore1; } }

        // TRUPPE

        private void PrintTipologieTruppa()
        {
            int i = 1;

            // TRUPPE MISCHIA
            CC.DarkYellowFr($"{i++}. {giocatore1.Truppe_Mischia[0].Tipologia}\n");

            // TRUPPE DISTANZA
            CC.DarkCyanFr($"{i++}. {giocatore1.Truppe_Distanza[0].Tipologia}\n");

            // TRUPPE TANK
            CC.DarkMagentaFr($"{i++}. {giocatore1.Truppe_Tank[0].Tipologia}\n");
        }

        private void PrintTruppeSchieraTruppe(List<Truppa> truppas)
        {
            int i = 1;
            switch (truppas[0].Tipologia)
            {
                case Truppa.TT.Mischia:
                    CC.DarkYellowFr("Truppe Mischia:\n");
                    foreach (var t in truppas)
                    {
                        CC.YellowFr($"{i++}. {t.Nome}");
                        CC.WhiteFr($" | Hai Qt. {t.Disponibili} disponibili ({t.Schierate} schierate, {t.Ferite} ferite)\n");
                    }
                    break;
                case Truppa.TT.Distanza:
                    CC.DarkCyanFr("Truppe Distanza:\n");
                    foreach (var t in truppas)
                    {
                        CC.CyanFr($"{i++}. {t.Nome}");
                        CC.WhiteFr($" | Hai Qt. {t.Disponibili} disponibili ({t.Schierate} schierate, {t.Ferite} ferite)\n");
                    }
                    break;
                case Truppa.TT.Tank:
                    CC.DarkMagentaFr("Truppe Tank:\n");
                    foreach (var t in truppas)
                    {
                        CC.MagentaFr($"{i++}. {t.Nome}");
                        CC.WhiteFr($" | Hai Qt. {t.Disponibili} disponibili ({t.Schierate} schierate, {t.Ferite} ferite)\n");
                    }
                    break;
            }
        }

        private void SchieraTruppe(List<Truppa> truppe)
        {
            int scelta;
            int truppeCount = truppe.Count;
            do
            {
                scelta = -1;
                try
                {
                    // Stampa Classi Truppe della Tipologia scelta
                    PrintTruppeSchieraTruppe(truppe);
                    CC.BlueFr("0. Indietro\n");

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
                        if (scelta < 0 || scelta > truppeCount) throw new NumeroNonValidoException();
                        if (scelta == 0) break;
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


                    // Stampa riepilogo schieramento
                    int totale = 0;

                    foreach (var item in qts)
                    {
                        CC.CyanFr($"{truppe[item.Key - 1].Nome}"); CC.WhiteFr($" => {item.Value}\n");
                        if (truppe[item.Key - 1].Disponibili < item.Value) throw new TruppeInsufficientiException();
                        totale += item.Value;
                    }

                    CC.BlueFr("Schierare queste Truppe?\n");

                    CC.DarkYellowFr($"Totale: {totale} Truppe\n");

                    CC.WhiteFr("1. Si\n" +
                        "2. No\n");

                    sceltaStr = Console.ReadLine();
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 1 || scelta > 2) throw new NumeroNonValidoException();

                    // Si
                    if (scelta == 1)
                    {
                        foreach (var item in qts)
                        {
                            truppe[item.Key - 1].Disponibili = -item.Value;
                            truppe[item.Key - 1].Schierate = item.Value;
                        }

                        CC.GreenFr("Truppe Schierate\n");

                    }
                }

                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FormattazioneNonValidaException) { scelta = -1; }
                catch (TruppeInsufficientiException) { }
                finally { if (scelta < 0 || scelta > 2) scelta = -1; }

            } while (scelta == -1);
        }

        private void PrintSchieraTruppe(Giocatore giocatore)
        {
            int scelta;
            int numTipologie = 3;
            do
            {
                scelta = -1;
                try
                {

                    // Stampa Truppe
                    CC.CyanFr("Cosa vuoi schierare?\n");
                    PrintTipologieTruppa();
                    CC.BlueFr($"0. Indietro\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numTipologie) throw new NumeroNonValidoException();

                    // Indietro
                    if (scelta == 0) break;

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            SchieraTruppe(giocatore.Truppe_Mischia);
                            break;
                        case 2:
                            SchieraTruppe(giocatore.Truppe_Distanza);
                            break;
                        case 3:
                            SchieraTruppe(giocatore.Truppe_Tank);
                            break;

                    }
                    // se non si è scelto Avanti, si ripete il ciclo
                    scelta = -1;

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta < 0 || scelta > numTipologie) scelta = -1; }

            } while (scelta == -1);

        }

        // TRUPPE ELITE

        // PER ABILITA

        // Colosso
        private int vita_Scudo1 = 0, vita_Scudo2 = 0;
        public int Vita_Scudo1 { get { return vita_Scudo1; } set { vita_Scudo1 = (vita_Scudo1 + value < 0) ? vita_Scudo1 = 0 : vita_Scudo1 + value; } }
        public int Vita_Scudo2 { get { return vita_Scudo2; } set { vita_Scudo2 = (vita_Scudo2 + value < 0) ? vita_Scudo2 = 0 : vita_Scudo2 + value; } }

        private void PrintAbilitaTruppeElite(List<Truppa_Elite_Guerra> elites)
        {
            int i = 0;
            foreach (var elite in elites)
            {
                i++;
                if (!elite.Assoldata) continue;

                CC.DarkCyanFr($"{i}. {elite.Nome}");



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

        private void PrintTipologieTruppeElite_Guerra(Giocatore giocatore)
        {
            int i = 1;

            // TRUPPE MISCHIA
            CC.DarkYellowFr($"{i++}. {giocatore.Truppe_Elite_Guerra_Mischia[0].Tipologia_Guerra}\n");

            // TRUPPE DISTANZA
            CC.DarkCyanFr($"{i++}. {giocatore.Truppe_Elite_Guerra_Distanza[0].Tipologia_Guerra}\n");

            // TRUPPE TANK
            CC.DarkMagentaFr($"{i++}. {giocatore.Truppe_Elite_Guerra_Tank[0].Tipologia_Guerra}\n");

        }

        // Mostra elenco Tipologie TruppeElite_Guerra e consente di visualizzare la lista scelta
        private void PrintAttivaAbilitaTruppeElite_Guerra(Giocatore giocatore)
        {
            int scelta;
            int numTipologieGuerra = 3;
            do
            {
                scelta = -1;
                try
                {
                    // Stampa tipologie Truppe Elite Guerra
                    CC.CyanFr("Scegli tipologia\n");
                    PrintTipologieTruppeElite_Guerra(giocatore);
                    CC.BlueFr("0. Indietro\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numTipologieGuerra) throw new NumeroNonValidoException();

                    // Indietro
                    if (scelta == 0) break;

                    switch (scelta)
                    {
                        case 1:
                            SceltaAbilitaTruppeElite(giocatore.Truppe_Elite_Guerra_Mischia, giocatore);
                            break;
                        case 2:
                            SceltaAbilitaTruppeElite(giocatore.Truppe_Elite_Guerra_Distanza, giocatore);
                            break;
                        case 3:
                            SceltaAbilitaTruppeElite(giocatore.Truppe_Elite_Guerra_Tank, giocatore);
                            break;
                        case 0:
                            break;
                    }

                    scelta = -1;

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FrammentiInsufficientiException) { }
                catch (TruppaEliteGiaAssoldataException) { }
                finally { if (scelta < 0 || scelta > numTipologieGuerra) scelta = -1; }

            } while (scelta == -1);
        }

        // Scelta abilita Truppe Elite
        private void SceltaAbilitaTruppeElite(List<Truppa_Elite_Guerra> elites, Giocatore giocatore)
        {
            int scelta;

            do
            {
                scelta = -1;
                try
                {
                    // Stampa Truppe Elite Guerra
                    PrintAbilitaTruppeElite(elites);
                    CC.BlueFr("0. Indietro\n");

                    CC.CyanFr("Inserisci numElite\n" +
                        "Invio e inseriscine un altro\n" +
                        "Invio senza nulla per terminare\n");

                    // per salvare Truppe Elite che l'utente vuole abilitare
                    List<int> elite_Scelti = new List<int>();

                    String sceltaStr;

                    bool first = true;

                    do
                    {
                        // lettura scelta
                        sceltaStr = Console.ReadLine();

                        if (first)
                        {
                            // se al primo input non si inserisce nulla
                            // si esce, nessun Eroe selezionato

                            if (sceltaStr.Equals(""))
                            {
                                CC.MagentaFr("Non hai scelto nessuna Truppa Elite\n");
                                scelta = 0;
                                break;
                            }
                            first = false;
                        }
                        else
                        {
                            // se non si inserisce nulla dopo, allora fine input
                            if (sceltaStr.Equals("")) break;
                        }


                        // gestione eccezioni input

                        if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                        if (scelta < 0 || !elites[scelta - 1].Assoldata) throw new NumeroNonValidoException();

                        // indietro, si esce
                        if (scelta == 0) break;

                        // se tutto va bene aggiungo alla List
                        if (!elite_Scelti.Contains(scelta))
                        {
                            elite_Scelti.Add(scelta);
                        }

                    } while (true);

                    // indietro scelta == 0 o Nessun Eroe selezionato
                    if (scelta == 0) break;

                    // Stampa riepilogo scelta Trippe Elite

                    int i = 1;

                    CC.BlueFr("Attivare le abilità di queste Truppe Elite?\n");
                    foreach (var item in elite_Scelti)
                    {
                        CC.CyanFr($"{i++}. {elites[item - 1].Nome}\n");
                    }

                    CC.WhiteFr("1. Si\n" +
                        "2. No\n");

                    sceltaStr = Console.ReadLine();
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 1 || scelta > 2) throw new NumeroNonValidoException();

                    // Si
                    if (scelta == 1)
                    {
                        int frammenti = giocatore.Fornace.Frammenti;
                        // Controllo che le abilità non siano in cooldown
                        // Controllo che il giocatore abbia Frammenti a sufficienza prima di attivare
                        foreach (var index_elite in elite_Scelti)
                        {
                            if (elites[index_elite - 1].Cooldown_Rimanente > 0) throw new CooldownNonTerminatoException();
                            if (giocatore.Fornace.Frammenti < elites[index_elite - 1].Costo_Abilita) throw new FrammentiInsufficientiException();
                            giocatore.Fornace.Frammenti = -elites[index_elite - 1].Costo_Abilita;
                        }

                        foreach (var index_elite in elite_Scelti)
                        {
                            elites[index_elite - 1].Attiva();
                        }

                        CC.GreenFr($"Abilità Attivate\n");

                        CC.WhiteFr($"Frammenti {frammenti} => {giocatore.Fornace.Frammenti}");

                        PS.kart();
                    }

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FrammentiInsufficientiException) { }
                catch (CooldownNonTerminatoException) { }
                catch (ArgumentOutOfRangeException) { }

                finally { if (scelta < 0 || scelta > 2) scelta = -1; }

            } while (scelta == -1);
        }

        // EROI

        private void PrintTipologieEroi_Atk()
        {
            // Attacco
            CC.DarkMagentaFr($"{giocatore1.Eroi_Attacco[0].Tipologia}\n");
        }

        private void PrintTipologieEroi_Dif()
        {
            // Difesa
            CC.DarkCyanFr($"{giocatore1.Eroi_Difesa[0].Tipologia}\n");
        }

        private void PrintEroi(List<Eroe> eroi)
        {
            int i = 1;
            foreach (var eroe in eroi)
            {
                // stampo Nome Eroe, il colore cambia in base alla Tipologia
                switch (eroe.Tipologia)
                {
                    case Eroe.TT.Attacco:
                        CC.MagentaFr($"{i++}. {eroe.Nome}");
                        break;
                    case Eroe.TT.Difesa:
                        CC.CyanFr($"{i++}. {eroe.Nome}");
                        break;
                }

                CC.WhiteFr(" | ");
                if (eroe.Evocato)
                {
                    if (eroe.Stato == 2)
                        CC.DarkYellowFr("Imprigionato\n");
                    else
                        CC.GreenFr("Con te\n");
                }
                else
                {
                    CC.RedFr("Da evocare\n");
                }
            }
        }

        private void PrintSceltaEroi(Giocatore giocatore)
        {
            int scelta;
            int tipologieEroe = 1;
            do
            {
                scelta = -1;
                try
                {
                    // Si stampano Eroi ATK se g1, Difesa se g2
                    CC.CyanFr("Scegli Eroe\n");
                    if (giocatore.Nome.Equals(giocatore1.Nome))
                    {
                        PrintTipologieEroi_Atk();

                        scelta = SceltaEroi(giocatore.Eroi_Attacco);
                    }
                    else
                    {
                        PrintTipologieEroi_Dif();

                        scelta = SceltaEroi(giocatore.Eroi_Difesa);
                    }

                    // Indietro in scelta Eroi
                    if (scelta == 0) break;


                    scelta = -1;
                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta != tipologieEroe + 1) scelta = -1; }

            } while (scelta == -1);

        }

        private int NumEroiScelti(List<Eroe> eroi)
        {
            int eroiScelti = 0;
            foreach (Eroe eroe in eroi)
            {
                if (eroe.Stato == 1)
                {
                    eroiScelti++;
                }
            }
            return eroiScelti;
        }

        private int SceltaEroi(List<Eroe> eroi)
        {
            int scelta;
            int eroiCount = eroi.Count;
            do
            {
                scelta = -1;
                try
                {
                    // Stampa Eroi della Tipologia scelta
                    PrintEroi(eroi);
                    CC.BlueFr("0. Indietro\n");

                    CC.CyanFr("Inserisci numEroe\n" +
                        "Invio e ripeti per altri Eroi (MAX 3)\n" +
                        "Invio senza nulla per terminare\n");

                    // per salvare gli Eroi scelti
                    List<int> eroi_Scelti = new List<int>();

                    int numEroiScelti = NumEroiScelti(eroi);

                    String sceltaStr;

                    bool first = true;

                    int i = 1;
                    do
                    {
                        // lettura scelta
                        sceltaStr = Console.ReadLine();

                        if (first)
                        {
                            // se al primo input non si inserisce nulla
                            // si esce, nessun Eroe selezionato

                            if (sceltaStr.Equals(""))
                            {
                                CC.MagentaFr("Non hai scelto nessun Eroe\n");
                                scelta = 0;
                                break;
                            }
                            first = false;
                        }
                        else
                        {
                            // se non si inserisce nulla dopo, allora fine input
                            if (sceltaStr.Equals("")) break;
                        }


                        // gestione eccezioni input

                        if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                        if (scelta < 0 || scelta > eroiCount) throw new NumeroNonValidoException();

                        // indietro, si esce
                        if (scelta == 0) break;

                        // non posso scegliere un eroe due volte
                        if (eroi[scelta - 1].Stato == 1) throw new EroeGiaAggiuntoException();

                        // se tutto va bene aggiungo alla lista
                        if (!eroi_Scelti.Contains(scelta))
                        {
                            eroi_Scelti.Add(scelta);
                            i++;
                        }

                    } while (numEroiScelti < 4);

                    // indietro scelta == 0 o Nessun Eroe selezionato
                    if (scelta == 0) break;

                    // Stampa riepilogo scelta Eroi

                    i = 1;

                    CC.BlueFr("Scegliere questi Eroi?\n");
                    foreach (var item in eroi_Scelti)
                    {
                        CC.CyanFr($"{i++}. {eroi[item - 1].Nome}\n");
                    }

                    CC.WhiteFr("1. Si\n" +
                        "2. No\n");

                    sceltaStr = Console.ReadLine();
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 1 || scelta > 2) throw new NumeroNonValidoException();

                    // Si
                    if (scelta == 1)
                    {
                        foreach (var item in eroi_Scelti)
                        {
                            if (!eroi[item - 1].Evocato) throw new EroeNonEvocatoException();
                            if (eroi[item - 1].Stato == 2) throw new EroeImprigionatoException();
                        }

                        foreach (var item in eroi_Scelti)
                        {
                            eroi[item - 1].Schierato();
                        }

                        CC.GreenFr($"Eroi Schierati\n");
                    }

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (EroeNonEvocatoException) { }
                catch (EroeImprigionatoException) { }
                catch (EroeGiaAggiuntoException) { }
                finally { if (scelta < 0 || scelta > 2) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }
        private void PrintAbilitaEroi(List<Eroe> eroi)
        {
            int i = 0;
            foreach (var eroe in eroi)
            {
                i++;
                // Se Eroe non schierato, passo al prossimo
                if (eroe.Stato != 1) continue;
                // stampo Nome Eroe, il colore cambia in base alla Tipologia
                switch (eroe.Tipologia)
                {
                    case Eroe.TT.Attacco:
                        CC.MagentaFr($"{i}. {eroe.Nome}");
                        break;
                    case Eroe.TT.Difesa:
                        CC.CyanFr($"{i}. {eroe.Nome}");
                        break;
                }

                CC.WhiteFr(" | ");
                if (eroe.Cooldown_Rimanente == 0)
                {
                    CC.WhiteFr($"{eroe.Nome_Abilita} - Attivazione {eroe.Costo_Abilita} Luce Blu\n");
                }
                else
                {
                    CC.RedFr($"{eroe.Nome_Abilita} - Attivazione {eroe.Costo_Abilita} Luce Blu\n");
                }
            }
        }

        // ABILITÀ EROI

        // Per abilità Bruciatore
        private int movimenti_Rimanenti = 0;

        public int Movimenti_Rimanenti { get { return movimenti_Rimanenti; } set { movimenti_Rimanenti = (movimenti_Rimanenti + value < 0) ? 0 : movimenti_Rimanenti + value; } }

        private void menoMovimento()
        {
            if (movimenti_Rimanenti == 1) CC.MagentaFr("Abilità Bruciatore terminata");
            movimenti_Rimanenti = (movimenti_Rimanenti - 1 < 0) ? 0 : movimenti_Rimanenti - 1;
        }

        // Per abilità Angelo

        private bool protezione_Angelica = false;
        public bool Protezione_Angelica { get { return protezione_Angelica; } set { protezione_Angelica = value; } }

        // Scelta abilita Eroi
        private void SceltaAbilitaEroi(List<Eroe> eroi, Giocatore giocatore)
        {
            int scelta;
            int eroiCount = 0;
            foreach (var eroe in eroi)
            {
                eroiCount = (eroe.Stato == 1) ? eroiCount + 1 : eroiCount;
            }

            do
            {
                scelta = -1;
                try
                {
                    // Stampa Eroi della Tipologia scelta
                    PrintAbilitaEroi(eroi);
                    CC.BlueFr("0. Indietro\n");

                    CC.CyanFr("Inserisci numEroe\n" +
                        "Invio e inseriscine un altro\n" +
                        "Invio senza nulla per terminare\n");

                    // per salvare gli Eroi che l'utente vuole abilitare
                    List<int> eroi_Scelti = new List<int>();

                    String sceltaStr;

                    bool first = true;

                    do
                    {
                        // lettura scelta
                        sceltaStr = Console.ReadLine();

                        if (first)
                        {
                            // se al primo input non si inserisce nulla
                            // si esce, nessun Eroe selezionato

                            if (sceltaStr.Equals(""))
                            {
                                CC.MagentaFr("Non hai scelto nessun Eroe\n");
                                scelta = 0;
                                break;
                            }
                            first = false;
                        }
                        else
                        {
                            // se non si inserisce nulla dopo, allora fine input
                            if (sceltaStr.Equals("")) break;
                        }


                        // gestione eccezioni input

                        if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                        if (scelta != 0 && eroi[scelta - 1].Stato != 1) throw new NumeroNonValidoException();

                        // indietro, si esce
                        if (scelta == 0) break;

                        // se tutto va bene aggiungo alla List
                        if (!eroi_Scelti.Contains(scelta))
                        {
                            eroi_Scelti.Add(scelta);
                        }

                    } while (true);

                    // indietro scelta == 0 o Nessun Eroe selezionato
                    if (scelta == 0) break;

                    // Stampa riepilogo scelta Eroi

                    int i = 1;

                    CC.BlueFr("Attivate le abilità di questi Eroi?\n");
                    foreach (int item in eroi_Scelti)
                    {
                        CC.CyanFr($"{i++}. {eroi[item - 1].Nome}\n");
                    }

                    CC.WhiteFr("1. Si\n" +
                        "2. No\n");

                    sceltaStr = Console.ReadLine();
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 1 || scelta > 2) throw new NumeroNonValidoException();

                    // Si
                    if (scelta == 1)
                    {
                        int luce_blu = giocatore.Altare.Luce_BLU;
                        // Controllo che le abilità non siano in cooldown
                        // Controllo che il giocatore abbia Luce Blu a sufficienza prima di attivare
                        foreach (var index_eroe in eroi_Scelti)
                        {
                            if (eroi[index_eroe - 1].Cooldown_Rimanente > 0) throw new CooldownNonTerminatoException();
                            if (giocatore.Altare.Luce_BLU < eroi[index_eroe - 1].Costo_Abilita) throw new LuceBluInsufficienteException();
                            giocatore.Altare.Luce_BLU = -eroi[index_eroe - 1].Costo_Abilita;
                        }

                        foreach (var index_eroe in eroi_Scelti)
                        {
                            eroi[index_eroe - 1].Attiva();
                        }

                        CC.GreenFr($"Abilità Attivate\n");

                        CC.WhiteFr($"Luce Blu {luce_blu} => {giocatore.Altare.Luce_BLU}");

                        PS.kart();
                    }

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (LuceBluInsufficienteException) { }
                catch (CooldownNonTerminatoException) { }
                catch (ArgumentOutOfRangeException) { }
                finally { if (scelta < 0 || scelta > 2) scelta = -1; }

            } while (scelta == -1);

        }

        // PREPARAZIONE

        // Stampa scelte disponibili
        private void PrintPreparazione()
        {
            int i = 1;
            CC.DarkCyanFr($"{i++}. Schiera Eroi\n");
            CC.YellowFr($"{i++}. Schiera Truppe\n");
        }

        // Permette di scegliere quali Truppe ed Eroi schierare
        public int Preparazione(Giocatore giocatore)
        {

            CC.DarkGreenFr("||PREPARAZIONE BATTAGLIA||\n");
            int scelta;
            int numScelte = 2;
            bool first = true;
            do
            {
                scelta = -1;
                try
                {
                    // Se g1 non ha Truppe non può attaccare
                    if (giocatore.Nome == giocatore1.Nome && first)
                    {
                        // Controllo che almeno una Truppa sia stata schierata prima di attaccare
                        int totale = 0;

                        foreach (var truppa in giocatore.Truppe_Mischia)
                        {
                            totale += truppa.Disponibili;
                        }
                        foreach (var truppa in giocatore.Truppe_Distanza)
                        {
                            totale += truppa.Disponibili;
                        }
                        foreach (var truppa in giocatore.Truppe_Tank)
                        {
                            totale += truppa.Disponibili;
                        }

                        if (totale < 1) throw new TruppeInsufficientiException();
                        first = false;
                    }
                    // Stampa Truppe
                    CC.CyanFr("Cosa vuoi schierare?\n");
                    PrintPreparazione();
                    CC.BlueFr($"{numScelte + 1}. Avanti\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 1 || scelta > numScelte + 1) throw new NumeroNonValidoException();

                    // Avanti
                    if (scelta == numScelte + 1)
                    {
                        // Controllo che almeno una Truppa sia stata schierata prima di attaccare
                        int totale = 0;

                        foreach (var item in giocatore.Truppe_Mischia)
                        {
                            totale += item.Schierate;
                        }
                        foreach (var item in giocatore.Truppe_Distanza)
                        {
                            totale += item.Schierate;
                        }
                        foreach (var item in giocatore.Truppe_Tank)
                        {
                            totale += item.Schierate;
                        }

                        if (totale > 0) { break; }
                        else { if (giocatore.Nome == giocatore1.Nome) { throw new TruppeNecessarieException(); } else { break; } }
                    }

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            PrintSceltaEroi(giocatore);
                            break;
                        case 2:
                            PrintSchieraTruppe(giocatore);
                            break;

                    }
                    // se non si è scelto Avanti, si ripete il ciclo
                    scelta = -1;

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (TruppeNecessarieException) { scelta = -1; }
                catch (TruppeInsufficientiException) { break; }
                finally { if (scelta != numScelte + 1) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        private int[] movimenti = new int[17];

        private void InizializzaMovimenti()
        {
            for (int i = 0; i < movimenti.Length; i++)
            {
                movimenti[i] = 0;
            }
        }

        public void StimaTruppeNemiche()
        {
            int totale = 0;

            Random ra = new Random();

            CC.DarkYellowFr("Truppe Mischia:\n");
            foreach (var truppa in giocatore1.Truppe_Mischia)
            {
                if (truppa.Schierate == 0) continue;
                int totaleTruppa = 0;
                double precisione = ra.Next(0, 31);
                precisione /= 100;
                int piu_meno = ra.Next(0, 2); // 0 = rimuovo, 1 = aggiungo Truppe
                totaleTruppa = (piu_meno == 0) ? (int)(truppa.Schierate + truppa.Schierate * precisione) : (int)(truppa.Schierate - truppa.Schierate * precisione);
                if (totaleTruppa < 1)
                    totaleTruppa = 1;

                totale += totaleTruppa;

                CC.YellowFr($"{truppa.Nome} Qt. {totaleTruppa}\n");
            }
            PS.kart();
            CC.DarkCyanFr("Truppe Distanza:\n");
            foreach (var truppa in giocatore1.Truppe_Distanza)
            {
                if (truppa.Schierate == 0) continue;
                int totaleTruppa = 0;
                double precisione = ra.Next(1, 31);
                precisione /= 100;
                int piu_meno = ra.Next(0, 2); // 0 = rimuovo, 1 = aggiungo Truppe
                totaleTruppa = (piu_meno == 0) ? (int)(truppa.Schierate + truppa.Schierate * precisione) : (int)(truppa.Schierate - truppa.Schierate * precisione);
                if (totaleTruppa < 1)
                    totaleTruppa = 1;

                totale += totaleTruppa;
                CC.CyanFr($"{truppa.Nome} Qt. {totaleTruppa}\n");
            }
            PS.kart();
            CC.DarkMagentaFr("Truppe Tank:\n");
            foreach (var truppa in giocatore1.Truppe_Tank)
            {
                if (truppa.Schierate == 0) continue;
                int totaleTruppa = 0;
                double precisione = ra.Next(1, 31);
                precisione /= 100;
                int piu_meno = ra.Next(0, 2); // 0 = rimuovo, 1 = aggiungo Truppe
                totaleTruppa = (piu_meno == 0) ? (int)(truppa.Schierate + truppa.Schierate * precisione) : (int)(truppa.Schierate - truppa.Schierate * precisione);
                if (totaleTruppa < 1)
                    totaleTruppa = 1;

                totale += totaleTruppa;
                CC.MagentaFr($"{truppa.Nome} Qt. {totaleTruppa}\n");
            }
            PS.kart();
            CC.RedFr($"Totale Truppe {totale}\n");

        }

        // BATTAGLIA

        private void SetUpVitaTruppe(List<Truppa> truppe) {
            int j = 0;
            foreach (var item in truppe)
            {
                truppe[j].Vite.Clear();
                for (int i = 0; i < truppe[j].Schierate; i++)
                {
                    truppe[j].Vite.Add(truppe[j].Vita);
                }
                j++;
            }
        }

        public void SetupViteTruppe()
        {
            // G1
            SetUpVitaTruppe(giocatore1.Truppe_Mischia);
            SetUpVitaTruppe(giocatore1.Truppe_Distanza);
            SetUpVitaTruppe(giocatore1.Truppe_Tank);

            // G2
            SetUpVitaTruppe(giocatore2.Truppe_Mischia);
            SetUpVitaTruppe(giocatore2.Truppe_Distanza);
            SetUpVitaTruppe(giocatore2.Truppe_Tank);

        }

        private int mov = 16;

        public void Avanza()
        {
            movimenti[mov] = 0;
            mov--;
            movimenti[mov] = 1;
            spezza();
            CC.BlueFr($"{mov}\n");
            spezza();

        }

        public int CalcoloDanni()
        {
            // Il calcolo dei danni avviene simultaneamente

            // Calcolo totale dei danni, e poi distribuzione equa in base al range a botte di 5 ad ogni Truppa
            // Se i danni sono sufficienti e più ad uccidere le Truppe di una Classe si passa alla successiva

            // del tipo 5000 danni, su 5 mammut, ognuno riceve 1000 danni
            // o 2 Mammut, 3500 danni, 3000 li prendono i mammut, e gli altri 500 i Soldati_semplici
            // In case di range uguale, le Truppe con costo minore vengono colpite prime
            // Se hanno anche stesso costo si estre random

            int danni_g1 = 0;
            int danni_g2 = 0;

            // CALCOLO DANNI

            // Giocatore 1

            // Controllo abilità Eroi

            // Bruciatore

            CC.DarkCyanFr($"{giocatore1.Nome}:\n");
            foreach (var truppa in giocatore1.Truppe_Tank)
            {
                if (truppa.Schierate > 0)
                {
                    if (truppa.Range >= GetDistance())
                    {
                        CC.GreenFr($"{truppa.Nome} In range\n");
                        danni_g1 += (truppa.Atk * truppa.Schierate);
                    }
                    else
                    {
                        CC.RedFr($"{truppa.Nome} Non range\n");
                    }
                }
            }

            foreach (var truppa in giocatore1.Truppe_Mischia)
            {
                if (truppa.Schierate > 0)
                {
                    if (truppa.Range >= GetDistance())
                    {
                        CC.GreenFr($"{truppa.Nome} In range\n");
                        danni_g1 += (truppa.Atk * truppa.Schierate);
                    }
                    else
                    {
                        CC.RedFr($"{truppa.Nome} Non range\n");
                    }
                }
            }

            foreach (var truppa in giocatore1.Truppe_Distanza)
            {
                if (truppa.Schierate > 0)
                {
                    if (truppa.Range >= GetDistance())
                    {
                        CC.GreenFr($"{truppa.Nome} In range\n");
                        danni_g1 += (truppa.Atk * truppa.Schierate);
                    }
                    else
                    {
                        CC.RedFr($"{truppa.Nome} Non range\n");
                    }
                }
            }

            // Giocatore 2

            // Calcolo

            CC.BlueFr($"{giocatore2.Nome}:\n");

            foreach (var truppa in giocatore2.Truppe_Tank)
            {
                if (truppa.Schierate > 0)
                {
                    if (truppa.Range >= GetDistance())
                    {
                        CC.GreenFr($"{truppa.Nome} In range\n");
                        danni_g2 += (truppa.Atk * truppa.Schierate);
                    }
                    else
                    {
                        CC.RedFr($"{truppa.Nome} Non range\n");
                    }
                }
            }

            foreach (var truppa in giocatore2.Truppe_Mischia)
            {
                if (truppa.Schierate > 0)
                {
                    if (truppa.Range >= GetDistance())
                    {
                        CC.GreenFr($"{truppa.Nome} In range\n");
                        danni_g2 += (truppa.Atk * truppa.Schierate);
                    }
                    else
                    {
                        CC.RedFr($"{truppa.Nome} Non range\n");
                    }
                }
            }

            foreach (var truppa in giocatore2.Truppe_Distanza)
            {
                if (truppa.Schierate > 0)
                {
                    if (truppa.Range >= GetDistance())
                    {
                        CC.GreenFr($"{truppa.Nome} In range\n");
                        danni_g2 += (truppa.Atk * truppa.Schierate);
                    }
                    else
                    {
                        CC.RedFr($"{truppa.Nome} Non range\n");
                    }
                }
            }

            // Bonus ATK Torre G2
            double temp_danni_g2 = danni_g2 * 1.2;
            if (temp_danni_g2 % 5 != 0)
            {
                temp_danni_g2 -= temp_danni_g2 % 5;
            }
            danni_g2 = (int)temp_danni_g2;

            // Truppe Elite abilita

            // G1

            // Colosso
            if (vita_Scudo1 > 0)
            {
                int danni_g1_temp = danni_g1;
                danni_g1 = (danni_g1 - vita_Scudo1 < 0) ? 0 : danni_g1 - vita_Scudo1;
                vita_Scudo1 -= danni_g1_temp - danni_g1;
            }

            // G2

            // Colosso
            if (vita_Scudo2 > 0)
            {
                int danni_g2_temp = danni_g2;
                danni_g2 = (danni_g2 - vita_Scudo2 < 0) ? 0 : danni_g2 - vita_Scudo2;
                vita_Scudo2 -= danni_g2_temp - danni_g2;
            }

            // Salvo per dati
            int danni_g1_save = danni_g1;
            int danni_g2_save = danni_g2;

            CC.MagentaFr("DANNI:\n");
            CC.RedFr($"danni {giocatore1.Nome}:{danni_g1}\n" +
                $"danni {giocatore2.Nome}:{danni_g2}\n");

            // DISTRIBUZIONE DANNI

            // G2

            CC.BlueFr($"Danni alle truppe di {giocatore2.Nome}:\n");

            RangeSort(giocatore2.Truppe_Tank);

            // salvo danni inflitti da bruciatore
            int danni_bruciatore = 0;

            foreach (var truppa in giocatore2.Truppe_Tank)
            {
                // Controllo abilità Bruciatore
                if (danni_g1 == 0 && movimenti_Rimanenti == 0) break;
                if (truppa.Schierate == 0) continue;
                int j = 0;
                int truppe_uccise = 0;
                int danni_subiti = 0;
                bool bruciatoreDeveDanneggiare = true;
                do
                {
                    for (int i = truppa.Vite.Count - 1; i > -1; i--)
                    {
                        // Controllo abilità Bruciatore
                        if (danni_g1 == 0 && movimenti_Rimanenti == 0) break;
                        int danni = 0;
                        // Infliggo prima i danni di Bruciatore
                        if (movimenti_Rimanenti > 0 && bruciatoreDeveDanneggiare)
                        {
                            danni = bruciatore.Danni_Per_Movimento; danni_subiti += danni; danni_bruciatore += danni;
                            if (truppa.Vite[i] - danni <= 0)
                            {
                                danni = bruciatore.Danni_Per_Movimento + (truppa.Vite[i] - danni);
                                danni_subiti += (truppa.Vite[i] - danni == 0) ? 0 : danni;
                                danni_bruciatore += (truppa.Vite[i] - danni == 0) ? 0 : danni;
                                truppa.Schierate = -1;
                                giocatore2.Altare.Anime_DIF = +1;
                                truppa.Vite.RemoveAt(i);
                                truppe_uccise += 1;
                                continue;
                                //CC.RedFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} uccisa\n");
                            }
                            else
                            {
                                truppa.Vite[i] -= danni;
                                //CC.DarkCyanFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} danneggiata\n");
                            }
                        }

                        if (danni_g1 > 0)
                        {
                            danni = 5;
                            if (truppa.Vite[i] - danni <= 0)
                            {
                                truppa.Schierate = -1;
                                giocatore2.Altare.Anime_DIF = +1;
                                truppa.Vite.RemoveAt(i);
                                truppe_uccise += 1;
                                //CC.RedFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} uccisa\n");
                            }
                            else
                            {
                                truppa.Vite[i] -= danni;
                                //CC.DarkCyanFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} danneggiata\n");
                            }
                            j++;
                            danni_g1 -= 5;
                            danni_subiti += danni;
                        }

                        if (truppa.Vite.Count == 0) { break; }
                    }
                    if (bruciatoreDeveDanneggiare) bruciatoreDeveDanneggiare = false;
                } while (danni_g1 > 0 && truppa.Vite.Count > 0);

                if (truppe_uccise > 0)
                {
                    CC.RedFr($"{truppa.Nome} - {truppe_uccise} Mort* | ");
                    CC.YellowFr($"Danni subiti: {danni_subiti}\n");
                }
                else
                {
                    CC.YellowFr($"{truppa.Nome} | Danni subiti: {danni_subiti}\n");
                }
            }

            RangeSort(giocatore2.Truppe_Mischia);

            foreach (var truppa in giocatore2.Truppe_Mischia)
            {
                // Controllo abilità Bruciatore
                if (danni_g1 == 0 && movimenti_Rimanenti == 0) break;
                if (truppa.Schierate == 0) continue;
                int j = 0;
                int truppe_uccise = 0;
                int danni_subiti = 0;
                bool bruciatoreDeveDanneggiare = true;
                do
                {
                    for (int i = truppa.Vite.Count - 1; i > -1; i--)
                    {
                        // Controllo abilità Bruciatore
                        if (danni_g1 == 0 && movimenti_Rimanenti == 0) break;
                        int danni = 0;
                        // Infliggo prima i danni di Bruciatore
                        if (movimenti_Rimanenti > 0 && bruciatoreDeveDanneggiare)
                        {
                            danni = bruciatore.Danni_Per_Movimento; danni_subiti += danni; danni_bruciatore += danni;
                            if (truppa.Vite[i] - danni <= 0)
                            {
                                danni = bruciatore.Danni_Per_Movimento + (truppa.Vite[i] - danni);
                                danni_subiti += (truppa.Vite[i] - danni == 0) ? 0 : danni;
                                danni_bruciatore += (truppa.Vite[i] - danni == 0) ? 0 : danni;
                                truppa.Schierate = -1;
                                giocatore2.Altare.Anime_DIF = +1;
                                truppa.Vite.RemoveAt(i);
                                truppe_uccise += 1;
                                continue;
                                //CC.RedFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} uccisa\n");
                            }
                            else
                            {
                                truppa.Vite[i] -= danni;
                                //CC.DarkCyanFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} danneggiata\n");
                            }
                        }

                        if (danni_g1 > 0)
                        {
                            danni = 5;
                            if (truppa.Vite[i] - danni <= 0)
                            {
                                truppa.Schierate = -1;
                                truppa.Vite.RemoveAt(i);
                                giocatore2.Altare.Anime_DIF = +1;
                                truppe_uccise += 1;
                                //CC.RedFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} uccisa\n");
                            }
                            else
                            {
                                truppa.Vite[i] -= danni;
                                //CC.DarkCyanFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} danneggiata\n");
                            }
                            j++;
                            danni_g1 -= 5;
                            danni_subiti += danni;
                        }

                        if (truppa.Vite.Count == 0) { break; }
                    }
                    if (bruciatoreDeveDanneggiare) bruciatoreDeveDanneggiare = false;

                } while (danni_g1 > 0 && truppa.Vite.Count > 0);

                if (truppe_uccise > 0)
                {
                    CC.RedFr($"{truppa.Nome} - {truppe_uccise} Mort* | ");
                    CC.YellowFr($"Danni subiti: {danni_subiti}\n");
                }
                else
                {
                    CC.YellowFr($"{truppa.Nome} | Danni subiti: {danni_subiti}\n");
                }

            }


            RangeSort(giocatore2.Truppe_Distanza);

            foreach (var truppa in giocatore2.Truppe_Distanza)
            {
                if (protezione_Angelica)
                {
                    CC.GreenFr($"{angelo.Descrizione_Uso_Abilita}\n");
                    protezione_Angelica = false;
                    break;
                }
                // Controllo abilità Bruciatore
                if (danni_g1 == 0 && movimenti_Rimanenti == 0) break;
                if (truppa.Schierate == 0) continue;
                int j = 0;
                int truppe_uccise = 0;
                int danni_subiti = 0;
                bool bruciatoreDeveDanneggiare = true;
                do
                {
                    for (int i = truppa.Vite.Count - 1; i > -1; i--)
                    {
                        // Controllo abilità Bruciatore
                        if (danni_g1 == 0 && movimenti_Rimanenti == 0) break;
                        int danni = 0;
                        // Infliggo prima i danni di Bruciatore
                        if (movimenti_Rimanenti > 0 && bruciatoreDeveDanneggiare)
                        {
                            danni = bruciatore.Danni_Per_Movimento; danni_subiti += danni; danni_bruciatore += danni;
                            if (truppa.Vite[i] - danni <= 0)
                            {
                                danni -= bruciatore.Danni_Per_Movimento + (truppa.Vite[i] - danni);
                                danni_subiti += (truppa.Vite[i] - danni == 0) ? 0 : danni;
                                danni_bruciatore += (truppa.Vite[i] - danni == 0) ? 0 : danni;
                                truppa.Schierate = -1;
                                giocatore2.Altare.Anime_DIF = +1;
                                truppa.Vite.RemoveAt(i);
                                truppe_uccise += 1;
                                continue;
                                //CC.RedFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} uccisa\n");
                            }
                            else
                            {
                                truppa.Vite[i] -= danni;
                                //CC.DarkCyanFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} danneggiata\n");
                            }
                        }

                        if (danni_g1 > 0)
                        {
                            danni = 5;
                            if (truppa.Vite[i] - danni <= 0)
                            {
                                truppa.Schierate = -1;
                                giocatore2.Altare.Anime_DIF = +1;
                                truppa.Vite.RemoveAt(i);
                                truppe_uccise += 1;
                                //CC.RedFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} uccisa\n");
                            }
                            else
                            {
                                truppa.Vite[i] -= danni;
                                //CC.DarkCyanFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} danneggiata\n");
                            }
                            j++;
                            danni_g1 -= 5;
                            danni_subiti += danni;
                        }

                        if (truppa.Vite.Count == 0) { break; }
                    }
                    if (bruciatoreDeveDanneggiare) bruciatoreDeveDanneggiare = false;
                } while (danni_g1 > 0 && truppa.Vite.Count > 0);

                if (truppe_uccise > 0)
                {
                    CC.RedFr($"{truppa.Nome} - {truppe_uccise} Mort* | ");
                    CC.YellowFr($"Danni subiti: {danni_subiti}\n");
                }
                else
                {
                    CC.YellowFr($"{truppa.Nome} | Danni subiti: {danni_subiti}\n");
                }
            }

            // Abilità bruciatore
            menoMovimento();

            // G1 

            CC.CyanFr($"Danni alle truppe di {giocatore1.Nome}:\n");

            RangeSort(giocatore1.Truppe_Tank);

            foreach (var truppa in giocatore1.Truppe_Tank)
            {
                if (danni_g2 == 0) break;
                if (truppa.Schierate == 0) continue;
                int j = 0;
                int truppe_uccise = 0;
                int danni_subiti = 0;
                do
                {
                    for (int i = truppa.Vite.Count - 1; i > -1; i--)
                    {
                        if (danni_g2 == 0) break;
                        if (truppa.Vite[i] - 5 == 0)
                        {
                            truppa.Schierate = -1;
                            giocatore1.Altare.Anime_ATK = +1;
                            truppa.Vite.RemoveAt(i);
                            truppe_uccise += 1;
                            //CC.RedFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} uccisa\n");
                        }
                        else
                        {
                            truppa.Vite[i] -= 5;
                            //CC.DarkCyanFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} danneggiata\n");
                        }
                        j++;
                        danni_g2 -= 5;
                        danni_subiti += 5;
                        if (truppa.Vite.Count == 0) { break; }
                    }
                } while (danni_g2 > 0 && truppa.Vite.Count > 0);

                if (truppe_uccise > 0)
                {
                    CC.RedFr($"{truppa.Nome} - {truppe_uccise} Mort* | ");
                    CC.YellowFr($"Danni subiti: {danni_subiti}\n");
                }
                else
                {
                    CC.YellowFr($"{truppa.Nome} | Danni subiti: {danni_subiti}\n");
                }
            }
            RangeSort(giocatore1.Truppe_Mischia);

            foreach (var truppa in giocatore1.Truppe_Mischia)
            {
                if (danni_g2 == 0) break;
                if (truppa.Schierate == 0) continue;
                int j = 0;
                int truppe_uccise = 0;
                int danni_subiti = 0;
                do
                {
                    for (int i = truppa.Vite.Count - 1; i > -1; i--)
                    {
                        if (danni_g2 == 0) break;
                        if (truppa.Vite[i] - 5 == 0)
                        {
                            truppa.Schierate = -1;
                            giocatore1.Altare.Anime_ATK = +1;
                            truppa.Vite.RemoveAt(i);
                            truppe_uccise += 1;
                            //CC.RedFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} uccisa\n");
                        }
                        else
                        {
                            truppa.Vite[i] -= 5;
                            //CC.DarkCyanFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} danneggiata\n");
                        }
                        j++;
                        danni_g2 -= 5;
                        danni_subiti += 5;
                        if (truppa.Vite.Count == 0) { break; }
                    }
                } while (danni_g2 > 0 && truppa.Vite.Count > 0);

                if (truppe_uccise > 0)
                {
                    CC.RedFr($"{truppa.Nome} - {truppe_uccise} Mort* | ");
                    CC.YellowFr($"Danni subiti: {danni_subiti}\n");
                }
                else
                {
                    CC.YellowFr($"{truppa.Nome} | Danni subiti: {danni_subiti}\n");
                }
            }
            RangeSort(giocatore1.Truppe_Distanza);

            foreach (var truppa in giocatore1.Truppe_Distanza)
            {
                if (danni_g2 == 0) break;
                if (truppa.Schierate == 0) continue;
                int j = 0;
                int truppe_uccise = 0;
                int danni_subiti = 0;
                do
                {
                    for (int i = truppa.Vite.Count - 1; i > -1; i--)
                    {
                        if (danni_g2 == 0) break;
                        if (truppa.Vite[i] - 5 == 0)
                        {
                            truppa.Schierate = -1;
                            giocatore1.Altare.Anime_ATK = +1;
                            truppa.Vite.RemoveAt(i);
                            truppe_uccise += 1;
                            //CC.RedFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} uccisa\n");
                        }
                        else
                        {
                            truppa.Vite[i] -= 5;
                            //CC.DarkCyanFr($"{j} {giocatore2.Nome} Truppa {i},{truppa.Nome} danneggiata\n");
                        }
                        j++;
                        danni_g2 -= 5;
                        danni_subiti += 5;
                        if (truppa.Vite.Count == 0) { break; }
                    }
                } while (danni_g2 > 0 && truppa.Vite.Count > 0);

                if (truppe_uccise > 0)
                {
                    CC.RedFr($"{truppa.Nome} - {truppe_uccise} Mort* | ");
                    CC.YellowFr($"Danni subiti: {danni_subiti}\n");
                }
                else
                {
                    CC.YellowFr($"{truppa.Nome} | Danni subiti: {danni_subiti}\n");
                }
            }

            int vita_Torre_G2 = giocatore2.Torre.Vita;
            if (danni_g1 > 0 && mov == 1)
            {
                CC.RedFr($"{giocatore2.Nome}, Torre sotto attacco diretto:\n");
                giocatore2.Torre.Danneggia(danni_g1);
            }

            // return 0, entrambi i giocatori hanno ancora Truppe,
            // return 1, g1 non ha più Truppe, gli Eroi vengono Imprigionati,
            // return 2, entrambi i giocatori non hanno Truppe
            // return 3, siamo a mov = 1, e g2 non ha più Truppe, si controlla la vita della Torre

            int result = -1;

            if (CheckTruppeG1() && CheckTruppeG2())
            {
                result = 0;
            }

            if (!CheckTruppeG2() && !CheckTruppeG1())
            {
                result = 2;
            }

            if (!CheckTruppeG1())
            {
                result = 1;
            }

            if (vita_Torre_G2 != giocatore2.Torre.Vita)
            {
                result = 3;
            }

            CC.CyanFr($"{giocatore1.Nome}:\n");
            // se g1 non ha subito danni
            if (danni_g2 == danni_g2_save)
            {
                CC.GreenFr("Nessun danno subito\n");
            }
            else
            {
                CC.RedFr($"Totale danni subiti: {danni_g2_save - danni_g2}\n");
            }

            CC.BlueFr($"{giocatore2.Nome}:\n");
            // se g2 non ha subito danni
            if (danni_g1 == danni_g1_save && danni_bruciatore == 0)
            {
                CC.GreenFr("Nessun danno subito\n");
            }
            else
            {
                CC.RedFr($"Totale danni subiti: {danni_g1_save - danni_g1 + danni_bruciatore}\n");
            }


            return result;
        }

        // Controlla se g1 ha ancora Truppe a disposizione
        private bool CheckTruppeG1()
        {
            int tot = 0;
            foreach (var truppa in giocatore1.Truppe_Mischia)
            {
                if (truppa.Vite.Count > 0) tot += truppa.Vite.Count;
            }
            foreach (var truppa in giocatore1.Truppe_Distanza)
            {
                if (truppa.Vite.Count > 0) tot += truppa.Vite.Count;
            }
            foreach (var truppa in giocatore1.Truppe_Tank)
            {
                if (truppa.Vite.Count > 0) tot += truppa.Vite.Count;
            }
            return (tot != 0);
        }

        // Controlla se g2 ha ancora Truppe a disposizione
        private bool CheckTruppeG2()
        {
            int tot = 0;
            foreach (var truppa in giocatore2.Truppe_Mischia)
            {
                if (truppa.Vite.Count > 0) tot += truppa.Vite.Count;
            }
            foreach (var truppa in giocatore2.Truppe_Distanza)
            {
                if (truppa.Vite.Count > 0) tot += truppa.Vite.Count;
            }
            foreach (var truppa in giocatore2.Truppe_Tank)
            {
                if (truppa.Vite.Count > 0) tot += truppa.Vite.Count;
            }
            return (tot != 0);
        }

        // Ordina una Tipologia di truppe, in base al Range
        // mi serve per dare la precedenza ai danni
        // e.g. I tank subiranno i danni per primi
        public void RangeSort(List<Truppa> truppe)
        {
            Random ra = new Random();
            for (int i = 0; i < truppe.Count - 1; i++)
            {
                // Trova l'indice del minimo elemento nella parte non ordinata
                int minIndex = i;
                for (int j = i + 1; j < truppe.Count; j++)
                {
                    if (truppe[j].Range < truppe[minIndex].Range)
                    {
                        minIndex = j;
                    }
                    // Se il range è uguale si controlla il costo
                    else if (truppe[j].Range == truppe[minIndex].Range)
                    {
                        if (truppe[j].Costo < truppe[minIndex].Range)
                        {
                            minIndex = j;
                        }
                        // Se anche il costo è uguale si fa Random
                        else if (truppe[j].Costo == truppe[minIndex].Range)
                        {
                            int caso = ra.Next(0, 2);
                            if (caso == 0)
                            {
                                minIndex = j;
                            }
                        }
                    }
                }

                // Scambia l'elemento minimo con l'elemento all'inizio della parte non ordinata
                Truppa temp = truppe[minIndex];
                truppe[minIndex] = truppe[i];
                truppe[i] = temp;
            }
        }

        public void spezza()
        {
            CC.WhiteFr("------------------------------------------\n");
        }

        public int GetDistance()
        {
            return mov - 1;
        }



        // Dopo il calcolo dei danni, i giocatori possono scegliere cosa fare
        // per G1, può decidere se continuare o ritirarsi, nel caso attivare le abilità 
        // Elite e degli Eroi

        public int Decisione_g1()
        {
            int scelta;
            int numScelte = 4;
            do
            {
                scelta = -1;
                try
                {

                    // Stampa decisioni
                    CC.CyanFr($"{giocatore1.Nome} | Cosa vuoi fare?\n");
                    Print_Scelte_g1();

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 1 || scelta > numScelte) throw new NumeroNonValidoException();

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            // Avanza
                            break;
                        case 2:
                            // Scelta abilità Eroe
                            SceltaAbilitaEroi(giocatore1.Eroi_Attacco, giocatore1);
                            break;
                        case 3:
                            // Scelta abilità Truppe Elite
                            PrintAttivaAbilitaTruppeElite_Guerra(giocatore1);
                            break;
                        case 4:
                            break;

                    }

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta != 1 && scelta != 4) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        public void Print_Scelte_g1()
        {
            int i = 1;
            CC.GreenFr($"{i++}. Avanza\n");
            CC.BlueFr($"{i++}. Attiva abilità Eroi\n");
            CC.DarkCyanFr($"{i++}. Attiva abilità Truppe Elite\n");
            CC.RedFr($"{i++}. Ritirati\n");
        }

        public void Print_Scelte_g2()
        {
            int i = 1;
            CC.BlueFr($"{i++}. Attiva abilità Eroi\n");
            CC.DarkCyanFr($"{i++}. Attiva abilità Truppe Elite\n");
        }

        public void Decisione_g2()
        {
            int scelta;
            int numScelte = 2;
            do
            {
                scelta = -1;
                try
                {
                    // Stampa decisioni
                    CC.CyanFr($"{giocatore2.Nome} | Cosa vuoi fare?\n");
                    Print_Scelte_g2();
                    CC.BlueFr($"{numScelte + 1}. Avanti\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 1 || scelta > numScelte + 1) throw new NumeroNonValidoException();

                    // Avanti
                    if (scelta == numScelte + 1) break;

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            // Scelta abilità Eroe
                            SceltaAbilitaEroi(giocatore2.Eroi_Difesa, giocatore2);
                            break;
                        case 2:
                            // Scelta abilità Truppe Elite
                            PrintAttivaAbilitaTruppeElite_Guerra(giocatore2);
                            break;
                    }

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta != numScelte + 1) scelta = -1; }

            } while (scelta == -1);

        }

        public int AAAttacco()
        {
            InizializzaMovimenti();
            movimenti[1] = 2; // posizione Truppe nemiche, 1

            int decisione_g1 = -1;
            int result = -1;
            for (int i = movimenti.Length - 1; i > 0; i--)
            {
                // Avvistamento
                if (i == 16)
                {
                    CC.RedFr($"{giocatore1.Nome} Ti sta attaccando!\n");
                    StimaTruppeNemiche();
                    Preparazione(giocatore2);
                    SetupViteTruppe();
                    Avanza();
                    continue;
                }

                decisione_g1 = this.Decisione_g1();
                if (decisione_g1 == 4)
                {
                    CC.GreenFr($"{giocatore1.Nome} Si ritira\n");
                    break;
                }
                Decisione_g2();
                result = CalcoloDanni();

                if (result == 1)
                {
                    // g1 ha perso tutte le Truppe
                    int count = 0;
                    foreach (var eroe in giocatore1.Eroi_Attacco)
                    {
                        if (eroe.Stato == 1)
                        {
                            eroe.Imprigionato();
                            count++;
                        }
                    }
                    CC.RedFr($"{giocatore1.Nome} Ha perso tutte le Truppe");
                    if (count > 0) CC.RedFr($", i suoi Eroi sono stati imprigionati");
                    PS.kart();
                    break;
                }
                if (result == 2)
                {
                    // g1 e g2 non hanno più Truppe, pareggio
                    CC.DarkMagentaFr("Nessun Giocatore ha più Truppe\n");
                    break;
                }
                if (result == 3)
                {
                    if (giocatore2.Torre.Vita == 0)
                    {
                        CC.DarkYellowFr($"{giocatore1.Nome} VICTORY!\n");
                    }
                }

                Avanza();
            }

            RitornaTruppeEroi();
            Reset();
            return result;
        }

        // ritorna le Truppe ai rispettivi giocatori se ce ne sono
        // ritorna Eroi ai rispettivi giocatori se ce ne sono
        private void RitornaTruppeEroi()
        {
            RitornaTruppe_g1();
            RitornaTruppe_g2();
            RitornaEroi_g1();
            RitornaEroi_g2();
        }


        private void RitornaEroi_g1()
        {
            foreach (var eroe in giocatore1.Eroi_Attacco)
            {
                if (eroe.Stato == 1)
                {
                    eroe.Disponibile();
                }
            }
        }

        private void RitornaEroi_g2()
        {
            foreach (var eroe in giocatore2.Eroi_Difesa)
            {
                if (eroe.Stato == 1)
                {
                    eroe.Disponibile();
                }
            }
        }

        private void RitornaTruppe(List<Truppa> truppe) {
            foreach (var truppa in truppe)
            {
                for (int i = 0; i < truppa.Vite.Count; i++)
                {
                    if (truppa.Vite[i] != truppa.Vita)
                    {
                        truppa.Ferite = +1;
                    }
                    else
                    {
                        truppa.Disponibili = +1;
                    }
                    truppa.Schierate = -1;
                }
            }
        }

        private void RitornaTruppe_g1()
        {
            RitornaTruppe(giocatore1.Truppe_Mischia);
            RitornaTruppe(giocatore1.Truppe_Distanza);
            RitornaTruppe(giocatore1.Truppe_Tank);            
        }

        private void RitornaTruppe_g2()
        {
            RitornaTruppe(giocatore2.Truppe_Mischia);
            RitornaTruppe(giocatore2.Truppe_Distanza);
            RitornaTruppe(giocatore2.Truppe_Tank);
        }
    }


}
