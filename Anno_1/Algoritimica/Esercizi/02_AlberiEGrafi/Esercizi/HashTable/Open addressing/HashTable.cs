using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable_Open_addressing
{
    internal class HashTable<Tkey, Tvalue>
    {
        private int capacity;
        private KeyValuePair<Tkey, Tvalue>[] Table;

        public HashTable(int capacity)
        {
            this.capacity = capacity;
            Table = new KeyValuePair<Tkey, Tvalue>[this.capacity];
        }

        private int GetHashIndex(Tkey key, int tries)
        {
            return Math.Abs(key.GetHashCode() + tries) % Table.Length;
        }

        private int GetHash(Tkey key)
        {
            return key.GetHashCode();
        }

        public void Add_value(Tkey key, Tvalue value, Tkey keyHash)
        {
            int index = GetHashIndex(key);

            // Effettuiamo l'hash
            // Se collisione rieffettuiamo l'hash
            if (Table[index] == null)
                Table[index] = new KeyValuePair<Tkey, Tvalue>();

            // Controllo se il valore è già stato inserito
            if (!Table.Any(t => t.Value.Equals(value)))
            {
                // se non
                // Aggiungo l'hash del valore e il valore nel bucket a index
                Console.Write($"Aggiungo value: {value}, key: {keyHash}");
                if (Table.Length > 0) Console.WriteLine($" - Collisione ad index: {index}");
                else Console.WriteLine();

                Table[index] = new KeyValuePair<Tkey, Tvalue>(keyHash, value);
            }
            else
            {
                Console.WriteLine($"value: {value}, already added at index: {index}");
            }
        }

        public Tvalue Find_value(Tkey key, Tvalue value)
        {
            int index = GetHashIndex(key);

            if (Table[index].Key != null)
            {
                // LINQ metodo, ritorna il primo elemento che soddisfa la condizione
                if (Table[index].FirstOrDefault(t => t.Value.Equals(value)).Value != null)
                {
                    return Table[index].FirstOrDefault(t => t.Value.Equals(value)).Value;
                }
            }

            throw new KeyNotFoundException();
        }

        public void Delete_value(Tkey key, Tvalue tvalue)
        {
            int index = GetHashIndex(key);

            if (Table[index] != null)
            {
                if (Table[index].FirstOrDefault(t => t.Value.Equals(tvalue)).Value != null)
                {
                    Table[index].Remove(Table[index].FirstOrDefault(t => t.Value.Equals(tvalue)));
                    return;
                }
            }

            throw new KeyNotFoundException();
        }
    }
}