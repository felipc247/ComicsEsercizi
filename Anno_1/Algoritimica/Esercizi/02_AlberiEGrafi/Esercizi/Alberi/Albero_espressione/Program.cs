using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albero_espressione
{
    internal class Program
    {
        public class NodoEspressione
        {
            public string Valore;
            public NodoEspressione Sinistro;
            public NodoEspressione Destro;

            public NodoEspressione(string valore)
            {
                Valore = valore;
                Sinistro = null;
                Destro = null;
            }
        }

        public class AlberoEspressione
        {
            public NodoEspressione Radice;

            public AlberoEspressione() { Radice = null; }

            public void CostruisciAlbero(string[] espressione)
            {
                Stack<NodoEspressione> stack = new Stack<NodoEspressione>();

                /*
                         /
                        / \
                       -   2
                      / \
                     +   4
                    / \
                   5   3 

                 */

                foreach (string token in espressione)
                {
                    NodoEspressione nodo = new NodoEspressione(token);

                    if (Operatore(token))
                    {
                        nodo.Destro = stack.Pop();
                        nodo.Sinistro = stack.Pop();
                    }

                    stack.Push(nodo);
                }

                Radice = stack.Pop();
            }

            private bool Operatore(string token)
            {
                return token == "+" || token == "-" || token == "*" || token == "/";
            }

            private bool Num(string token)
            {
                int i = 0;
                return int.TryParse(token, out i);
            }

            private int Calculate(NodoEspressione nodo, Stack<NodoEspressione> stack)
            {
                if (nodo == null) return 0;

                if (nodo.Sinistro != null)
                {
                    Calculate(nodo.Sinistro, stack);
                }

                if (nodo.Destro != null)
                {
                    Calculate(nodo.Destro, stack);
                }

                if (Operatore(nodo.Valore))
                {
                    int num1, num2;
                    int.TryParse(stack.Pop().Valore, out num1);
                    int.TryParse(stack.Pop().Valore, out num2);
                    switch (nodo.Valore)
                    {
                        case "/":
                            stack.Push(new NodoEspressione(num2 / num1 + ""));
                            break;
                        case "*":
                            stack.Push(new NodoEspressione(num2 * num1 + ""));
                            break;
                        case "-":
                            stack.Push(new NodoEspressione(num2 - num1 + ""));
                            break;
                        case "+":
                            stack.Push(new NodoEspressione(num2 + num1 + ""));
                            break;
                    }
                }
                else
                {
                    stack.Push(nodo);
                }


            }

        }

        static void Main(string[] args)
        {
            String[] op = { "5", "3", "+", "4", "*", "2", "/", "6", "/" };
            AlberoEspressione alberoEspressione = new AlberoEspressione();
            alberoEspressione.CostruisciAlbero(op);
            int i = 0;
        }
    }
}
