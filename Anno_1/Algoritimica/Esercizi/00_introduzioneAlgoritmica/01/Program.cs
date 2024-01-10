using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritmica_00_1
{
    internal class Program
    {
        static int MCD(int d1, int d2)
        {
            int min, max;
            if (d1 > d2)
            {
                min = d2;
                max = d1;
            }
            else
            {
                min = d1;
                max = d2;
            }

            // se max è divisibile per min, min è l'MCD, non devo fare altri controlli
            if (max % min == 0) return min;

            List<int> comuni = new List<int>();
            List<int> min_scomp = Scomposizione_in_Primi(min);
            List<int> max_scomp = Scomposizione_in_Primi(max);

            // Ricerco i fattori comuni
            foreach (int i in min_scomp)
            {
                if (comuni.Contains(i)) continue;
                List<int> times_found_d2 = max_scomp.FindAll(d => d.Equals(i));
                List<int> times_found_d1 = min_scomp.FindAll(d => d.Equals(i));
                if (times_found_d2.Count > 0)
                {
                    int times_found = (times_found_d1.Count > times_found_d2.Count) ? times_found_d2.Count : times_found_d1.Count;
                    for (int j = 0; j < times_found; j++)
                    {
                        comuni.Add(i);
                    }
                }
            }

            // Calcolo MCD
            int MCD = 1;

            for (int i = 0; i < comuni.Count; i++)
            {
                MCD *= comuni[i];
            }

            return (min < 0 || max < 0) ? -MCD : MCD;

        }

        static List<int> Scomposizione_in_Primi(int num)
        {
            List<int> primi = new List<int>();
            int divisore = 2;
            int numOriginale = num;
            // partendo da 2, controllo se i numeri in successione
            // sono divisori di num
            do
            {
                // Se num è divisibile, controllo che divisore sia primo
                if (num % divisore == 0)
                {
                    bool primo = true;
                    double divisoreMaxCheck = Math.Sqrt(divisore);
                    int contr = 3;
                    do
                    {
                        if (divisore < 2) break;
                        if (divisore % contr == 0)
                        {
                            if (divisore != contr) primo = false;
                            break;
                        }
                        contr++;
                    } while (contr < divisoreMaxCheck);
                    // se primo aggiungo
                    if (primo)
                    {
                        primi.Add(divisore);
                        //Console.WriteLine(divisore);
                    }
                    // divido il num per il divisore
                    num /= divisore;
                }
                else
                {
                    // se non divisibile allora passo al prossimo
                    // altrimenti ripeto con lo stesso divisore
                    divisore = (divisore == 2) ? divisore + 1 : divisore + 2;
                }
                // controllo fino a metà perché quest'ultima potrebbe essere un fattore primo in alcuni casi
            } while (divisore <= numOriginale / 2);
            //Console.WriteLine("________");
            return primi;
        }

        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine(MCD(784753260, 226346744));
            DateTime end = DateTime.Now;
            // stampo tempo di esecuzione
            Console.WriteLine(end - start);
        }
    }
}
