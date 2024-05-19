using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Figure_Composte_da_Rettangoli
{
    internal class Rectangle
    {
        private double length;
        private double height { get; set; }

        public Rectangle(double length, double height)
        {
            this.length = (length >= 0) ? length : 0;
            this.height = (height >= 0) ? height : 0;
        }

        // GET // SET

        public double Length
        {
            get { return length; }
            set { length = (value >= 0) ? value : 0; }
        }

        public double Height { 
            get { return height; }
            set { height = (value >= 0) ? value : 0; }
        }


        public double Perimeter()
        {
            return (length + height) * 2;
        }

        public double Area()
        {
            return length * height;
        }
    }


}

