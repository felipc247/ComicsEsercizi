using Simple_RTS.Exceptions.Input;
using Simple_RTS.Exceptions;
using Simple_RTS.Exceptions.Risorse;
using Simple_RTS.Truppe;
using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_RTS.Eroi;
using Simple_RTS.Exceptions.Frammenti;
using Simple_RTS.Exceptions.Truppe_Elite;
using Simple_RTS.Truppe_Elite.Guerra;
using Simple_RTS.Exceptions.Eroi;

namespace Simple_RTS.Base
{
    // Dove ottieni Luce Blu e puoi evocare Eroi in cambio di Anime
    internal class Altare
    {
        private int anime_ATK = 0;
        private int anime_DIF = 0;
        private int luce_BLU = 0;
        private int produzione = 100;
        private int limite = 1000;
        private Giocatore giocatore;

        // per abilita
        private int produzione_bonus = 0;

        public Altare() { }

        public void SetUp(Giocatore giocatore)
        {
            this.giocatore = giocatore;
        }

        // GET // SET
        public int Anime_ATK
        {
            get { return anime_ATK; }
            set { anime_ATK = (anime_ATK + value < 0) ? 0 : anime_ATK + value; }
        }

        public int Anime_DIF
        {
            get { return anime_DIF; }
            set { anime_DIF = (anime_DIF + value < 0) ? 0 : anime_DIF + value; }
        }

        public int Luce_BLU
        {
            get { return luce_BLU; }
            set
            {
                if (luce_BLU + value > limite) { if (luce_BLU == 1000) throw new LuceBluMaxException(); luce_BLU = limite; }
                else
                { luce_BLU = (luce_BLU + value < 0) ? 0 : luce_BLU + value; }
            }
        }

        public int Limite
        {
            get { return limite; }
        }

        public int Produzione_Bonus
        {
            get { return produzione_bonus; }
            set { produzione_bonus = value; }
        }

        // METODI

        public void FuriaDivina()
        {
            int luce_BLUTemp = luce_BLU;
            if (Luce_BLU + produzione + produzione_bonus > limite)
            {
                CC.BlueFr("Furia Divina: "); CC.WhiteFr($"+ {0} Luce Blu\n");
            }
            else
            {
                CC.BlueFr("Furia Divina: "); CC.WhiteFr($"+ {produzione + produzione_bonus} Luce Blu\n");
            }

            try
            {
                Luce_BLU = produzione + produzione_bonus;
            }
            catch (LuceBluMaxException) { }

            CC.WhiteFr($"Luce Blu {luce_BLUTemp} => {luce_BLU}\n");
        }

        // EVOCAZIONE Eroi

        public void PrintTipologieEroi()
        {
            int i = 1;
            // Guerra
            CC.DarkMagentaFr($"{i++}. {giocatore.Eroi_Attacco[0].Tipologia}\n");

            // Supporto
            CC.DarkCyanFr($"{i++}. {giocatore.Eroi_Difesa[0].Tipologia}\n");
        }

