using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Animali
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cane cane = new Cane("Dora");
            Gatto gatto = new Gatto("Mahiro");
            Animale a = new Animale("sscx");

            cane.FaiVerso();
            gatto.FaiVerso();
        }
    }
}
