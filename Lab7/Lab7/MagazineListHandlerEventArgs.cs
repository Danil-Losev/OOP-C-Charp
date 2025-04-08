using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class MagazineListHandlerEventArgs : EventArgs
    {
        public string NameOfChangedCollection { get; set; }
        public string TypeOfChangedEllement { get; set; }
        public int NumberOfChangedEllement { get; set; }
        public MagazineListHandlerEventArgs(string nameOfChangedCollection, string typeOfChangedEllement, int numberOfChangedEllement)
        {
            NameOfChangedCollection = nameOfChangedCollection;
            TypeOfChangedEllement = typeOfChangedEllement;
            NumberOfChangedEllement = numberOfChangedEllement;
        }
        public override string ToString()
        {
            return "Name of changed collection: " + NameOfChangedCollection + ", " +
                   "Information about changes: " + TypeOfChangedEllement + ", " +
                   "Number of changed element: " + NumberOfChangedEllement;
        }
    }
}
