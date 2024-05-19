using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_di_Prestiti_per_una_Biblioteca
{
    internal class Biblioteca
    {
        private List<Libro> libri;
        private List<Utente> utenti;
        private Dictionary<int, int> prestiti; // <id_libro, id_utente>

        public Biblioteca()
        {
            libri = new List<Libro>();
            utenti = new List<Utente>();
            prestiti = new Dictionary<int, int>();
        }

        // GET // SET

        // Methods

        // Libri

        public void Add_Book(Libro libro)
        {
            // Non posso aggiungere libri con lo stesso Id
            if (libri.Find(l => l.Id == libro.Id) != null)
            {
                Console.WriteLine($"Libro: {libro.Id} già presente");
                return;
            }
            libri.Add(libro);
            Console.WriteLine($"Libro: {libro.Id} aggiunto");
        }

        public void Remove_Book(int id_libro)
        {
            // Non posso rimuovere un libro in prestito
            if (!prestiti.ContainsKey(id_libro))
            {
                libri.Remove(libri.Find(l => l.Id == id_libro));
                Console.WriteLine($"Libro: {id_libro} rimosso");
            }
            else
            {
                Console.WriteLine($"Libro: {id_libro} in prestito, non rimuovibile");
            }
        }

        public Libro Search_Book(int id_libro)
        {
            Libro libro = libri.Find(l => l.Id == id_libro);
            if (libro != null) Console.WriteLine($"Libro trovato, Id: {id_libro}");
            else Console.WriteLine($"Libro NON trovato, Id: {id_libro}");
            return libro;
        }

        public void Print_All_Books()
        {
            foreach (var libro in libri)
            {
                string titolo = libro.Titolo;
                string autore = libro.Autore;
                string genere = libro.Genere;
                int id = libro.Id;

                Console.WriteLine($"Titolo: {titolo}, Autore: {autore}, Genere: {genere}, Id: {id}");
            }
        }

        // Utenti

        public void Add_User(Utente utente)
        {
            // Non posso aggiungere utenti con lo stesso Id
            if (utenti.Find(u => u.Id == utente.Id) != null)
            {
                Console.WriteLine($"Libro: {utente.Id} già presente"); 
                return;
            }
            utenti.Add(utente);
            Console.WriteLine($"Utente: {utente.Id} aggiunto");
        }

        public void Remove_User(int id_utente)
        {
            // Non posso rimuovere un utente con libri in prestito
            if (!prestiti.ContainsValue(id_utente))
            {
                utenti.Remove(utenti.Find(u => u.Id == id_utente));
                Console.WriteLine($"Utente: {id_utente} rimosso");
            }
            else {
                Console.WriteLine($"Utente: {id_utente} ha un/più libro/i in prestito, non rimuovibile");
            }
        }

        public void Print_All_Users()
        {
            foreach (var utente in utenti)
            {
                string nome = utente.Nome;
                string cognome = utente.Cognome;
                int id = utente.Id;

                Console.WriteLine($"Titolo: {nome}, Autore: {cognome}, Id: {id}");
            }
        }

        // Prestiti

        public void Borrow_Book(int id_libro, int id_utente)
        {
            if (!prestiti.ContainsKey(id_libro))
            {
                prestiti.Add(id_libro, id_utente);
                Console.WriteLine($"Prestito avvenuto | id_libro: {id_libro}, id_utente: {id_utente}");
            }
            else {
                Console.WriteLine($"Prestito impossibile | libro: {id_libro} è in prestito");
            }
        }

        public void Back_Book(int id_libro)
        {
            if (prestiti.ContainsKey(id_libro))
            {
                prestiti.Remove(id_libro);
                Console.WriteLine($"Libro: {id_libro} non più in prestito");
            }
            else
            {
                Console.WriteLine($"Libro: {id_libro} non in prestito");
            }
        }

        public void Print_All_Loans() {
            foreach (var prestito in prestiti)
            {
                int id_libro = prestito.Key;
                int id_utente = prestito.Value;
                Console.WriteLine($"id_libro: {id_libro}, id_utente {id_utente}");
            }
        }

    }
}
