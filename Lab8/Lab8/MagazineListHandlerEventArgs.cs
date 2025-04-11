using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    public class MagazineListHandlerEventArgs : EventArgs
    {
        public string NameOfChangedCollection { get; set; }
        public string TypeOfChangedEllement { get; set; }
        public int NumberOfChangedEllement { get; set; }
        public MagazineListHandlerEventArgs(string nameOfChangedCollection, string typeOfChangedEllement, int numberOfChangedEllement) : base()
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

    public class MagazinesChangedEventArgs<TKey> : EventArgs
    {
        public string NameOfCollection { get; set; }
        public Update TypeOfUpdate { get; set; }
        public string? SourceOfUpdate { get; set; }
        public TKey KeyOfElement { get; set; }

        public MagazinesChangedEventArgs(string nameOfCollection, Update typeOfUpdate, string? sourceOfUpdate, TKey keyOfElement) : base()
        {
            NameOfCollection = nameOfCollection;
            TypeOfUpdate = typeOfUpdate;
            SourceOfUpdate = sourceOfUpdate;
            KeyOfElement = keyOfElement;
        }

        public override string ToString()
        {
            return "Name of collection: " + NameOfCollection + ", " +
                   "Type of update: " + TypeOfUpdate.ToString() + ", " +
                   "Source of update: " + SourceOfUpdate + ", " +
                   "Key of element: " + KeyOfElement.ToString();
        }
    }
}
