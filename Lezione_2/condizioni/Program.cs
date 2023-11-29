// See https://aka.ms/new-console-template for more information

// ex00

using System.Numerics;

String continua = "";
do
{
    int voto = -1;
    do
    {
        Console.WriteLine("Inserire un voto da 0 a 100");

        try
        {
            voto = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Input non idoneo");
        }

    } while (!(voto >= 0 && voto <= 100));

    if (voto < 50)
    {
        Console.WriteLine("Insufficiente");
    }
    else if (voto < 70)
    {
        Console.WriteLine("Sufficiente");
    }
    else if (voto < 90)
    {
        Console.WriteLine("Buono");
    }
    else
    {
        Console.WriteLine("Eccelente");
    }
    Console.WriteLine("Ripetere? [0/1]");
    continua = Console.ReadLine();
} while (continua == "0");
Console.WriteLine("Uscita programma");

// ex01

int num = 7;

for (int i = 1; i <= 10; i++)
    Console.WriteLine("[7*" + i + "] = " + (num * i));

// ex02

int somma = 0;
for (int i = 0; i < 10; i++)
    somma += 2 * (i + 1);
Console.WriteLine("La somma dei primi 10 numeri pari è " + somma);

// ex03
num = -1;
Console.WriteLine("Inserire un numero intero positivo");
do
{
    try
    {
        num = int.Parse(Console.ReadLine());
    }
    catch
    {
        Console.WriteLine("Numero non idoneo");
    }
} while (num < 1);

int ii = 0;
BigInteger fattoriale = 1;
while (ii != num)
{
    fattoriale *= (ii + 1);
    ii++;
}
Console.WriteLine("Il fattoriale di " + num + " è " + fattoriale);

// ex04
num = -1;
Console.WriteLine("Inserire un numero intero positivo");
do
{
    try
    {
        num = int.Parse(Console.ReadLine());
    }
    catch
    {
        Console.WriteLine("Numero non idoneo");
    }
} while (num < 1);

int iii = 3;
bool primo = true;
// controllo primo

// Non controllo i numeri pari
if (num % 2 != 0)
{
    do
    {
        if (num % iii == 0)
        {
            Console.WriteLine("III = " + iii);
            primo = (num == iii) ? true : false;
            if (primo) break;
        }
        iii++;
    } while (true);
}

if (primo)
{
    Console.WriteLine("Il numero è primo");
}
else
{
    Console.WriteLine("Il numero NON è primo");
}