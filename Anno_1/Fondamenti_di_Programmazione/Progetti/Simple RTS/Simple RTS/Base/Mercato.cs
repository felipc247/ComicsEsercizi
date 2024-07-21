using Simple_RTS.Exceptions;
using Simple_RTS.Exceptions.Frammenti;
using Simple_RTS.Exceptions.Input;
using Simple_RTS.Exceptions.Risorse;
using Simple_RTS.Exceptions.Strutture;
using Simple_RTS.Exceptions.Truppe;
using Simple_RTS.Exceptions.Truppe_Elite;
using Simple_RTS.Truppe;
using Simple_RTS.Truppe_Elite.Guerra;
using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Base
{
    // Luogo di scambi
    internal class Mercato
    {
        private Giocatore giocatore;
        // Dictionary per contenere i vari slots del mercato
        private Dictionary<string, string> slots = new Dictionary<string, string> {
            { "Truppe", "Potenzia le tue Truppe" },
            { "Luce Blu", "Scambia Frammenti in cambio di Luce Blu"},
            { "Sacrificio Blu", "Sacrifica le tue Truppe in cambio di Luce Blu"},
            { "Truppe Elite", "Assolda potenti Truppe Elite"},
            { "Produzione", "Migliora la Produzione della Fornace"},
        };

        private int luce_Blu_Turno = 0; 
        private int limite_Blu_Turno = 200; 

        public Mercato(Giocatore giocatore)
        {
            this.giocatore = giocatore;
        }

        // stampa tutti gli slot con formattazione
        private void PrintSlots()
        {
            int i = 1;
            foreach (var slot in slots)
            {
                CC.DarkCyanFr(i++ + ". " + slot.Key); CC.CyanFr(" => "); CC.WhiteFr(slot.Value + "\n");
            }
        }

        // GET // SET

        public int Luce_Blu_Turno { get { return luce_Blu_Turno; } set { luce_Blu_Turno = (value < 0) ? 0 : value; } }

        // Consente di scegliere uno slot del mercato
        public void SceltaSlot()
        {
            CC.DarkGreenFr("||MERCATO||\n");
            int scelta;
            do
            {
                scelta = -1;
                try
                {
                    // stampa slots
                    CC.CyanFr("Cosa vuoi fare?\n");
                    PrintSlots();
                    CC.BlueFr($"{slots.Count + 1}. Avanti\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 1 || scelta > slots.Count + 1) throw new NumeroNonValidoException();

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            // Potenzia Truppe per Frammenti
                            scelta = PrintPotenziaTruppe();
                            break;
                        case 2:
                            // Scambia Frammenti per Luce Blu
                            scelta = PrintScambioBlu();
                            break;
                        case 3:
                            // Sacrifica Truppe per Luce Blu
                            // MAX 200 Luce Blu per tuo turno
                            scelta = PrintSacrificioBlu();
                            break;
                        case 4:
                            scelta = PrintAssoldaTruppeElite();
                            break;
                        case 5:
                            scelta = PotenziaFornace();
                            break;
                        case 0:
                            break;

                    }

                    // se nelle sotto funzioni si è scelto di tornare indietro
                    // gli Slots vanno stampati di nuovo
                    if (scelta != slots.Count + 1) scelta = -1;
                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta < 1 || scelta > slots.Count + 1) scelta = -1; }

            } while (scelta == -1);
        }

        // SACRIFICIO BLU

        private void PrintTruppeSacrificioBlu(List<Truppa> truppas)
        {

            int i = 1;
            switch (truppas[0].Tipologia)
            {
                case Truppa.TT.Mischia:
                    CC.DarkYellowFr("Truppe Mischia:\n");
                    foreach (var t in truppas)
                    {
                        CC.YellowFr($"{i++}. {t.Nome}");
                        CC.BlueFr($" - {t.Conversione_Blu} Luce Blu");
                        CC.WhiteFr($" | Qt. {t.Quantita} ({t.Ferite} ferite, {t.Disponibili} disponibili)\n");

                    }
                    break;
                case Truppa.TT.Distanza:
                    CC.DarkCyanFr("Truppe Distanza:\n");
                    foreach (var t in truppas)
                    {
                        CC.CyanFr($"{i++}. {t.Nome}");
                        CC.BlueFr($" - {t.Conversione_Blu} Luce Blu");
                        CC.WhiteFr($" | Qt. {t.Quantita} ({t.Ferite} ferite, {t.Disponibili} disponibili)\n");
                    }
                    break;
                case Truppa.TT.Tank:
                    CC.DarkMagentaFr("Truppe Tank:\n");
                    foreach (var t in truppas)
                    {
                        CC.MagentaFr($"{i++}. {t.Nome}");
                        CC.BlueFr($" - {t.Conversione_Blu} Luce Blu");
                        CC.WhiteFr($" | Qt. {t.Quantita} ({t.Ferite} ferite, {t.Disponibili} disponibili)\n");
                    }
                    break;
            }

        }

        private int SacrificioBlu(List<Truppa> truppe)
        {
            int scelta;
            int truppeCount = truppe.Count;
            do
            {
                scelta = -1;
                try
                {
                    // Stampa Classi Truppe della Tipologia scelta
                    PrintTruppeSacrificioBlu(truppe);
                    CC.BlueFr("0. Indietro\n");
                    CC.BlueFr($"{truppeCount + 1}. Luce Blu\n");

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
                        if (qt < 0 || qt > truppe[scelta - 1].Quantita) throw new TruppeInsufficientiException();

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

                    // Mostro Luce Blu e ripeto il ciclo
                    if (scelta == truppeCount + 1)
                    {
                        CC.GreenFr("Luce Blu " + giocatore.Altare.Luce_BLU + "\n");
                        scelta = -1;
                    }
                    else
                    {
                        // Stampa riepilogo sacrificio
                        int totale = 0;
                        int totale_Luce_blu = 0;

                        CC.BlueFr("Sacrificare queste Truppe? (Precedenza Truppe Ferite)\n");
                        foreach (var item in qts)
                        {
                            CC.CyanFr($"{truppe[item.Key - 1].Nome}"); CC.WhiteFr($" => {item.Value}\n");
                            totale += item.Value;
                            totale_Luce_blu += truppe[item.Key - 1].Conversione_Blu * item.Value;
                        }
                        CC.DarkYellowFr($"Totale: {totale} Truppe => ");
                        CC.BlueFr($"{totale_Luce_blu} Luce Blu\n");
                        if (totale_Luce_blu + luce_Blu_Turno > limite_Blu_Turno) CC.RedFr($"ATTENZIONE!\n" +
                            $"Non puoi ottenere più di {limite_Blu_Turno} Luce Blu per turno\n" +
                            $"la Luce Blu extra verrà persa...\n");
                        if (giocatore.Altare.Luce_BLU + totale_Luce_blu > giocatore.Altare.Limite) CC.RedFr($"\nATTENZIONE!\n" +
                            $"Questa azione supera il limite MAX di {giocatore.Altare.Limite} Luce Blu\n" +
                            $"la Luce Blu extra verrà persa...\n");
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
                                // Precedenza al sacrificio alle Truppe Ferite
                                int sacrificio = (truppe[item.Key - 1].Ferite - item.Value < 0) ? Math.Abs(truppe[item.Key - 1].Ferite - item.Value) : item.Value;
                                truppe[item.Key - 1].Ferite = -sacrificio;
                                truppe[item.Key - 1].Disponibili = -(item.Value - sacrificio);
                            }
                            int luce_blu = giocatore.Altare.Luce_BLU;
                            giocatore.Altare.Luce_BLU = (totale_Luce_blu + luce_Blu_Turno > limite_Blu_Turno) ? limite_Blu_Turno - luce_Blu_Turno : totale_Luce_blu;
                            CC.CyanFr($"Luce Blu {luce_blu} => {giocatore.Altare.Luce_BLU}\n");
                            luce_Blu_Turno += giocatore.Altare.Luce_BLU - luce_blu;
                        }
                    }
                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FormattazioneNonValidaException) { scelta = -1; }
                catch (TruppeInsufficientiException) { }
                finally { if (scelta < 0 || scelta > 2) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        // Permette di scegliere una tipologia Truppa da sacrificare
        private int PrintSacrificioBlu()
        {
            int scelta;
            int numTipologie = 3;
            do
            {
                scelta = -1;
                try
                {
                    if (giocatore.Altare.Luce_BLU == giocatore.Altare.Limite) throw new LuceBluMaxException();
                    if (luce_Blu_Turno == limite_Blu_Turno) throw new LimiteBluTurnoRaggiuntoException();
                    // Stampa Tipologia Truppe
                    CC.CyanFr("Scegli la categoria\n");
                    PrintTipologieTruppe();
                    CC.BlueFr("0. Indietro\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numTipologie + 1) throw new NumeroNonValidoException();

                    // Indietro
                    if (scelta == 0) break;

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            scelta = SacrificioBlu(giocatore.Truppe_Mischia);
                            break;
                        case 2:
                            scelta = SacrificioBlu(giocatore.Truppe_Distanza);
                            break;
                        case 3:
                            scelta = SacrificioBlu(giocatore.Truppe_Tank);
                            break;
                        case 0:
                            // indietro
                            break;
                    }
                    // si è scelto indietro in SacrificioBlu, si ripete il ciclo
                    if (scelta == 0) scelta = -1;

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (LuceBluMaxException) { break; }
                catch (LimiteBluTurnoRaggiuntoException) { break; }
                finally { if (scelta < 0 || scelta > numTipologie + 1) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        // PRODUZIONE FRAMMENTI

        private int PotenziaFornace()
        {
            int scelta;
            int numScelte = 1;
            Fornace fornace = giocatore.Fornace;
            do
            {
                scelta = -1;
                try
                {

                    // Stampa Truppe
                    CC.CyanFr("Potenzia Fornace?\n");

                    CC.DarkYellowFr($"{numScelte}. Fornace");
                    CC.WhiteFr($" | ");
                    // Lvl Max Fornace
                    if (fornace.Costi_Potenziamento.Length + 1 > fornace.Livello)
                    {
                        CC.WhiteFr($"Lvl.{fornace.Livello} => ");
                        CC.GreenFr($"Lvl.{fornace.Livello + 1}");
                        CC.WhiteFr($" | ");
                        if (fornace.Frammenti < fornace.Costo_Potenziamento)
                        {
                            CC.RedFr($"Costo {fornace.Costo_Potenziamento}");
                        }
                        else
                        {
                            CC.WhiteFr($"Costo {fornace.Costo_Potenziamento}");
                        }
                        PS.kart();
                    }
                    else
                    {
                        CC.GreenFr("Livello MAX\n");
                    }
                    CC.BlueFr("0. Indietro\n");
                    CC.BlueFr($"{numScelte + 1}. Mostra Frammenti\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numScelte + 1) throw new NumeroNonValidoException();

                    // Indietro
                    if (scelta == 0) break;

                    // Frammenti
                    if (scelta == numScelte + 1)
                    {
                        CC.GreenFr($"Frammenti: {fornace.Frammenti}\n");
                        scelta = -1;
                    }
                    else
                    {
                        // Fornace Max
                        if (fornace.Livello == fornace.Costi_Potenziamento.Length + 1) throw new FornaceLivelloMaxException();

                        // Frammenti insufficienti
                        if (fornace.Frammenti < fornace.Costo_Potenziamento) throw new FrammentiInsufficientiException();


                        int frammenti = fornace.Frammenti;
                        int produzione = fornace.Produzione;
                        fornace.Frammenti = -fornace.Costo_Potenziamento;
                        // se tutto va bene si procede
                        fornace.Potenzia();
                        CC.WhiteFr($"Frammenti {frammenti} => {fornace.Frammenti}\n");
                    }

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FornaceLivelloMaxException) { }
                catch (FrammentiInsufficientiException) { }
                finally { if (scelta < 0 || scelta > numScelte + 1) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        // TRUPPE ELITE

        private void PrintTipologiaTruppeElite()
        {
            int i = 1;
            // Guerra
            CC.GrayFr($"{i++}. {giocatore.Truppe_Elite_Guerra_Tank[0].Tipologia}\n");

            // Supporto
            CC.DarkGreenFr($"{i++}. {giocatore.Truppe_Elite_Supporto[0].Tipologia}\n");
        }

        private void PrintTipologieTruppeElite_Guerra()
        {
            int i = 1;

            // TRUPPE MISCHIA
            CC.DarkYellowFr($"{i++}. {giocatore.Truppe_Elite_Guerra_Mischia[0].Tipologia_Guerra}\n");

            // TRUPPE DISTANZA
            CC.DarkCyanFr($"{i++}. {giocatore.Truppe_Elite_Guerra_Distanza[0].Tipologia_Guerra}\n");

            // TRUPPE TANK
            CC.DarkMagentaFr($"{i++}. {giocatore.Truppe_Elite_Guerra_Tank[0].Tipologia_Guerra}\n");

        }

        private void PrintTruppeElite_Supporto(List<Truppa_Elite> truppa_Elites)
        {
            int i = 1;
            foreach (var truppa in truppa_Elites)
            {
                // stampo Nome Truppa e costo con colore condizionale
                CC.DarkCyanFr($"{i++}. {truppa.Nome}");

                CC.WhiteFr(" | ");
                if (truppa.Assoldata)
                {
                    CC.GreenFr("Con te\n");
                }
                else
                {
                    if (truppa.Costo > giocatore.Fornace.Frammenti)
                    {
                        CC.RedFr($"Costo {truppa.Costo}");
                    }
                    else
                    {
                        CC.WhiteFr($"Costo {truppa.Costo}");
                    }
                    PS.kart();

                }

            }
        }

        // Mostra elenco TruppeElite_Supporto e consente di potenziare quella scelta
        private int AssoldaTruppeElite_Supporto()
        {
            int scelta;
            int numTruppeElite = giocatore.Truppe_Elite_Supporto.Count;
            do
            {
                scelta = -1;
                try
                {

                    // Stampa Truppe
                    CC.CyanFr("Quale Truppa Elite Supporto vuoi Assoldare?\n");
                    PrintTruppeElite_Supporto(giocatore.Truppe_Elite_Supporto);
                    CC.BlueFr("0. Indietro\n");
                    CC.BlueFr($"{numTruppeElite + 1}. Mostra Frammenti\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numTruppeElite + 1) throw new NumeroNonValidoException();

                    // Indietro
                    if (scelta == 0) break;

                    // Frammenti
                    if (scelta == numTruppeElite + 1)
                    {
                        CC.GreenFr($"Frammenti: {giocatore.Fornace.Frammenti}\n");
                        scelta = -1;
                    }
                    else
                    {
                        Truppa_Elite truppa_Elite = giocatore.Truppe_Elite_Supporto[scelta - 1];

                        // Truppa Elite già assoldata
                        if (truppa_Elite.Assoldata == true) throw new TruppaEliteGiaAssoldataException();

                        CC.YellowFr($"Descrizione: ");CC.WhiteFr($"{truppa_Elite.Descrizione}\n");
                        CC.DarkGreenFr($"Abilita - {truppa_Elite.Nome_Abilita}: "); CC.WhiteFr($"{truppa_Elite.Descrizione_Abilita}\n");

                        CC.DarkCyanFr("Assoldare Truppa Elite?\n");
                        CC.WhiteFr("1. Si\n" +
                            "2. No\n");
                        sceltaStr = Console.ReadLine();
                        if (sceltaStr.Equals("")) throw new StringaVuotaException();
                        if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                        if (scelta < 1 || scelta > 2) throw new NumeroNonValidoException();

                        if (scelta == 1)
                        {
                            // Frammenti insufficienti
                            if (truppa_Elite.Costo > giocatore.Fornace.Frammenti) throw new FrammentiInsufficientiException();

                            int frammenti = giocatore.Fornace.Frammenti;
                            giocatore.Fornace.Frammenti = -truppa_Elite.Costo;
                            CC.WhiteFr($"Frammenti {frammenti} => {giocatore.Fornace.Frammenti}\n");

                            // se tutto va bene si procede
                            truppa_Elite.Assolda();
                            CC.GreenFr(truppa_Elite.Nome);
                            CC.WhiteFr($" si è unit* alla tua causa!\n");
                        }
                    }

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FrammentiInsufficientiException) { }
                catch (TruppaEliteGiaAssoldataException) { }
                finally { if (scelta < 0 || scelta > numTruppeElite + 1) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        // Stampa formattata dei costi delle Truppe Elite Guerra passate
        private void PrintTruppeElite_Guerra(List<Truppa_Elite_Guerra> truppa_Elites)
        {
            int i = 1;
            foreach (var truppa in truppa_Elites)
            {
                // stampo Nome Truppa, il colore cambia in base alla Tipologia
                switch (truppa.Tipologia_Guerra)
                {
                    case Truppa_Elite_Guerra.TT_Guerra.Mischia:
                        CC.YellowFr($"{i++}. {truppa.Nome}");
                        break;
                    case Truppa_Elite_Guerra.TT_Guerra.Distanza:
                        CC.CyanFr($"{i++}. {truppa.Nome}");
                        break;
                    case Truppa_Elite_Guerra.TT_Guerra.Tank:
                        CC.MagentaFr($"{i++}. {truppa.Nome}");
                        break;
                }

                CC.WhiteFr(" | ");
                if (truppa.Assoldata)
                {
                    CC.GreenFr("Con te\n");
                }
                else
                {
                    if (truppa.Costo > giocatore.Fornace.Frammenti)
                    {
                        CC.RedFr($"Costo {truppa.Costo}\n");
                    }
                    else
                    {
                        CC.WhiteFr($"Costo {truppa.Costo}\n");
                    }
                }
            }
        }

        // Aggiunge la Truppa Elite Guerra al tuo esercito se hai Frammenti sufficienti
        private int AssoldaTruppeElite_Guerra(List<Truppa_Elite_Guerra> truppa_Elites)
        {
            int scelta;
            int numTruppeElite = truppa_Elites.Count;
            do
            {
                scelta = -1;
                try
                {

                    // Stampa Truppe
                    CC.CyanFr("Quale Eroe vuoi Assoldare?\n");
                    PrintTruppeElite_Guerra(truppa_Elites);
                    CC.BlueFr("0. Indietro\n");
                    CC.BlueFr($"{numTruppeElite + 1}. Mostra Frammenti\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numTruppeElite + 1) throw new NumeroNonValidoException();

                    // Indietro
                    if (scelta == 0) break;

                    // Frammenti
                    if (scelta == numTruppeElite + 1)
                    {
                        CC.GreenFr($"Frammenti: {giocatore.Fornace.Frammenti}\n");
                        scelta = -1;
                    }
                    else
                    {
                        Truppa_Elite truppa_Elite = truppa_Elites[scelta - 1];

                        // Truppa Elite già assoldata
                        if (truppa_Elite.Assoldata == true) throw new TruppaEliteGiaAssoldataException();
                        CC.YellowFr($"Descrizione: "); CC.WhiteFr($"{truppa_Elite.Descrizione}\n");
                        CC.DarkGreenFr($"Abilita - {truppa_Elite.Nome_Abilita}: "); CC.WhiteFr($"{truppa_Elite.Descrizione_Abilita}\n");

                        CC.DarkCyanFr("Assoldare Truppa Elite?\n");
                        CC.WhiteFr("1. Si\n" +
                            "2. No\n");
                        sceltaStr = Console.ReadLine();
                        if (sceltaStr.Equals("")) throw new StringaVuotaException();
                        if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                        if (scelta < 1 || scelta > 2) throw new NumeroNonValidoException();

                        if (scelta == 1)
                        {
                            // Frammenti insufficienti
                            if (truppa_Elite.Costo > giocatore.Fornace.Frammenti) throw new FrammentiInsufficientiException();

                            int frammenti = giocatore.Fornace.Frammenti;
                            giocatore.Fornace.Frammenti = -truppa_Elite.Costo;
                            CC.WhiteFr($"Frammenti {frammenti} => {giocatore.Fornace.Frammenti}\n");

                            // se tutto va bene si procede
                            truppa_Elite.Assolda();
                            CC.GreenFr(truppa_Elite.Nome);
                            CC.WhiteFr($" si è unit* alla tua causa!\n");
                        }
                    }

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FrammentiInsufficientiException) { }
                catch (TruppaEliteGiaAssoldataException) { }
                finally { if (scelta < 0 || scelta > numTruppeElite + 1) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        // Mostra elenco Tipologie TruppeElite_Guerra e consente di visualizzare la lista scelta
        private int PrintAssoldaTruppeElite_Guerra()
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
                    PrintTipologieTruppeElite_Guerra();
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
                            scelta = AssoldaTruppeElite_Guerra(giocatore.Truppe_Elite_Guerra_Mischia);
                            break;
                        case 2:
                            scelta = AssoldaTruppeElite_Guerra(giocatore.Truppe_Elite_Guerra_Distanza);
                            break;
                        case 3:
                            scelta = AssoldaTruppeElite_Guerra(giocatore.Truppe_Elite_Guerra_Tank);
                            break;
                        case 0:
                            break;
                    }

                    if (scelta == 0) scelta = -1;

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FrammentiInsufficientiException) { }
                catch (TruppaEliteGiaAssoldataException) { }
                finally { if (scelta < 0 || scelta > numTipologieGuerra) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        // Permette di scegliere la Truppa Elite da potenziare
        private int PrintAssoldaTruppeElite()
        {
            int scelta;
            do
            {
                scelta = -1;
                try
                {
                    // stampa slots
                    CC.CyanFr("Scegli tipologia Truppa Elite\n");
                    PrintTipologiaTruppeElite();
                    CC.BlueFr("0. Indietro\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > slots.Count) throw new NumeroNonValidoException();

                    if (scelta == 0) break;

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            scelta = PrintAssoldaTruppeElite_Guerra();
                            break;
                        case 2:
                            scelta = AssoldaTruppeElite_Supporto();
                            break;

                        case 0:
                            break;

                    }

                    // si è scelto indietro in AssoldaTruppeElite_Supporto()
                    if (scelta == 0) scelta = -1;
                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta < 0 || scelta > slots.Count) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        // SCAMBIO BLU 

        // Quantita' e prezzi Luce Blu
        int[] qt = { 100, 300, 700 };
        int[] prices = { 1000, 2950, 6500 };

        private void PrintQuantitaLuceBlu()
        {
            for (int i = 0; i < qt.Length; i++)
            {
                CC.DarkCyanFr($"{i + 1}. {qt[i]} Luce Blu");

                CC.WhiteFr(" | ");
                if (giocatore.Altare.Luce_BLU == giocatore.Altare.Limite)
                {
                    CC.GreenFr("MAX\n");
                }
                else
                {
                    if (giocatore.Fornace.Frammenti < prices[i])
                    {
                        CC.RedFr($"{prices[i]} Frammenti\n");
                    }
                    else
                    {
                        CC.WhiteFr($"{prices[i]} Frammenti\n");
                    }
                }




            }
        }


        // Effettua lo Scambio blu scelto
        private void ScambioBlu(int scelta)
        {
            int frammenti = giocatore.Fornace.Frammenti;
            int luce_Blu = giocatore.Altare.Luce_BLU;

            giocatore.Fornace.Frammenti = -prices[scelta - 1];
            giocatore.Altare.Luce_BLU = qt[scelta - 1];

            CC.GreenFr("Successo!\n");
            CC.WhiteFr($"Frammenti {frammenti} => {giocatore.Fornace.Frammenti}\n");
            CC.WhiteFr($"Luce Blu {luce_Blu} => {giocatore.Altare.Luce_BLU}\n");

        }

        // Stampa gli Scambi Blu disponibili
        private int PrintScambioBlu()
        {
            int scelta;
            int pricesLength = prices.Length;
            do
            {
                scelta = -1;
                try
                {
                    // stampa scambi disponibili
                    CC.CyanFr("Scegli la quantita'\n");
                    PrintQuantitaLuceBlu();
                    CC.BlueFr($"{pricesLength + 1}. Mostra Frammenti\n");
                    CC.BlueFr("0. Indietro\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > pricesLength + 1) throw new NumeroNonValidoException();

                    // indietro scelta == 0
                    if (scelta == 0) break;

                    if (scelta != pricesLength + 1)
                    {
                        // Frammenti insufficienti, si esce dal ciclo
                        if (giocatore.Fornace.Frammenti < prices[scelta - 1]) throw new FrammentiInsufficientiException();

                        // Luce Blu Max, si esce dal ciclo
                        if (giocatore.Altare.Luce_BLU == giocatore.Altare.Limite) throw new LuceBluMaxException();
                    }

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            ScambioBlu(scelta);
                            break;
                        case 2:
                            ScambioBlu(scelta);
                            break;
                        case 3:
                            ScambioBlu(scelta);
                            break;
                        case 0:
                            break;
                        // case prices.Length + 1
                        default:
                            CC.GreenFr($"Frammenti: {giocatore.Fornace.Frammenti}\n");
                            scelta = -1;
                            break;

                    }
                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FrammentiInsufficientiException) { break; }
                catch (LuceBluMaxException) { break; }
                finally { if (scelta < 0 || scelta > pricesLength + 1) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        // TRUPPE

        // Stampa le Tipologie di Truppe con formattazione
        private void PrintTipologieTruppe()
        {
            int i = 1;

            // TRUPPE MISCHIA
            CC.DarkYellowFr($"{i++}. {giocatore.Truppe_Mischia[0].Tipologia}\n");

            // TRUPPE DISTANZA
            CC.DarkCyanFr($"{i++}. {giocatore.Truppe_Distanza[0].Tipologia}\n");

            // TRUPPE TANK
            CC.DarkMagentaFr($"{i++}. {giocatore.Truppe_Tank[0].Tipologia}\n");

        }

        // Stampa le Truppe di una Tipologia, con Lvl e costo potenziamento
        private void PrintTruppe(List<Truppa> truppas)
        {
            int i = 1;
            foreach (var truppa in truppas)
            {
                // stampo Nome Truppa, il colore cambia in base alla Tipologia
                switch (truppa.Tipologia)
                {
                    case Truppa.TT.Mischia:
                        CC.YellowFr($"{i++}. {truppa.Nome}");
                        break;
                    case Truppa.TT.Distanza:
                        CC.CyanFr($"{i++}. {truppa.Nome}");
                        break;
                    case Truppa.TT.Tank:
                        CC.MagentaFr($"{i++}. {truppa.Nome}");
                        break;
                }

                // Stampo effetto potenziamento Lvl. 1 => Lvl. 2
                if (truppa.Livello < truppa.Costi_Potenziamento.Length + 1)
                {
                    CC.WhiteFr($" | Lvl. {truppa.Livello} => ");
                    CC.GreenFr($"Lvl. {truppa.Livello + 1}");
                    CC.WhiteFr(" | ");
                }
                else
                {
                    // Stampo solo il livello corrente se Lvl. MAX
                    CC.WhiteFr($" | Lvl. {truppa.Livello} | ");
                }

                // Stampo il costo Potenziamento o Livello MAX
                if (truppa.Livello == truppa.Costi_Potenziamento.Length + 1)
                {
                    CC.GreenFr($"Livello MAX\n");
                }
                else if (truppa.Costo_Potenziamento > giocatore.Fornace.Frammenti)
                {
                    CC.RedFr($"Costo {truppa.Costo_Potenziamento}\n");
                }
                else
                {
                    CC.WhiteFr($"Costo {truppa.Costo_Potenziamento}\n");
                }
            }
        }


        // Permette di scegliere la Classe da potenziare e ne effettua il miglioramento
        // Non deve necessariamente essere già nel tuo esercito

        private int PotenziaTruppe(List<Truppa> truppe)
        {
            int scelta;
            int truppeCount = truppe.Count;
            do
            {
                scelta = -1;
                try
                {
                    // Stampa Classi Truppe della Tipologia scelta
                    CC.CyanFr("Quale classe vuoi potenziare?\n");
                    PrintTruppe(truppe);
                    CC.BlueFr("0. Indietro\n");
                    CC.BlueFr($"{truppeCount + 1}. Frammenti\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > truppeCount + 1) throw new NumeroNonValidoException();

                    // indietro scelta == 0
                    if (scelta == 0) break;

                    // Mostro frammenti e ripeto il ciclo
                    if (scelta == truppeCount + 1)
                    {
                        CC.GreenFr("Frammenti " + giocatore.Fornace.Frammenti + "\n");
                        scelta = -1;
                    }
                    else // effettuo il potenziamento se ho frammenti a sufficienza
                    {
                        // Frammenti insufficienti, si esce dal ciclo
                        if (giocatore.Fornace.Frammenti < truppe[scelta - 1].Costo_Potenziamento)
                            throw new FrammentiInsufficientiException();

                        // Truppa Lvl MAX, si esce dal ciclo
                        if (truppe[scelta - 1].Livello == truppe[scelta - 1].Costi_Potenziamento.Length + 1)
                            throw new TruppaLivelloMassimoException();

                        // se tutto va bene si procede
                        int frammenti = giocatore.Fornace.Frammenti;
                        giocatore.Fornace.Frammenti = -truppe[scelta - 1].Costo_Potenziamento;
                        CC.WhiteFr($"Frammenti {frammenti} => {giocatore.Fornace.Frammenti}\n");

                        truppe[scelta - 1].Potenzia();
                    }
                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (FrammentiInsufficientiException) { break; }
                catch (TruppaLivelloMassimoException) { break; }
                finally { if (scelta < 0 || scelta > truppeCount + 1) scelta = -1; }

            } while (scelta == -1);

            return scelta;

        }

        // Permtte di stampare le tipologie di Truppe disponibili
        private int PrintPotenziaTruppe()
        {
            int scelta;
            int numTipologie = 3;
            do
            {
                scelta = -1;
                try
                {

                    // Stampa Truppe
                    CC.CyanFr("Cosa vuoi potenziare?\n");
                    PrintTipologieTruppe();
                    CC.BlueFr("0. Indietro\n");
                    CC.BlueFr($"{numTipologie + 1}. Mostra Frammenti\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numTipologie + 1) throw new NumeroNonValidoException();

                    // Indietro
                    if (scelta == 0) break;

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            scelta = PotenziaTruppe(giocatore.Truppe_Mischia);
                            break;
                        case 2:
                            scelta = PotenziaTruppe(giocatore.Truppe_Distanza);
                            break;
                        case 3:
                            scelta = PotenziaTruppe(giocatore.Truppe_Tank);
                            break;
                        case 0:
                            // indietro
                            break;
                        // case numTipologie + 1
                        default:
                            CC.GreenFr($"Frammenti: {giocatore.Fornace.Frammenti}\n");
                            scelta = -1;
                            break;
                    }
                    // si è scelto indietro in PotenziaTruppe, si ripete il ciclo
                    if (scelta == 0) scelta = -1;

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta < 0 || scelta > numTipologie + 1) scelta = -1; }

            } while (scelta == -1);

            return scelta;

        }
    }
}
