using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_Videogiochi
{
    internal class Archivio
    {
        private String nome;
        private List<Videogioco> videogiocos = new List<Videogioco>();

        public Archivio(String nome) { 
            this.nome = nome;
        }

        public String Nome { 
            get { return nome; }
            set { nome = value; }
        }

        public void spezzaTesto() {
            Console.WriteLine("---------------------------");
        }

        public void AggiungiVideogioco(Videogioco videogioco) {
            videogiocos.Add(videogioco);
        }

        public void VisualizzaVideogiochi() {
            spezzaTesto();
            foreach (var videogioco in videogiocos)
            {
                videogioco.VisualizzaInfo();
            }
            spezzaTesto();
        }

        public void VisualizzaVideogiochi(String genere) {
            List<Videogioco> videogiocosGenere = videogiocos.FindAll(v => v.Genere == genere);
            spezzaTesto();
            foreach (var videogioco in videogiocosGenere)
            {
                videogioco.VisualizzaInfo();
            }
            spezzaTesto();
        }

        public void VisualizzaVideogiochi(int punteggio)
        {
            List<Videogioco> videogiocosPunteggio = videogiocos.FindAll(v => v.Punteggio == punteggio);
            spezzaTesto();
            foreach (var videogioco in videogiocosPunteggio)
            {
                videogioco.VisualizzaInfo();
            }
            spezzaTesto();
        }

    }
}
