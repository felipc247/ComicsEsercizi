using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Biblioteca
{
    internal class Biblioteca
    {
        private List<Libro> libros = new List<Libro>();
        private String nome;

        public Biblioteca(String nome){
            this.nome = nome;
        }

        public String Nome { 
            get { return nome; }
            set { nome = value; }
        }

        public void AggiungiLibro(Libro libro) { 
            libros.Add(libro);  
        }

        public void VisualizzaCatalogo() { 
            foreach (Libro libro in libros) {
                libro.VisualizzaInfo();
            }
        }

        public bool CercaLibro(string titolo) {
            // lambda expression che restituisce il primo elemento in base
            // ad uno o più criteri di ricerca
            Libro libroCercato = libros.Find(l => l.Titolo == titolo);
            return libroCercato != null;
        }

    }
}
