using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Inventario_del_Giocatore
{
    internal class Oggetto
    {
        private double peso;
        private string nome;

        public Oggetto(double peso, string nome) {
            this.nome = nome;
            this.peso = peso;
        }

        // GET // SET

        public double Peso { get { return peso; } }

        public string Nome { get {  return nome; } }

        // Methods

        public virtual string ToString() {
            return $"Peso: {peso}, Nome: {nome}";
        }

    }
}
