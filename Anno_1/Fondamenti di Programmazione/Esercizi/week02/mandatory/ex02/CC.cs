using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ricerca_Avanzata_Dictionary
{
    internal class CC
    {
        public CC() { }

        public static void printColor(String mes, ConsoleColor fg, ConsoleColor bg)
        {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.Write(mes);
            Console.ResetColor();
        }

        // FOREGROUND
        public static void MagentaFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(mes);
            Console.ResetColor();
        }

        public static void RedFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(mes);
            Console.ResetColor();
        }

        public static void GreenFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(mes);
            Console.ResetColor();
        }

        public static void YellowFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(mes);
            Console.ResetColor();
        }

        public static void BlueFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(mes);
            Console.ResetColor();
        }
        
        public static void DarkCyanFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(mes);
            Console.ResetColor();
        }

        public static void DarkYellowFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(mes);
            Console.ResetColor();
        }

        public static void CyanFr(String mes)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(mes);
            Console.ResetColor();
        }

        // BACKGROUND

        public static void WhiteFr(String mes)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(mes);
            Console.ResetColor();
        }
    }
}
