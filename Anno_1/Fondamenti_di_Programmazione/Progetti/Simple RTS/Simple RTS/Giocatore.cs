using Simple_RTS.Base;
using Simple_RTS.Combattimento;
using Simple_RTS.Eroi;
using Simple_RTS.Eroi.TipAttacco;
using Simple_RTS.Eroi.TipDifesa;
using Simple_RTS.Truppe_Elite.Guerra;
using Simple_RTS.Truppe_Elite.Guerra.Mischia;
using Simple_RTS.Truppe_Elite.Guerra.Tank;
using Simple_RTS.Truppe_Elite.Supporto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Truppe
{
    internal class Giocatore
    {
        private String nome;

        // Risorse sono nelle strutture

        // TRUPPE/TRUPPE ELITE/EROI
        // Truppe
        private List<Truppa> truppe_Mischia = new List<Truppa>();
        private List<Truppa> truppe_Distanza = new List<Truppa>();
        private List<Truppa> truppe_Tank = new List<Truppa>();

        // Truppe Elite

        // Guerra
        private List<Truppa_Elite_Guerra> truppe_Elite_Guerra_Mischia = new List<Truppa_Elite_Guerra>();
        private List<Truppa_Elite_Guerra> truppe_Elite_Guerra_Distanza = new List<Truppa_Elite_Guerra>();
        private List<Truppa_Elite_Guerra> truppe_Elite_Guerra_Tank = new List<Truppa_Elite_Guerra>();

        // Supporto
        private List<Truppa_Elite> truppe_Elite_Supporto = new List<Truppa_Elite>();

        // Eroi

        // Attacco
        private List<Eroe> eroi_attacco = new List<Eroe>();

        // Difesa
        private List<Eroe> eroi_difesa = new List<Eroe>();

        // Strutture
        // il Mercato è l'unica struttura che non viene assegnata a Giocatore
        private Fornace fornace;
        private Altare altare;
        private Torre torre;

        // Attacco
        private Attacco attacco;

        // per abilita Bombardiere Pazzo
        private Torre torreAvversaria;

        // TRUPPE/TRUPPE ELITE/EROI AVVERSARI per abilita
        private List<Truppa> enemy_truppe_Mischia;
        private List<Truppa> enemy_truppe_Distanza;
        private List<Truppa> enemy_truppe_Tank;

        public Giocatore(string nome)
        {
            this.nome = nome;
        }

        // alcune cose non possono essere passate immediatamente quando si crea un'istanza di Giocatore
        // in quanto due istanze diverse hanno bisogno di attributi reciproci
        public void SetUp(Attacco attacco,Fornace fornace, Altare altare, Torre torre1, Torre torre2, List<Truppa> enemy_truppe_Mischia, List<Truppa> enemy_truppe_Distanza, List<Truppa> enemy_truppe_Tank)
        {
            this.attacco = attacco;
            this.fornace = fornace;
            this.altare = altare;
            this.torre = torre1;

            this.torreAvversaria = torre2;

            this.enemy_truppe_Mischia = enemy_truppe_Mischia;
            this.enemy_truppe_Distanza= enemy_truppe_Distanza;
            this.enemy_truppe_Tank= enemy_truppe_Tank;

            Add_Truppe();
            Add_Truppe_Elite();
            Add_Eroi();
        }

        // GET // SET

        public String Nome { get { return nome; } }

        // Add Truppe Elite

        private void Add_Truppe_Elite()
        {
            Add_Truppe_Elite_Guerra();
            Add_Truppe_Elite_Supporto();
        }

        private void Add_Truppe_Elite_Guerra()
        {
            Add_Truppe_Elite_Guerra_Mischia();
            Add_Truppe_Elite_Guerra_Distanza();
            Add_Truppe_Elite_Guerra_Tank();
        }

        // Add Truppe

        private void Add_Truppe_Elite_Guerra_Mischia()
        {
            truppe_Elite_Guerra_Mischia.Add(new Vex(enemy_truppe_Mischia));
        }

        private void Add_Truppe_Elite_Guerra_Distanza()
        {
            truppe_Elite_Guerra_Distanza.Add(new Bombardiere_Pazzo(torreAvversaria));
        }

        private void Add_Truppe_Elite_Guerra_Tank()
        {
            truppe_Elite_Guerra_Tank.Add(new Colosso(attacco, this));
        }

        private void Add_Truppe_Elite_Supporto()
        {
            truppe_Elite_Supporto.Add(new Pan(altare));
        }

        private void Add_Truppe()
        {
            Add_Truppe_Mischia();
            Add_Truppe_Distanza();
            Add_Truppe_Tank();
        }

        private void Add_Truppe_Mischia()
        {
            truppe_Mischia.Add(new Soldato_Semplice());
            truppe_Mischia.Add(new Spadaccino());
        }

        private void Add_Truppe_Distanza()
        {
            truppe_Distanza.Add(new Arciere());
        }

        private void Add_Truppe_Tank()
        {
            truppe_Tank.Add(new Mammut());
        }

        // Add Eroi

        private void Add_Eroi()
        {
            Add_Eroi_Attacco();
            Add_Eroi_Difesa();
        }

        private void Add_Eroi_Attacco()
        {
            eroi_attacco.Add(new Bruciatore(attacco));
        }

        private void Add_Eroi_Difesa()
        {
            eroi_difesa.Add(new Angelo(attacco));
        }
        // GET // SET

        // Truppe normali
        public List<Truppa> Truppe_Mischia { get { return truppe_Mischia; } }
        public List<Truppa> Truppe_Distanza { get { return truppe_Distanza; } }
        public List<Truppa> Truppe_Tank { get { return truppe_Tank; } }

        // Truppe Elite

        // Guerra
        public List<Truppa_Elite_Guerra> Truppe_Elite_Guerra_Mischia { get { return truppe_Elite_Guerra_Mischia; } }
        public List<Truppa_Elite_Guerra> Truppe_Elite_Guerra_Distanza { get { return truppe_Elite_Guerra_Distanza; } }
        public List<Truppa_Elite_Guerra> Truppe_Elite_Guerra_Tank { get { return truppe_Elite_Guerra_Tank; } }

        // Supporto
        public List<Truppa_Elite> Truppe_Elite_Supporto { get { return truppe_Elite_Supporto; } }

        // Eroi

        // Attacco
        public List<Eroe> Eroi_Attacco { get { return eroi_attacco; } }

        // Difesa
        public List<Eroe> Eroi_Difesa { get { return eroi_difesa; } }


        // Struttue

        //  Fornace
        public Fornace Fornace { get { return fornace; } }

        // Altare
        public Altare Altare { get { return altare; } }

        // Torre

        public Torre Torre { get { return torre; } }

        public Torre TorreAvversaria { get { return torreAvversaria; } }


    }
}
