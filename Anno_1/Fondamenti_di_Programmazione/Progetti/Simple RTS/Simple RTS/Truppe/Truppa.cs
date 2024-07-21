using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Truppe
{
    internal class Truppa
    {
        protected String nome;
        protected int costo;
        protected int vita;
        protected List<int> vite = new List<int>();
        protected int atk;
        protected int livello = 1;
        protected int range;
        protected int quantita = 0;
        protected int costo_Potenziamento;
        protected int[] costi_Potenziamento;
        protected int conversione_blu;
        protected TT tipologia;
        protected int disponibili = 0;
        protected int schierate = 0;
        protected int ferite = 0;

        public enum TT
        {
            Mischia,
            Distanza,
            Tank
        }

        public Truppa() { }

        public Truppa(Truppa truppa)
        {
            this.nome = truppa.nome;
            this.costo = truppa.costo;
            this.vita = truppa.vita;
            this.vite = truppa.vite;
            this.atk = truppa.atk;
            this.livello = truppa.livello;
            this.range = truppa.range;
            this.quantita = truppa.quantita;
            this.costo_Potenziamento = truppa.costo_Potenziamento;
            this.costi_Potenziamento = truppa.costi_Potenziamento;
            this.conversione_blu = truppa.conversione_blu;
            this.tipologia = truppa.tipologia;
            this.disponibili = truppa.Disponibili;
            this.schierate = truppa.schierate;
            this.ferite = truppa.ferite;
        }

        // GET // SET
        public String Nome { get { return nome; } }

        public int Costo { get { return costo; } }

        public int Vita { get { return vita; } }

        public List<int> Vite { get { return vite; } }

        public int Atk { get { return atk; } }

        public int Livello { get { return livello; } }

        public int Range { get { return range; } }

        public int Quantita { get { return disponibili + schierate + ferite; } }

        public int Disponibili { get { return disponibili; } set { disponibili += value; if (disponibili < 0) disponibili = 0; } }

        public int Schierate { get { return schierate; } set { schierate += value; if (schierate < 0) schierate = 0; } }

        public int Ferite { get { return ferite; } set { ferite += value; if (ferite < 0) ferite = 0; } }

        public int Costo_Potenziamento { get { return costo_Potenziamento; } }

        public int[] Costi_Potenziamento { get { return costi_Potenziamento; } }

        public int Conversione_Blu { get { return conversione_blu; } }

        public TT Tipologia { get { return tipologia; } }


        // METODI

        public virtual void Potenzia() { }


    }
}
