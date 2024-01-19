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
            HashTable<string, string> hashTable = new HashTable<string, string>(1);
            hashTable.Add_value("coto", "coto");
            hashTable.Add_value("coto", "coto");
            hashTable.Add_value("b", "b");
            hashTable.Add_value("b", "b");
            try
            {
                hashTable.Delete_value("b", "b");

            }
            catch (KeyNotFoundException)
            {
            }
        }
    }
}
