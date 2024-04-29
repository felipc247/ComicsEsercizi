// See https://aka.ms/new-console-template for more information

// ex02
int larg = 0;
do
{
    Console.WriteLine("Inserire larghezza triangolo (numero intero)");
    try
    {
        larg = int.Parse(Console.ReadLine());
        break;
    }
    catch
    {
        Console.WriteLine("Input non idoneo");
    }

} while (true);

int alt = 0;
do
{
    Console.WriteLine("Inserire altezza triangolo (numero intero)");
    try
    {
        alt = int.Parse(Console.ReadLine());
        break;
    }
    catch
    {
        Console.WriteLine("Input non idoneo");
    }

} while (true);

for (int i = 0; i < alt; i++)
{
    for (int j = 0; j < larg; j++) Console.Write("*");
    Console.WriteLine();
}