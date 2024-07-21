using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Eroi
{
    internal class EroeImprigionatoException : Exception
    {
        public EroeImprigionatoException() {
            CC.RedFr("Il tuo Eroe è Imprigionato!\n");
        }
    }
}
