using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    public class Listener
    {
        private List<ListEntry> entries;

        public Listener()
        {
            entries = new List<ListEntry>();
        }

        private void HandleMagazineAdded(object source, MagazineListHandlerEventArgs args)
        {
            ListEntry entry = new ListEntry(args.NameOfChangedCollection, args.TypeOfChangedEllement, args.NumberOfChangedEllement);
            entries.Add(entry);
        }
        private void HandleMagazineReplaced(object source, MagazineListHandlerEventArgs args)
        {
            ListEntry entry = new ListEntry(args.NameOfChangedCollection, args.TypeOfChangedEllement, args.NumberOfChangedEllement);
            entries.Add(entry);
        }

        public void Subscribe(MagazineCollection collection)
        {
            collection.MagazineAdded += HandleMagazineAdded;
            collection.MagazineReplaced += HandleMagazineReplaced;
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
