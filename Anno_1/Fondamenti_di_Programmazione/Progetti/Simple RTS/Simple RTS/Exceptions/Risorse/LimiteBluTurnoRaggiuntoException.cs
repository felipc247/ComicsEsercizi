using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Risorse
{
    internal class LimiteBluTurnoRaggiuntoException : Exception
    {
        public LimiteBluTurnoRaggiuntoException() {
            CC.GreenFr("Limite Sacrificio Blu Raggiunto!\n");
        }
    }
}
