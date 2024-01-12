using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivio_videogiochi
{
    internal class CC
    {
        public CC() { }

        public static void printColor(String mes, ConsoleColor fg, ConsoleColor bg) {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.WriteLine(mes);
            Console.ResetColor();
        }

        // FOREGROUND
        public static void MagentaFr(String mes) { 
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(mes);
            Console.ResetColor();
        }

        public static void RedFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mes);
            Console.ResetColor();
        }

        public static void GreenFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mes);
            Console.ResetColor();
        }

        public static void BlueFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(mes);
            Console.ResetColor();
        }

        public static void DarkYellowFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(mes);
            Console.ResetColor();
        }

        public static void CyanFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(mes);
            Console.ResetColor();
        }

        // BACKGROUND

        public static void WhiteFr(String mes)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(mes);
            Console.ResetColor();
        }
    }
}
