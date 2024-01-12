using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Biblioteca
{
    internal class Libro
    {
        private String titolo;
        private String autore;
        private int anno_pubblicazione;

        public Libro(string titolo, string autore, int anno_pubblicazione)
        {
            this.titolo = titolo;
            this.autore = autore;
            this.anno_pubblicazione = anno_pubblicazione;
        }

        public String Titolo
        {
            get { return titolo; }
            set { titolo = value; }
        }

        public String Autore
        {
            get { return autore; }
            set { autore = value; }
        }

        public int Anno_pubblicazione
        {
            get { return anno_pubblicazione; }
            set { anno_pubblicazione = value; }
        }

        public void VisualizzaInfo()
        {
            Console.WriteLine($"Titolo: {titolo}\n" +
                $"Autore: {autore}\n" +
                $"Anno pubblicazione: {anno_pubblicazione}");
        }
    }
}
