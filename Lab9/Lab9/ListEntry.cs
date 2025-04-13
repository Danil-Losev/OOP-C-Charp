using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    public class ListEntry
    {
        public string NameOfCollection { get; set; }
        public Update NameOfUpdate { get; set; }
        public string NameOfProperty { get; set; }
        public string TKey { get; set; }

        public ListEntry(string nameOfCollection, Update nameOfUpdate, string nameOfProperty, string tKey)
        {
            NameOfCollection = nameOfCollection;
            NameOfUpdate = nameOfUpdate;
            NameOfProperty = nameOfProperty;
            TKey = tKey;
        }

        public override string ToString()
        {
            return "Name Of Collection: " + NameOfCollection + " | " +
                   "Name Of Update: " + NameOfUpdate.ToString() + " | " +
                   "Name Of Property: " + NameOfProperty + " | " +
                   "TKey: " + TKey;
        }
    }
}
