using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Frammenti
{
    internal class FrammentiInsufficientiException : Exception
    {
        public FrammentiInsufficientiException()
        {
            CC.RedFr("Frammenti insufficienti!\n");
        }
    }
}
