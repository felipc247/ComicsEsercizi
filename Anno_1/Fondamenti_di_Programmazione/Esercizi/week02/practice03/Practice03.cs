using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace pratica03
{
    internal class Program
    {
        static List<String> estraiOperatori(String operazione)
        {
            List<String> operatori = new List<String>();
            for (int i = 0; i < operazione.Length; i++)
            {
                switch (operazione[i])
                {
                    case '*':
                        operatori.Add(operazione[i]+"");
                        break;
                    case '/':
                        operatori.Add(operazione[i] + "");
                        break;
                    case '+':
                        operatori.Add(operazione[i] + "");
                        break;
                    case '-':
                        operatori.Add(operazione[i] + "");
                        break;
                }

            }
            return operatori;
        }

        static List<double> estraiOperandi(String operazione)
        {
            List<double> operandi = new List<double>();
            double numD = 0;
            String num = "";
            for (int i = 0; i < operazione.Length; i++)
            {
                switch (operazione[i])
                {
                    case '1':
                        num += "1";
                        break;
                    case '2':
                        num += "2";
                        break;
                    case '3':
                        num += "3";
                        break;
                    case '4':
                        num += "4";
                        break;
                    case '5':
                        num += "5";
                        break;
                    case '6':
                        num += "6";
                        break;
                    case '7':
                        num += "7";
                        break;
                    case '8':
                        num += "8";
                        break;
                    case '9':
                        num += "9";
                        break;
                    case '0':
                        num += "0";
                        break;
                    case ',':
                        num += ",";
                        break;
                    default:
                        //char checkNum = num[0];
                        //if ((int)checkNum > 47 && (int)checkNum < 58)
                        //{
                        //    // inizia con un numero, perciò top
                        try
                        {
                            numD = double.Parse(num);
                            operandi.Add(numD);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                            throw new NonUnNumeroException("Hai inserito dei caratteri non idonei");
                        } catch (NonUnNumeroException ex) {
                            Console.WriteLine(ex.Message);
                        }
                        //}
                        num = "";
                        break;
                }
                if (i == operazione.Length - 1)
                {
                    try
                    {
                        numD = double.Parse(num);
                        operandi.Add(numD);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        throw new NonUnNumeroException("Hai inserito dei caratteri non idonei");
                    }
                    catch (NonUnNumeroException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
            return operandi;
        }

        static void Main(string[] args)
        {
            // ex 00
            Console.WriteLine("Inserisci operazione");
            String operazione = Console.ReadLine();
            List<double> operandi = estraiOperandi(operazione);
            List<String> operatori = estraiOperatori(operazione);
            
            // stampa operandi e operatori
            foreach (double num in operandi)
            {
                Console.WriteLine(num);
            }

            foreach (String operatore in operatori)
            {
                Console.WriteLine(operatore);
            }

            try
            {
                if (operandi.Count < 2)
                {
                    throw new OperandiInsufficientiException();
                }
            }
            catch (OperandiInsufficientiException)
            {
                Console.WriteLine("Devi inserire almeno 2 operatori");
            }
            double result = 0;
            for (int i = 0;i < operatori.Count;i++) {
                
            }
        }
    }
}
