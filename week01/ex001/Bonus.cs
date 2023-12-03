// See https://aka.ms/new-console-template for more information

// ex04 bonus
void shiftDestraVetInt(ref int[] vet, int pos)
{
    int[] sup = new int[vet.Length];
    for (int i = 0; i < vet.Length; i++)
    {
        // se posso eseguire lo shift senza andare sotto 0 con l'indice allora vet[i-pos]
        // altrimenti prendo un valore partendo dalla fine del vettore
        // uso Abs pk sennò vet.length - diventa + (- -) quando i
        //sup[i] = (i > pos - 1) ? vet[i - pos] : vet[vet.Length - Math.Abs(i - pos)];
        // pos - 1 è sempre positivo, il caso pos = i e quindi 0 è incluso nel ramo true
        sup[i] = (i > pos - 1) ? vet[i - pos] : vet[vet.Length - (pos - i)];
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

// stampa vettore inserito
Console.WriteLine("Stampo inserimento");
for (int i = 0; i < intVet.Length; i++)
{
    Console.Write($"[{intVet[i]}] ");
}
Console.WriteLine();

// inserimento shift

int shiftDX = 0;
do
{
    Console.WriteLine("Inserire int shift");
    try
    {
        shiftDX = int.Parse(Console.ReadLine());
        break;
    }
    catch
    {
        Console.WriteLine("Input non idoneo");
    }

} while (true);

// effettuo shift dx di pos posizioni
shiftDestraVetInt(ref intVet, shiftDX);
Console.WriteLine("Stampo vet shift dx");
for (int i = 0; i < intVet.Length; i++)
{
    Console.Write($"[{intVet[i]}] ");
}
