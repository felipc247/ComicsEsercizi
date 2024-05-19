using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Agenzia_Immobiliare
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Box box = new Box("123", "loc. wec 23", 52100,"AR", 30);
            Appartamento appartamento = new Appartamento("123", "loc. wec 23", 52100,"AR", 30, 3, 2);
            Villa villa = new Villa("532", "cwf 4", 12452, "FI", 3000, 300);

            AgenziaImmobiliare a_i = new AgenziaImmobiliare();

            a_i.Add_Immobile(box);
            a_i.Add_Immobile(appartamento);
            a_i.Add_Immobile(villa);

            List<Immobile> found = a_i.SearchImmobile("123");

            foreach (var im in found)
            {
                Console.WriteLine(im.ToString());
            }        
        
        }
    }
}
