// See https://aka.ms/new-console-template for more information

// ex00
String password = "cotoletta";
String passwordInserita = "";
int i = 0;
do
{
    if (i == 0)
    {
        i++;
    }
    else
    {
        Console.WriteLine("Mi dispiace la password è sbagliata! Riprova...");
    }
    Console.WriteLine("Inserisci Password");
    passwordInserita = Console.ReadLine();

} while (!passwordInserita.Equals(password));
Console.WriteLine("Complimenti la password corretta!");
