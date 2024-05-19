using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Figure_Composte_da_Rettangoli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shape shape = new Shape();

            shape.Add(20, 30);
            shape.Add(20, 30);
            shape.Add(20, 30);
            shape.Add(20, 30);
            shape.Add(20, 30);

            shape.Print_All();

            Console.WriteLine(shape.Get_Total_Perimeter());

            Console.WriteLine(shape.Get_Total_Area());

            shape.Remove(3);
            
            Console.WriteLine(shape.Get_Total_Area());

            shape.Modify(3, 50, 50);

            shape.Print_All();

            Console.WriteLine(shape.GetCount());
        }
    }
}
