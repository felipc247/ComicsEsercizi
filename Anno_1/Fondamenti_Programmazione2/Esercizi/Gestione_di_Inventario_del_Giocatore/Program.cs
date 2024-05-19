using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestione_di_Inventario_del_Giocatore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InventarioGiocatore inventario = new InventarioGiocatore(30);

            Arma spada = new Arma(3, "Op", Equipaggiabile.Tipologia.arma, 15);
            Armatura corazza = new Armatura(5, "yt", Equipaggiabile.Tipologia.armatura, 50);

            Consumabile mela = new Consumabile(1, "Mela", 2);

            Oggetto bastone = new Oggetto(1, "Bastone");

            inventario.PickObject(spada);
            inventario.PickObject(corazza);
            inventario.PickObject(mela);
            inventario.PickObject(bastone);

            inventario.UseUsable(mela);
            inventario.UseUsable(mela);
            inventario.UseUsable(mela);

            inventario.Print_all();

            inventario.EquipeEquipable(spada);
            inventario.RemoveObject(spada);

            inventario.Print_all();
        }
    }
}
