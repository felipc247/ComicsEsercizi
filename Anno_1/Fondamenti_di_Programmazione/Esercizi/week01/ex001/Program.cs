// See https://aka.ms/new-console-template for more information

// ex04
void shiftDestraVetInt(ref int[] vet) {
    int[] sup = new int[vet.Length];
    for (int i = 0; i < vet.Length; i++)
    {
        sup[i] = (i != 0) ? vet[i - 1] : vet[vet.Length - 1];
    }
    vet = sup;
}

// inserimento lunghezza sequenza
Console.WriteLine("Inserire lunghezza sequenza");
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

// creazione e riempimento vettore interi
int[] intVet = new int[lung];

for (int i = 0; i < lung; i++)
{
    do
    {
        Console.WriteLine($"inserisci num [{i}]:");
        try
        {
            intVet[i] = int.Parse(Console.ReadLine());
            break;
        }
        catch
        {
            Console.WriteLine("Input non idoneo");
        }

    } while (true);
}

Console.WriteLine("Stampo inserimento");
for (int i = 0; i < intVet.Length; i++)
{
    Console.Write($"[{intVet[i]}] ");
}

shiftDestraVetInt(ref intVet);
Console.WriteLine("\nStampo vet shift dx");
for (int i = 0; i < intVet.Length; i++)
{
    Console.Write($"[{intVet[i]}] ");
}
