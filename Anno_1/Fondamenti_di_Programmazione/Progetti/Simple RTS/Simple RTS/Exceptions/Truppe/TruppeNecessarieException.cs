﻿using Simple_RTS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_RTS.Exceptions.Truppe
{
    internal class TruppeNecessarieException : Exception
    {
        public TruppeNecessarieException() {
            CC.RedFr("Devi schierare almeno 1 Truppa!\n");
        }
    }
}