        public void PrintEroi(List<Eroe> eroi) {
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
                    CC.GreenFr("Con te\n");
                }
                else
                {
                    switch (eroe.Tipologia)
                    {
                        case Eroe.TT.Attacco:
                            if (eroe.Costo > giocatore.Altare.Anime_ATK)
                            {
                                CC.RedFr($"Costo {eroe.Costo}\n");
                            }
                            else
                            {
                                CC.WhiteFr($"Costo {eroe.Costo}\n");
                            }
                            break;
                        case Eroe.TT.Difesa:
                            if (eroe.Costo > giocatore.Altare.Anime_DIF)
                            {
                                CC.RedFr($"Costo {eroe.Costo}\n");
                            }
                            else
                            {
                                CC.WhiteFr($"Costo {eroe.Costo}\n");
                            }
                            break;
                    }

                }
            }
        }

        public int EvocaEroe(List<Eroe> eroi) {
            int scelta;
            int numEroi = eroi.Count;
            do
            {
                scelta = -1;
                try
                {

                    // Stampa Truppe
                    CC.CyanFr("Quale Eroe vuoi Evocare?\n");
                    PrintEroi(eroi);
                    CC.BlueFr("0. Indietro\n");
                    switch (eroi[0].Tipologia)
                    {
                        case Eroe.TT.Attacco:
                            CC.BlueFr($"{numEroi + 1}. Mostra Anime ATK\n");
                            break;
                        case Eroe.TT.Difesa:
                            CC.BlueFr($"{numEroi + 1}. Mostra Anime DIF\n");
                            break;
                    }

                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > numEroi + 1) throw new NumeroNonValidoException();

                    // Indietro
                    if (scelta == 0) break;

                    // Stampa Anime
                    if (scelta == numEroi + 1)
                    {
                        switch (eroi[0].Tipologia)
                        {
                            case Eroe.TT.Attacco:
                                CC.GreenFr($"{giocatore.Altare.Anime_ATK} Anime ATK\n");
                                break;
                            case Eroe.TT.Difesa:
                                CC.GreenFr($"{giocatore.Altare.Anime_DIF} Anime DIF\n");
                                break;
                        }
                        scelta = -1;
                    }
                    else
                    {
                        Eroe eroe = eroi[scelta - 1];

                        // Eroe già evocato
                        if (eroe.Evocato == true) throw new EroeGiaEvocatoException();
                        CC.YellowFr($"Descrizione: "); CC.WhiteFr($"{eroe.Descrizione}\n");
                        CC.DarkGreenFr($"Abilita - {eroe.Nome_Abilita}: "); CC.WhiteFr($"{eroe.Descrizione_Abilita}\n");

                        CC.DarkCyanFr("Evocare Eroe?\n");
                        CC.WhiteFr("1. Si\n" +
                            "2. No\n");
                        sceltaStr = Console.ReadLine();
                        if (sceltaStr.Equals("")) throw new StringaVuotaException();
                        if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                        if (scelta < 1 || scelta > 2) throw new NumeroNonValidoException();

                        if (scelta == 1)
                        {
                            int anime = 0;
                            switch (eroe.Tipologia)
                            {
                                case Eroe.TT.Attacco:
                                    // Anime insufficienti
                                    if (eroe.Costo > giocatore.Altare.Anime_ATK) throw new AnimeAtkInsufficientiException();
                                    anime = giocatore.Altare.Anime_ATK;
                                    giocatore.Altare.Anime_ATK = -eroe.Costo;
                                    CC.WhiteFr($"Anime Atk {anime} => {giocatore.Altare.Anime_ATK}\n");
                                    break;
                                case Eroe.TT.Difesa:
                                    // Anime insufficienti
                                    if (eroe.Costo > giocatore.Altare.Anime_DIF) throw new AnimeDifInsufficientiException();
                                    anime = giocatore.Altare.Anime_DIF;
                                    giocatore.Altare.Anime_DIF = -eroe.Costo;
                                    CC.WhiteFr($"Anime Dif {anime} => {giocatore.Altare.Anime_DIF}\n");
                                    break;
                            }                           

                            // se tutto va bene si procede
                            eroe.Evoca();
                            CC.GreenFr(eroe.Nome);
                            CC.WhiteFr($" è al tuo fianco ora!\n");
                        }
                    }

                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                catch (EroeGiaEvocatoException) { }
                catch (AnimeAtkInsufficientiException) { }
                catch (AnimeDifInsufficientiException) { }
                finally { if (scelta < 0 || scelta > numEroi + 1) scelta = -1; }

            } while (scelta == -1);

            return scelta;
        }

        public void PrintEvocaEroi()
        {
            CC.DarkGreenFr("||EVOCA EROI||\n");
            int scelta;
            int tipologieEroe = 2;
            do
            {
                scelta = -1;
                try
                {
                    // stampa slots
                    CC.CyanFr("Scegli tipologia Eroe\n");
                    PrintTipologieEroi();
                    CC.BlueFr($"{tipologieEroe + 1}. Avanti\n");
                    
                    // lettura scelta
                    String sceltaStr = Console.ReadLine();

                    // gestione eccezioni input
                    if (sceltaStr.Equals("")) throw new StringaVuotaException();
                    if (!int.TryParse(sceltaStr, out scelta)) throw new InputNonValidoException();
                    if (scelta < 0 || scelta > tipologieEroe + 1) throw new NumeroNonValidoException();

                    // Avanti
                    if (scelta == tipologieEroe + 1) break;

                    // se tutto va bene si procede
                    switch (scelta)
                    {
                        case 1:
                            scelta = EvocaEroe(giocatore.Eroi_Attacco);
                            break;
                        case 2:
                            scelta = EvocaEroe(giocatore.Eroi_Difesa);
                            break;                 
                        case 0:
                            // indietro
                            break;

                    }

                    // si è scelto indietro ()
                    if (scelta == 0) scelta = -1;
                }
                catch (StringaVuotaException) { }
                catch (InputNonValidoException) { scelta = -1; }
                catch (NumeroNonValidoException) { }
                finally { if (scelta != tipologieEroe + 1) scelta = -1; }

            } while (scelta == -1);

        }




    }
}
