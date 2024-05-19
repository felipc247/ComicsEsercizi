using BitcoinExchange.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace BitcoinExchange
{
    internal class Program
    {

        // voi implementate la lettura dai file e il parsing dal file con i test

        static Dictionary<string, double> insertFileinMap(string path)
        {
            Dictionary<string, double> map = new Dictionary<string, double>();

            if (File.Exists(path))
            {
                // Apri il file in modalità di lettura
                using (StreamReader sr = new StreamReader(path))
                {
                    // Estraggo i titoli delle colonne e li stampo
                    string[] columns = sr.ReadLine().Split(';');
                    for (int i = 0; i < columns.Length; i++)
                    {
                        Console.Write($"{columns[i]}");
                        if (i < columns.Length - 1) Console.Write(" | ");
                        else Console.WriteLine();
                    }

                    // Leggi il contenuto del file riga per riga
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Controlli sulla validità dei dati
                        string[] data;
                        try
                        {
                            data = line.Split(';');
                            if (data.Length != columns.Length) throw new FormatoNonCorrettoException();
                            string date = data[0];
                            if (!DateTime.TryParse(date, out _)) throw new NotADateException();
                            double value;
                            if (!double.TryParse(data[1], out value)) throw new NotANumberException();
                            if (value < 0 || value > 1000) throw new ValueOutBoundsException();

                            // fine controlli
                            // possiamo inserire il valore nella mappa
                            map[date] = value;

                        }
                        catch (FormatoNonCorrettoException)
                        {
                            Console.WriteLine("Errore, formato non corretto");
                        }
                        catch (NotADateException)
                        {
                            Console.WriteLine("Errore, non una data");
                        }
                        catch (NotANumberException)
                        {
                            Console.WriteLine("Errore, non un numero");
                        }
                        catch (ValueOutBoundsException)
                        {
                            Console.WriteLine("Errore, numero non in range");
                        }

                    }
                }
            }
            else
            {
                Console.WriteLine("File is not there");
            }

            foreach (var item in map)
            {
                Console.WriteLine($"{item.Key} | {item.Value}");
            }


            return map;
        }

        static void PrintExchangeValue(string path, Dictionary<string, double> map)
        {
            if (File.Exists(path))
            {
                // Apri il file in modalità di lettura
                using (StreamReader sr = new StreamReader(path))
                {
                    // Estraggo i titoli delle colonne e li stampo
                    string[] columns = sr.ReadLine().Split('|');
                    for (int i = 0; i < columns.Length; i++)
                    {
                        Console.Write($"{columns[i]}");
                        if (i < columns.Length - 1) Console.Write(" | ");
                        else Console.WriteLine();
                    }

                    // Leggi il contenuto del file riga per riga
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Controlli sulla validità dei dati
                        string[] data;
                        try
                        {
                            data = line.Split('|');
                            if (data.Length != columns.Length) throw new FormatoNonCorrettoException();
                            string date = data[0];
                            if (!DateTime.TryParse(date, out _)) throw new NotADateException();
                            float value;
                            if (!float.TryParse(data[1], out value)) throw new NotANumberException();
                            if (value < 0 || value > 1000) throw new ValueOutBoundsException();

                            // fine controlli
                            // possiamo stampare l'exchange rate
                            Console.WriteLine($"{date} | {value} => {getExchangeValue(map, date, value)}");

                        }
                        catch (FormatoNonCorrettoException)
                        {
                            Console.WriteLine("Errore, formato non corretto => " + line);
                        }
                        catch (NotADateException)
                        {
                            Console.WriteLine("Errore, non una data => " + line);
                        }
                        catch (NotANumberException)
                        {
                            Console.WriteLine("Errore, non un numero => " + line);
                        }
                        catch (ValueOutBoundsException)
                        {
                            Console.WriteLine("Errore, numero non in range => " + line);
                        }

                    }
                }
            }
            else
            {
                Console.WriteLine("File is not there");
            }
        }

        static double getExchangeValue(Dictionary<string, double> map, string date, double amount)
        {
            if (map.ContainsKey(date)) return map[date] * amount;

            string closest_date = "";
            string previous_date = "";

            int i = 0;

            foreach (var item in map)
            {
                if (i == 0)
                {
                    if (DateTime.Parse(date) < DateTime.Parse(item.Key))
                    {
                        closest_date = item.Key;
                        break;
                    }

                    previous_date = item.Key;
                    i++;
                }
                else
                {

                    if (DateTime.Parse(date) < DateTime.Parse(item.Key))
                    {
                        if (map[previous_date] < map[item.Key])
                            closest_date = previous_date;
                        else
                            closest_date = item.Key;

                        break;
                    }
                    previous_date = item.Key;

                }

            }

            if (closest_date.Equals("")) closest_date = previous_date;

            return map[closest_date] * amount;

        }

        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Dictionary<string, double> map = insertFileinMap("dates.csv");
            PrintExchangeValue("values.csv", map);
        }
    }
}
