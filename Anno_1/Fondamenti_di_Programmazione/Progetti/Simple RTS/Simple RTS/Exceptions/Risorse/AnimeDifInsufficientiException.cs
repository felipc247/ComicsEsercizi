using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Risorse
{
    internal class AnimeDifInsufficientiException : Exception
    {
        public AnimeDifInsufficientiException() {
            CC.RedFr("Anime DIF insufficienti!\n");
        }
    }
}
