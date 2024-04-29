using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manipolazioneListe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            bool exit = false;
            do
            {
                Console.WriteLine($"1. aggiungi int\n" +
                    $"2. rimuovi int\n" +
                    $"3. stampa lista int\n" +
                    $"4. esci");
                int scelta = 4;
                do
                {
                    try
                    {
                        String sceltaString = Console.ReadLine();
                        scelta = int.Parse(sceltaString);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Input non idoneo");
                    }

                } while (true);

                switch (scelta)
                {
                    case 1:
                        int num = 0;
                        do
                        {
                            try
                            {
                                Console.WriteLine("Inserisci numero");
                                String numString = Console.ReadLine();
                                num = int.Parse(numString);
                                list.Add(num);
                                break;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Input non idoneo");
                            }

                        } while (true);
                        break;
                    case 2:
                        Console.WriteLine("Inserisci la posizione del numero da rimuovere");
                        int posNum = 0;
                        do
                        {
                            try
                            {
                                String posNumString = Console.ReadLine();
                                posNum = int.Parse(posNumString);
                                list.RemoveAt(posNum);
                                break;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Input non idoneo");
                            }
                        } while (true);
                        break;
                    case 3:
                        int index = 0;
                        foreach (int i in list) {
                            Console.WriteLine($"[{index++}] {i}");
                        }
                        break;
                    default:
                        Console.WriteLine("uscita");
                        exit = true;
                        break;
                }

            } while (!exit);
        }
    }
}
