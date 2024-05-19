using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Animali
{
    internal class Animale
    {
        private String nome;

        public Animale(String nome) {
            this.nome = nome;
        }

        public String Nome { 
            get { return this.nome; }
            set { this.nome = value; }
        }

        public virtual void FaiVerso() {
            Console.WriteLine("Suono generico");
        }
    }
}
