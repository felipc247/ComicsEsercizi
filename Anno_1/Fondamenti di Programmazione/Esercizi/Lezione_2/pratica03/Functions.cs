// See https://aka.ms/new-console-template for more information

// ex00
double CalcolaAreaCerchio(double raggio) {
    return Math.PI * Math.Pow(raggio, 2);
}

double raggio = 0;
Console.WriteLine("Inserire raggio");

bool OKAY = false;
do
{
	try
	{
        raggio = double.Parse(Console.ReadLine());
		OKAY = true;    
	}
	catch
	{
		Console.WriteLine("Numero non idoneo");
	}
    
} while (!OKAY);

Console.WriteLine($"Il raggio è {CalcolaAreaCerchio(raggio)}");
