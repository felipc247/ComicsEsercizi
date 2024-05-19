using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Figure_Composte_da_Rettangoli
{
    internal class Shape
    {
        private List<Rectangle> rectangles;

        public Shape()
        {
            rectangles = new List<Rectangle>();
        }

        public int GetCount()
        {
            return rectangles.Count;
        }

        // GET // SET

        public int Shape_count {
            get { return rectangles.Count; }
        }

        // METHODS
        public void Print_All()
        {
            for (int i = 0; i < rectangles.Count; i++)
            {
                Console.WriteLine($"rectangle: {i} | length: {rectangles[i].Length}, height: {rectangles[i].Height}");
            }
        }

        public void Print(int index)
        {
            Console.WriteLine($"rectangle: {index} | length: {rectangles[index].Length}, height: {rectangles[index].Height}");
        }

        public void Modify(int index, int length, int height) {
            rectangles[index].Length = length;
            rectangles[index].Height = height;
            Console.WriteLine($"rectangle {index} modificato!");
        }

        public void Remove(int index) { 
            rectangles.RemoveAt(index);
        }

        public void Add(int length, int height) { 
            rectangles.Add(new Rectangle(length, height));
        }

        public double Get_Total_Area() { 
            double area = 0;
            foreach (var rectangle in rectangles)
            {
                area += rectangle.Area();
            }

            return area;
        }

        public double Get_Total_Perimeter()
        {
            double perimeter = 0;
            foreach (var rectangle in rectangles)
            {
                perimeter += rectangle.Perimeter();
            }

            return perimeter;
        }

    }
}
