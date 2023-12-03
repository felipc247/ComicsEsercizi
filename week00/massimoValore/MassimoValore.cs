// See https://aka.ms/new-console-template for more information
// int.MaxValue rappresenta il numero intero maggiore
// che possiamo ottenere, questo è dovuto al numero di bit che lo compongono
// 0111 1111 1111 1111 1111 1111 1111 1111
int max = int.MaxValue;
Console.WriteLine("Max int value = " + max);
// aggiungendo 1 abbiamo un overflow ed otteniamo
// 1000 0000 0000 0000 0000 0000 0000 0000
// il bit più significativo, cioè a sinistra assume il valore di 1
// questo porta a modificare il segno da + a -
max += 1;
Console.WriteLine("Max int value + 1 = " + max);
