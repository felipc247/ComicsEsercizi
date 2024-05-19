// See https://aka.ms/new-console-template for more information

int[] nums = new int[4];
// richiedo variabili
for (int i = 0; i < nums.Length; i++)
{
    Console.WriteLine("Inserisci num " + i + ":");
    nums[i] = int.Parse(Console.ReadLine());
}
// calcoli
int somma = nums[0] + nums[1];
int differenza = nums[2] + nums[3];
int prodotto = somma * differenza;

// Uso un operatore ternario per evitare di usare la struttura di controllo if else
String risultato = (prodotto == 42) ? "Uguale a 42" : prodotto+"";
String parita = (prodotto % 2 == 0) ? "Pari" : "Dispari";

// stampo risultato
Console.WriteLine("Il risultato è " +  risultato + "\nIl risultato è "+ parita);
//Coto