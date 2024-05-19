using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHashTable
{
    internal class HashTable<Tkey, Tvalue>
    {
        private int capacity;
        private List<KeyValuePair<Tkey, Tvalue>>[] Table;

        public HashTable(int capacity)
        {
            this.capacity = capacity;
            Table = new List<KeyValuePair<Tkey, Tvalue>>[this.capacity];
        }

        private int GetHash(Tkey key)
        {
            return Math.Abs(key.GetHashCode()) % Table.Length;
        }

        public void Add_value(Tkey key, Tvalue value)
        {
            int index = GetHash(key);

            // se List a index non esiste la creiamo
            if (Table[index] == null)
                Table[index] = new List<KeyValuePair<Tkey, Tvalue>>();

            if (Table[index].Find(t => t.Key.Equals(key)).Key == null)
            {
                // Aggiungo l'hash del valore e il valore nel bucket a index
                Table[index].Add(new KeyValuePair<Tkey, Tvalue>(key, value));
                Console.WriteLine("Aggiungo valore");
            }
            else
            {
                Console.WriteLine("Valore presente, testa di pane");
            }
        }

        public Tvalue Find_value(Tkey key, Tvalue tvalue)
        {
            int index = GetHash(key);

            if (Table[index] != null) {
                if (Table[index].Find(t => t.Value.Equals(tvalue)).Value != null) {
                    return Table[index].Find(t => t.Value.Equals(tvalue)).Value;
                }
            }

            throw new KeyNotFoundException("Chiave non trovata" + key);
        }

        public void Delete_value(Tkey key,Tvalue tvalue) { 
            int index = GetHash(key);

            if (Table[index] != null)
            {
                if (Table[index].Find(t => t.Value.Equals(tvalue)).Value != null)
                {
                    Table[index].Remove(Table[index].Find(t => t.Value.Equals(tvalue)));
                    return;
                }
            }

            throw new KeyNotFoundException("Chiave non trovata" + key);
        }
    }
}
