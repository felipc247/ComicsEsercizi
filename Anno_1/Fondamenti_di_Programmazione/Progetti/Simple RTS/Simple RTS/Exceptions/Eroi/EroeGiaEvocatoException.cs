using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Eroi
{
    internal class EroeGiaEvocatoException : Exception
    {
        public EroeGiaEvocatoException() {
            CC.GreenFr("Eroe già Evocato!\n");
        }
    }
}
