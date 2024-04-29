using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfida_Algoritmica___Ordinamento
{
    internal class Giocatore
    {
        private char[] nome = new char[3];

        public Giocatore(char[] nome)
        {
            this.nome = nome;
        }

        public char[] Nome{ 
            get { return nome; }
            set {  nome = value; }
        }
    }
}
