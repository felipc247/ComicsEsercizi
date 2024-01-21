using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHashTable
{
    internal class Program
    {

        static void Main(string[] args)
        {
            HashTable<int, string> hashTable = new HashTable<int, string>(3);
            int v1 = 3, v2 = 4, v3 = 5, v4 = 6;
            hashTable.Add_value(v1, "coto", v1.GetHashCode());
            hashTable.Add_value(v2, "re", v2.GetHashCode());
            hashTable.Add_value(v3, "wd", v3.GetHashCode());
            hashTable.Add_value(v4, "bd", v4.GetHashCode());
            hashTable.Add_value(v4, "bd", v4.GetHashCode());
            try
            {
                hashTable.Delete_value(v2, "re");
                Console.WriteLine("Coppia Chiave/valore trovati ed eliminati");
                hashTable.Find_value(v2, "re");
                Console.WriteLine("Coppia Chiave/valore trovati");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Coppia Chiave/valore NON trovati");
            }
        }
    }
}
