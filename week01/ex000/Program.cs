// See https://aka.ms/new-console-template for more information

// ex03
bool findIncreasingSubsequance(int[] vet)
{
    bool crescente = true;
    // non controllo l'ultimo numero, pk non avrei nulla con cui confrontarlo
    for (int i = 0; i < vet.Length - 1; i++)
    {
        if (!(vet[i] < vet[i + 1])) { 
            crescente = false; break;
        }
    }
    return crescente;
};


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

// controllo sequenza crescente e output
Console.WriteLine((findIncreasingSubsequance(intVet)) ? "Sequenza strettamente crescente" : "Sequenza non crescente");