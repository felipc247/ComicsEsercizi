// See https://aka.ms/new-console-template for more information

// See https://aka.ms/new-console-template for more information

// ex00 Bonus
String password = "cotoletta";
String passwordInserita = "";
int i = 0; // per controllare che sia la prima volta
int countErr = 0;
do
{
    // se non è la prima volta stampo mess di password errata
    if (i == 0)
    {
        i++;
    }
    else
    {
        // aggiungo un errore
        countErr++;
        // tentativi esauriti
        if (countErr == 3)
        {
            Console.WriteLine("Mi dispiace hai esaurito i tentativi!");
            break;
        }
        Console.WriteLine("Mi dispiace la password è sbagliata! Riprova...");

    }
    Console.WriteLine("Inserisci Password");
    passwordInserita = Console.ReadLine();

} while (!passwordInserita.Equals(password));

if (countErr < 3) Console.WriteLine("Complimenti la password corretta!");



