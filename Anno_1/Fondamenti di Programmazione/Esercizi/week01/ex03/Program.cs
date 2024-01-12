using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conteggio_Occorrenze_Numeri_Random
{
    internal class Program
    {

        static void GeneraNumeriCasuali(ref int[] vet)
        {
            Random random = new Random();

            for (int i = 0; i < vet.Length; i++)
            {
                // genero int random
                vet[i] = random.Next(0, 10);
                Console.WriteLine(vet[i]);
            }
        }

        static void AggiornaConteggioOccorrenze(int[] vet, ref int[] countVet) {
            for (int i = 0; i < vet.Length; i++)
            {
                // aggiungo occorrenza
                countVet[vet[i]]++;
            }
        }

        static void StampaConteggio(int[] countVet) {
            for (global::System.Int32 i = 0; i < countVet.Length; i++)
            {
                Console.WriteLine($"[{i}] occorrenze = {countVet[i]}");
            }
        }
        static void Main(string[] args)
        {
            // ex03

            int length = 0;
            do
            {
                Console.WriteLine("Inserire lunghezza vettore");
                try
                {
                    length = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Input non idoneo");
                }
            } while (true);

            Console.WriteLine("-------------------------");
            int[] vet = new int[length];
            // vet per contare le occorrenze
            int[] countVet = new int[10];
            // inizializzo countVet
            for (int i = 0; i < countVet.Length; i++)
            {
                countVet[i] = 0;
            }

            do
            {
                // riempimento int vet random
                GeneraNumeriCasuali(ref vet);
                // aggiorno occorrenze
                AggiornaConteggioOccorrenze(vet, ref countVet);
                // stampa conteggio
                StampaConteggio(countVet);
                
                // sezione continua
                int continua = -1;
                do
                {
                    Console.WriteLine("Continuare?[0/1]");
                    try
                    {
                        continua = int.Parse(Console.ReadLine());
                        // se non si inserisce un int che sia 0 o 1, forzo -1 per far ripetere l'input
                        continua = (continua == 0 || continua == 1) ? continua : -1;
                    }
                    catch
                    {
                        Console.WriteLine("Input non idoneo");
                    }
                } while (continua == -1);

                if (continua == 0) break;

            } while (true);

            Console.WriteLine("fin");


        }
    }
}
