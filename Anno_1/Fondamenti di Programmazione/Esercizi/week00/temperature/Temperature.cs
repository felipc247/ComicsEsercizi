// See https://aka.ms/new-console-template for more information
using System.Collections;

String risposta = "";
do
{
    ArrayList temperature = new ArrayList();
    for (int i = 0; i < 7; i++)
    {
        Console.WriteLine("Inserire temperatura giorno " + (i + 1));
        temperature.Add(double.Parse(Console.ReadLine()));
    }

    double mediaTemp = 0;
    double maxTemp = 0;
    double minTemp = (double)temperature[0];
    foreach (double temp in temperature)
    {
        mediaTemp += temp;
        if (temp > maxTemp) maxTemp = temp;
        if (temp < minTemp) minTemp = temp;
    }
    mediaTemp /= 7;

    Console.WriteLine("Temperatura media settimana = " + mediaTemp);
    Console.WriteLine("Temperatura massima settimana = " + maxTemp);
    Console.WriteLine("Temperatura minima settimana = " + minTemp);
    Console.WriteLine("Reinserire i dati? (S/N)");
    risposta = Console.ReadLine();
} while (risposta == "S" || risposta == "s");
Console.WriteLine("Termine programma");

