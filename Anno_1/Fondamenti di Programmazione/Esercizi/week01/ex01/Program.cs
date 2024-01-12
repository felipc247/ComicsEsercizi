// See https://aka.ms/new-console-template for more information

// ex01
Console.WriteLine("Inserire lunghezza triangolo (numero intero)");
int lung = 0;
do
{
    try
    {
        lung = int.Parse(Console.ReadLine());
        break;
    }
    catch
    {
        Console.WriteLine("Input non idoneo");
    }

} while (true);

for (int i = 0; i < lung; i++)
{
    for(int j = 0; j < i + 1; j++) Console.Write("*");
    Console.WriteLine();
}