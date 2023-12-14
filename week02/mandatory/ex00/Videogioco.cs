using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_Videogiochi
{
    internal class Videogioco
    {
        private String nome;
        private String genere;
        private int punteggio; // da 1 a 10

        public Videogioco(string nome, string genere, int punteggio)
        {
            this.nome = nome;
            this.genere = genere;
            this.punteggio = punteggio;
        }

        // GET // SET (con errori se input non idoneo)
        public String Nome
        {
            get { return nome; }
            set
            {
                if (value == null)
                {
                    throw new StringaVuotaException();
                }
                else
                {
                    nome = value;
                }
            }
        }

        public String Genere
        {
            get { return genere; }
            set
            {
                if (value.Equals(""))
                {
                    throw new StringaVuotaException();
                }
                else
                {
                    genere = value;
                }
            }
        }

        public int Punteggio
        {
            get { return punteggio; }
            set
            {
                if (punteggio >= 1 && punteggio <= 10)
                {
                    punteggio = value;
                }
                else
                {
                    throw new PunteggioNonValidoException();
                }
            }
        }

        public void VisualizzaInfo() {
            Console.WriteLine($"Nome: {nome}\n" +
                $"Genere: {genere}\n" +
                $"Punteggio: {punteggio}");
        }
    }
}
