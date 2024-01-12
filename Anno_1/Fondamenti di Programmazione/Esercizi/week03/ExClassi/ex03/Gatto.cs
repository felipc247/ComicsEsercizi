using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Animali
{
    internal class Gatto : Animale
    {
        public Gatto(string nome) : base(nome)
        {
            this.Nome = nome;
        }

        public override void FaiVerso()
        {
            Console.WriteLine("MIAOO");
        }
    }
}
