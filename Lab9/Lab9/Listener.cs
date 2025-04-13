using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    public class Listener<TKey>
    {
        private List<ListEntry> entries;
        public Listener()
        {
            entries = new List<ListEntry>();
        }
        public void OnMagazinesChanged (object source, MagazinesChangedEventArgs<TKey> args)
        {
            ListEntry entry = new ListEntry(args.NameOfCollection, args.TypeOfUpdate, args.SourceOfUpdate, (string)(object)args.KeyOfElement);
            entries.Add(entry);
        }
        public void Subscribe(MagazineCollection<TKey> collection)
        {
            collection.MagazinesChanged += OnMagazinesChanged;
        }

        public override string ToString()
        {
            string result = "";
            foreach (ListEntry entry in entries)
            {
                result += entry.ToString() + "\n";
            }
            return result;
        }
    }
}
