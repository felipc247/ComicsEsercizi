// See https://aka.ms/new-console-template for more information
// dichiarazione variabili
String var1;
String var2;
String var3;
// comunico all'utente cosa deve fare
Console.WriteLine("Inserisci prima variabile:");
// lettura var1
var1 = Console.ReadLine();
Console.WriteLine("Inserisci seconda variabile:");
// lettura var2
var2 = Console.ReadLine();
// stampo valori variabili
Console.WriteLine("Prima variabile = " + var1);
Console.WriteLine("Seconda variabile = "+ var2);
// scambio valori con variabile di supporto
var3 = var1;
var1 = var2;
var2 = var3;
// termine scambio valori
// stampa valori scambiati
Console.WriteLine("Prima variabile = " + var1);
Console.WriteLine("Seconda variabile = " + var2);

