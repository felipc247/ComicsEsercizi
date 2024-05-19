using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Inventario_del_Giocatore
{
    internal class InventarioGiocatore
    {
        private Type arma_type = new Arma(0, "", Equipaggiabile.Tipologia.arma, 10).GetType();
        private Type armatura_type = new Armatura(0, "", Equipaggiabile.Tipologia.armatura, 10).GetType();
        private Type consumabile_type = new Consumabile(10, "", 3).GetType();

        private List<Oggetto> oggettoList;
        private Arma arma = null;
        private Armatura armatura = null;
        private int peso_max;
        private double peso_act = 0;

        public InventarioGiocatore(int peso_max)
        {
            oggettoList = new List<Oggetto>();
            this.peso_max = peso_max;
        }

        // GET // SET

        public List<Oggetto> OggettoList { get { return oggettoList; } }

        public Arma Arma { get { return arma; } }

        public Armatura Armatura { get { return armatura; } }

        public int Peso_max { get { return peso_max; } }

        // Methods

        public void PickObject(Oggetto oggetto)
        {
            if (oggetto.Peso + peso_act > peso_max)
            {
                Console.WriteLine("Peso eccessivo, non puoi raccogliere l'oggetto");
                return;
            }
            //Type oggetto_type = oggetto.GetType();

            //if (oggetto_type.Equals(arma_type))
            //{
            //    arma = (Arma)oggetto;
            //}
            //else if (oggetto_type.Equals(armatura_type))
            //{
            //    armatura = (Armatura)oggetto;
            //}
            //else
            //{
            oggettoList.Add(oggetto);
            //}

            peso_act += oggetto.Peso;
        }

        public void RemoveObject(Oggetto oggetto)
        {
            Type oggetto_type = oggetto.GetType();

            // Se l'oggetto è un'arma o un'armatura
            // Controllo se sono equipaggiate
            // Nel caso le rimuovo da lì
            if (oggetto_type.Equals(arma_type) && arma != null)
            {
                if (oggetto.Equals(arma)) { arma = null; peso_act += oggetto.Peso; }
            }
            else if (oggetto_type.Equals(armatura_type) && armatura != null)
            {
                if (oggetto.Equals(armatura)) { armatura = null; peso_act += oggetto.Peso; }
            }
            else
            {
                // Altrimenti rimuovo dalla lista
                oggettoList.Remove(oggetto);
            }

            peso_act -= oggetto.Peso;
        }

        public void UseUsable(Oggetto oggetto)
        {
            Type oggetto_type = oggetto.GetType();

            if (oggetto_type.Equals(consumabile_type))
            {
                Consumabile temp = (Consumabile)oggetto;
                temp.Use();
            }
        }

        public void EquipeEquipable(Oggetto oggetto)
        {
            Type oggetto_type = oggetto.GetType();

            if (oggetto_type.Equals(arma_type))
            {
                // ?
                arma = (Arma)oggetto;
                RemoveObject(oggetto);
            }
            else if (oggetto_type.Equals(armatura_type))
            {
                // ?
                armatura = (Armatura)oggetto;
                RemoveObject(oggetto);
            }


        }

        public void Print_all()
        {
            Console.WriteLine("Inventario:");

            foreach (Oggetto o in oggettoList)
            {
                Console.WriteLine(o.ToString());
            }

            if (arma != null) Console.WriteLine(arma.ToString());
            if (armatura != null) Console.WriteLine(armatura.ToString());
            Console.WriteLine("\nPeso totale: " + peso_act + ", Peso max: " + peso_max);
        }



    }
}
