using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfida_Algoritmica___Ordinamento
{
    internal class NonUnNumeroException : Exception
    {
        public NonUnNumeroException() {
            CC.RedFr("Inserisci un numero intero\n");
        }
    }
}
