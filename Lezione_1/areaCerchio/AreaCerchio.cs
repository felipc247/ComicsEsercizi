// See https://aka.ms/new-console-template for more information
double raggio = 0;
double area = 0;
Console.WriteLine("Inserisci raggio:");
// converto la stringa a double
raggio = double.Parse(Console.ReadLine());
// calcolo l'area del cerchio
area = Math.PI * Math.Pow(raggio, 2);
// stampo risultato
Console.WriteLine("L'area del cerchio è = "+ area);
