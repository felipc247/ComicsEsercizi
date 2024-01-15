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

            public double Calculate()
            {
                Stack<NodoEspressione> stack = new Stack<NodoEspressione>();
                return Calculate_Rec(Radice, stack);
            }

            private double Calculate_Rec(NodoEspressione nodo, Stack<NodoEspressione> stack)
            {
                if (nodo == null) return 0;

                // vado in fondo all'albero per trovare i primi 2 numeri da cui iniziare

                if (nodo.Sinistro != null)
                {
                    Calculate_Rec(nodo.Sinistro, stack);
                }

                if (nodo.Destro != null)
                {
                    Calculate_Rec(nodo.Destro, stack);
                }

                // Quando trovo un operatore Pop dei 2 valori all'interno dello stack
                // e pusho il risultato dell'operazione nello stack
                if (Operatore(nodo.Valore))
                {
                    double num1, num2;
                    double.TryParse(stack.Pop().Valore, out num1);
                    double.TryParse(stack.Pop().Valore, out num2);
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
                    // Se non è un operatore, si tratta di un numero, perciò pusho il nodo
                    stack.Push(nodo);
                }

                return double.Parse(stack.Peek().Valore);


            }

        }

        static void Main(string[] args)
        {
            String[] op = { "5", "3", "+", "4", "*", "4", "/"};
            AlberoEspressione alberoEspressione = new AlberoEspressione();
            alberoEspressione.CostruisciAlbero(op);
            Console.WriteLine(alberoEspressione.Calculate());
            int i = 0;
        }
    }
}
