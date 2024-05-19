using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pianoforte pianoforte = new Pianoforte();   
            Chitarra chitarra = new Chitarra(); 
            Tromba tromba = new Tromba();

            Musicista musicista = new Musicista();

            musicista.Esegui_Suonata(pianoforte);
            musicista.Esegui_Suonata(chitarra);
            musicista.Esegui_Suonata(tromba);
        }
    }
}
