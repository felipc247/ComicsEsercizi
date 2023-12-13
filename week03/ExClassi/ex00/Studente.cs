using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Studente
{
    internal class Studente
    {
        private String nome;
        private String cognome;
        private int matricola;

        public Studente(string nome, string cognome, int matricola)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.matricola = matricola;
        }

        public String Nome { 
            get { return nome; }
            set { nome = value; }
        }

        public String Cognome
        {
            get { return cognome; }
            set { cognome = value; }
        }

        public void Studiare() { 
            Console.WriteLine("Studiando");
        }

        public void VisualizzaDati() {
            Console.WriteLine($"Nome: {nome}\n" +
                $"Cognome: {cognome}\n" +
                $"Matricola: {matricola}");    
        }
    }
}
