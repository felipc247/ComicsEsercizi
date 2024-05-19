using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_di_Prestiti_per_una_Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();

            int id_libro = 1;

            Libro libro = new Libro("Spy anya", "Me", "Spy", id_libro++);
            Libro libro1 = new Libro("Spy anya", "Me", "Spy", id_libro++);
            Libro libro2 = new Libro("Spy anya", "Me", "Spy", id_libro++);
            Libro libro3 = new Libro("Spy anya", "Me", "Spy", id_libro++);

            int id_utente = 1;

            Utente utente = new Utente("Anya", "Forger", id_utente++);
            Utente utente1 = new Utente("Yor", "Forger", id_utente++);
            Utente utente2 = new Utente("Loid", "Forger", id_utente++);
            Utente utente3 = new Utente("Bond", "Forger", id_utente++);

            biblioteca.Add_Book(libro);
            biblioteca.Add_Book(libro1);
            biblioteca.Add_Book(libro2);
            biblioteca.Add_Book(libro3);

            biblioteca.Add_User(utente);
            biblioteca.Add_User(utente1);
            biblioteca.Add_User(utente2);
            biblioteca.Add_User(utente3);

            biblioteca.Borrow_Book(1, 1);
            biblioteca.Borrow_Book(2, 3);
            biblioteca.Borrow_Book(2, 4);
            biblioteca.Borrow_Book(3, 2);
            biblioteca.Borrow_Book(3, 4);

            biblioteca.Remove_Book(1);
            biblioteca.Remove_User(1);

            biblioteca.Back_Book(1);
            biblioteca.Back_Book(2);

            biblioteca.Remove_User(1);
            biblioteca.Remove_Book(1);

            biblioteca.Print_All_Books();

            biblioteca.Print_All_Users();

            biblioteca.Print_All_Loans();


        }
    }
}
