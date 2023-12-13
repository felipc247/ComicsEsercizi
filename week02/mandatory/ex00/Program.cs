using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_Videogiochi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Archivio archivio = new Archivio("Videogames");
            
            Videogioco videogioco = new Videogioco("Anya's game", "Spy", 10);
            Videogioco videogioco1 = new Videogioco("Anya's game1", "Spy", 9);
            Videogioco videogioco2 = new Videogioco("Anya's game2", "Action", 3);
            Videogioco videogioco3 = new Videogioco("Anya's game3", "Action", 9);
            Videogioco videogioco4 = new Videogioco("Anya's game4", "Bocchi", 10);

            archivio.AggiungiVideogioco(videogioco);
            archivio.AggiungiVideogioco(videogioco1);
            archivio.AggiungiVideogioco(videogioco2);
            archivio.AggiungiVideogioco(videogioco3);
            archivio.AggiungiVideogioco(videogioco4);

            Console.WriteLine("Stampo tutti i dati del gioco");
            archivio.VisualizzaVideogiochi();

            Console.WriteLine("\nStampo giochi by genere 'Spy'");
            archivio.VisualizzaVideogiochi("Spy");

            Console.WriteLine("\nStampo giochi by punteggio '10'");
            archivio.VisualizzaVideogiochi(10);
        }
    }
}
