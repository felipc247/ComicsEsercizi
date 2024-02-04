using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notazione_Polacca_Inversa__RPN_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String[] input = { "5", "4", "3", "+", "-", "3", "*", "8", "9" , "-", "*"};

            Stack<string> stack = new Stack<string>();

            foreach (string s in input)
            {
                double value;
                if (double.TryParse(s, out value))
                {
                    stack.Push(s);
                }
                else
                {
                    double num2 = int.Parse(stack.Pop());
                    double num1 = int.Parse(stack.Pop());
                    string result = "";
                    switch (s)
                    {
                        case "+":
                            result += num1 + num2;
                            break;
                        case "-":
                            result += num1 - num2;
                            break;
                        case "*":
                            result += num1 * num2;
                            break;
                        case "/":
                            result += num1 / num2;
                            break;
                        default:
                            Console.WriteLine("Operatore invalido");
                            break;
                    }
                    if (result != "")
                        stack.Push(result);
                }
            }

            Console.WriteLine(int.Parse(stack.Pop()));
        }
    }
}
