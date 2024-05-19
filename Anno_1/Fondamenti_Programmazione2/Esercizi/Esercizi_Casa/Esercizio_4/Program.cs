using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Playlist playlist = new Playlist(); 

            Brano brano = new Brano("Peanuts", "Anya");
            Brano brano1 = new Brano("Criceto Mix", "Komaru");
            Brano brano2 = new Brano("Hard solo", "Bocchi");

            playlist.Add_Playlist(brano);
            playlist.Add_Playlist(brano1);
            playlist.Add_Playlist(brano2);

            List<string> descrizioni = playlist.Get_Descrizioni();

            foreach (var descrizione in descrizioni)
            {
                Console.WriteLine(descrizione);
            }
        }
    }
}
