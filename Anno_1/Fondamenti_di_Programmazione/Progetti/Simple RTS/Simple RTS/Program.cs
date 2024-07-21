using Simple_RTS.Base;
using Simple_RTS.Combattimento;
using Simple_RTS.Exceptions;
using Simple_RTS.Exceptions.Gioco;
using Simple_RTS.Exceptions.Input;
using Simple_RTS.Truppe;
using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS
{
    internal class Program
    {
        // GIOCATORE 1
        static Altare altare1 = new Altare();
        static Fornace fornace1 = new Fornace();
        static Torre torre1 = new Torre();
        static Arena arena1 = new Arena();
        static Mercato mercato1;
        static Giocatore giocatore1;

        // GIOCATORE 2
        static Altare altare2 = new Altare();
        static Fornace fornace2 = new Fornace();
        static Torre torre2 = new Torre();
        static Arena arena2 = new Arena();
        static Mercato mercato2;
        static Giocatore giocatore2;

        // ATTACCO
        static Attacco attacco = new Attacco();

        // TURNO
        static int turno = 1;


        static bool Richiesta_Attacco(Giocatore giocatore)
        {
            int scelta;
            do
            {
                scelta = -1;
                try
                {

                    // Stampa decisioni
                    CC.CyanFr($"{giocatore.Nome} | Vuoi attaccare?\n");

                    CC.WhiteFr("1. Si\n" +
                        "2. No\n");

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 1 || scelta > 2) throw new NumeroNonValidoException();

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta != 1 && scelta != 2) scelta = -1; }

            } while (scelta == -1);

            return (scelta == 1);
        }

        static int Turno_G1()
        {
            CleanConsole(giocatore1);
            CC.RedFr($"TURNO {turno}\n");
            int result = -1;

            // Cooldown abilita
            Eroi_e_Elite_Reset(giocatore1);
            // Reset limite sacrificio blu
            ResetLimiteSacrificioBlu(mercato1);

            // PRIMA FASE
            CC.GreenFr("<^> PRIMA FASE <^>\n");
            fornace1.Fusione();
            altare1.FuriaDivina();
            mercato1.SceltaSlot();
            // SECONDA FASE
            CleanConsole(giocatore1);
            CC.GreenFr("<^> SECONDA FASE <^>\n");
            arena1.PrintAssoldaTruppe();
            CleanConsole(giocatore1);
            altare1.PrintEvocaEroi();
            CleanConsole(giocatore1);
            arena1.PrintSceltaAbilitaEliteSupporto();
            CleanConsole(giocatore1);

            // ATTACCO
            if (turno > 6)
            {
                CC.GreenFr("<^> ATTACCO <^>\n");
                if (Richiesta_Attacco(giocatore1))
                {
                    attacco.SetUp(giocatore1, giocatore2);
                    attacco.Reset();
                    if (attacco.Preparazione(giocatore1) != -1)
                    {
                        CC.Clean();
                        result = attacco.AAAttacco();
                    }
                    CC.WhiteFr("Avanti");
                    Console.ReadLine();
                }

            }
            return result;
        }

        static int Turno_G2()
        {
            CleanConsole(giocatore2);
            CC.RedFr($"TURNO {turno}\n");
            int result = -1;

            // Cooldown abilita
            Eroi_e_Elite_Reset(giocatore2);
            // Reset limite sacrificio blu
            ResetLimiteSacrificioBlu(mercato2);

            // PRIMA FASE
            CC.GreenFr("<^> PRIMA FASE <^>\n");
            fornace2.Fusione();
            altare2.FuriaDivina();
            mercato2.SceltaSlot();
            // SECONDA FASE
            CleanConsole(giocatore2);
            CC.GreenFr("<^> SECONDA FASE <^>\n");
            arena2.PrintAssoldaTruppe();
            CleanConsole(giocatore2);
            altare2.PrintEvocaEroi();
            CleanConsole(giocatore2);
            arena2.PrintSceltaAbilitaEliteSupporto();
            CleanConsole(giocatore2);


            // ATTACCO
            if (turno > 6)
            {
                CC.GreenFr("<^> ATTACCO <^>\n");
                if (Richiesta_Attacco(giocatore2))
                {
                    attacco.SetUp(giocatore2, giocatore1);
                    attacco.Reset();
                    if (attacco.Preparazione(giocatore2) != -1)
                    {
                        CC.Clean();
                        result = attacco.AAAttacco();
                    }
                    CC.WhiteFr("Avanti");
                    Console.ReadLine();
                }

            }
            return result;
        }

        static void CleanConsole(Giocatore giocatore)
        {
            CC.Clean();
            if (giocatore.Nome.Equals(giocatore1.Nome))
            {
                CC.BlueFr($"----------------------------------------------------\n" +
                $"TURNO di {giocatore.Nome}\n" +
                $"----------------------------------------------------\n");
            }
            else
            {
                CC.MagentaFr($"----------------------------------------------------\n" +
                $"TURNO di {giocatore.Nome}\n" +
                $"----------------------------------------------------\n");
            }
        }

        static void Eroi_e_Elite_Reset(Giocatore giocatore)
        {
            // Eroi
            foreach (var eroe in giocatore.Eroi_Attacco)
            {
                if (eroe.Cooldown_Rimanente > 0) eroe.CoolDownAbilita();
            }
            foreach (var eroe in giocatore.Eroi_Difesa)
            {
                if (eroe.Cooldown_Rimanente > 0) eroe.CoolDownAbilita();
            }
            // Truppe Elite
            // Guerra
            foreach (var elite in giocatore.Truppe_Elite_Guerra_Mischia)
            {
                if (elite.Cooldown_Rimanente > 0) elite.CoolDownAbilita();
            }
            foreach (var elite in giocatore.Truppe_Elite_Guerra_Distanza)
            {
                if (elite.Cooldown_Rimanente > 0) elite.CoolDownAbilita();
            }
            foreach (var elite in giocatore.Truppe_Elite_Guerra_Tank)
            {
                if (elite.Cooldown_Rimanente > 0) elite.CoolDownAbilita();
            }
            // Supporto
            foreach (var elite in giocatore.Truppe_Elite_Supporto)
            {
                if (elite.Cooldown_Rimanente > 0) elite.CoolDownAbilita();
            }
        }

        static void ResetLimiteSacrificioBlu(Mercato mercato){
            mercato.Luce_Blu_Turno = 0;
        }

        static void Main(string[] args)
        {
            String nome_g1, nome_g2;
            do
            {
                CC.BlueFr("G1, inserisci il tuo nome: ");
                try
                {
                    nome_g1 = Console.ReadLine();
                    if (nome_g1.Equals("")) throw new StringaVuotaException();
                    break;
                }
                catch (StringaVuotaException) { }
            } while (true);

            do
            {
                CC.MagentaFr("G2, inserisci il tuo nome: ");
                try
                {
                    nome_g2 = Console.ReadLine();
                    if (nome_g2.Equals("")) throw new StringaVuotaException();
                    if (nome_g2.Equals(nome_g1)) throw new NomeUgualeException();
                    break;
                }
                catch (StringaVuotaException) { }
                catch (NomeUgualeException) { }
            } while (true);

            // SET UP G1
            giocatore1 = new Giocatore(nome_g1);

            mercato1 = new Mercato(giocatore1);
            arena1.SetUp(giocatore1);
            altare1.SetUp(giocatore1);

            // SET UP G2
            giocatore2 = new Giocatore(nome_g2);

            mercato2 = new Mercato(giocatore2);
            arena2.SetUp(giocatore2);
            altare2.SetUp(giocatore2);

            // SET UP DEAD LOCK
            giocatore1.SetUp(attacco, fornace1, altare1, torre1, torre2, giocatore2.Truppe_Mischia, giocatore2.Truppe_Distanza, giocatore2.Truppe_Tank);

            giocatore2.SetUp(attacco, fornace2, altare2, torre2, torre1, giocatore1.Truppe_Mischia, giocatore1.Truppe_Distanza, giocatore1.Truppe_Tank);

            // Bonus G2 di 200 frammenti per non essere in svantaggio

            fornace2.Fusione();

            // INIZIO


            do
            {
                if (Turno_G1() == 3)
                {
                    if (giocatore1.Torre.Vita == 0) break;
                }
                turno += 1;

                if (Turno_G2() == 3)
                {
                    if (giocatore2.Torre.Vita == 0) break;
                }
                turno += 1;

            } while (true);

        }
    }
}
