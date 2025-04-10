using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class ListEntry
    {
        public string NameOfCollection { get; set; }
        public string NameOfEvent { get; set; }
        public int NumberOfChangedElement { get; set; }
        public ListEntry(string nameOfCollection, string nameOfEvent, int numberOfChangedElement)
        {
            NameOfCollection = nameOfCollection;
            NameOfEvent = nameOfEvent;
            NumberOfChangedElement = numberOfChangedElement;
        }
        public override string ToString()
        {
            return "Name of collection: " + NameOfCollection + ", " +
                   "Name of event: " + NameOfEvent + ", " +
                   "Number of changed element: " + NumberOfChangedElement;
        }
    }
}
