// See https://aka.ms/new-console-template for more information
using System.Collections;

ArrayList nomi = new ArrayList();
int cod = 0;
do
{
    Console.WriteLine("Cod 1 = aggiungi nome\n" +
        "Cod 2 = rimuovi nome\n" +
        "Cod 3 = visualizza elenco\n" +
        "Cod 0 = Esci");
    cod = int.Parse(Console.ReadLine());
    switch (cod)
    {
        case 0:
            Console.WriteLine("Uscita programma");
            break;
        case 1:
            Console.WriteLine("Inserire nome:");
            nomi.Add(Console.ReadLine());
            break;
        case 2:
            Console.WriteLine("Inserire posizione nome da rimuovere:");
            nomi.RemoveAt(int.Parse(Console.ReadLine()));
            break;
            case 3:
            Console.WriteLine("Elenco nomi:");
            int i = 0;
            foreach (String nome in nomi) {
                Console.WriteLine("["+ i++ +"] "+nome);
            }
            break;
            default: Console.WriteLine("Cod errato, riprovare");
            break;
    }
} while (cod != 0);

