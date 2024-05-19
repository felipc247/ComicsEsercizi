using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio_4
{
    internal class Brano 
    {
        private string titolo;
        private string artista;

        public Brano(string titolo, string artista)
        {
            this.titolo = titolo;
            this.artista = artista;
        }

        // GET // SET

        public string Titolo { get {  return titolo; } }
        public string Artista { get {  return artista; } }

        // Methods

        public string Descrizione() {
            return $"Titolo: {titolo}, Artista: {artista}";
        } 


    }
}
