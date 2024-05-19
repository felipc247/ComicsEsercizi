using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_di_Prestiti_per_una_Biblioteca
{
    internal class Utente
    {
        private string nome;
        private string cognome;
        private int id;

        public Utente(string nome, string cognome, int id)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.id = id;
        }
        
        // GET // SET

        public string Nome { get {  return nome; } }    

        public string Cognome { get {  return cognome; } }

        public int Id { get { return id; } }

        

    }
}
