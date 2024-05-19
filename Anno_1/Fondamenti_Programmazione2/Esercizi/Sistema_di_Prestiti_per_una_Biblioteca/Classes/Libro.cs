using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_di_Prestiti_per_una_Biblioteca
{
    internal class Libro
    {
        private string titolo;
        private string autore;
        private string genere;

        private int id;

        public Libro(string titolo, string autore, string genere, int id)
        {
            this.titolo = titolo;
            this.autore = autore;
            this.genere = genere;
            this.id = id;
        }

        // GET // SET

        public string Titolo { get { return titolo; } }

        public string Autore { get {  return autore; } }    

        public string Genere { get {  return genere; } }

        public int Id { get { return id; } }


    }
}
