using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    public delegate void MagazineListHandler(object source, MagazineListHandlerEventArgs args);
    public delegate void MagazinesChangedHandler<TKey>(object source, MagazinesChangedEventArgs<TKey> args);

    public class MagazineCollection
    {


        private List<Magazine> magazines;
        public void AddDefaults()
        {
            magazines = new List<Magazine>(){new Magazine("Forbes", Frequency.Monthly, new DateTime(2020, 1, 1), 1000),
              new Magazine("Time", Frequency.Weekly, new DateTime(2020, 1, 1), 500) };
            OnMagazineAdded(NameOfCollection, "Added default magazines", 2);
        }
        public void AddMagazines(params Magazine[] newMagazines)
        {
            if (magazines == null)
            {
                magazines = new List<Magazine>();
            }
            magazines.AddRange(newMagazines);
            OnMagazineAdded(NameOfCollection, "Added magazines", newMagazines.Length);
        }
        public override string ToString()
        {
            string result = "";
            foreach (Magazine magazine in magazines)
            {
                result += magazine.ToString() + "\n";
            }
            return result;
        }
        public virtual string ToShortString()
        {
            string result = "";
            foreach (Magazine magazine in magazines)
            {
                result += magazine.ToShortString() + "\n";
            }
            return result;
        }

        public int CollectionLenth
        {
            get
            {
                return magazines.Count;
            }
        }

        private static int nameCompare(Magazine m1, Magazine m2)
        {
            return m1.Edition.CompareTo(m2.Edition);
        }

        public void SortByName()
        {
            magazines.Sort(nameCompare);
        }

        public void SortByReleaseDate()
        {
            magazines.Sort(new Edition());
        }


        public void SortByCirculation()
        {
            magazines.Sort(new EditionComparer());
        }

        public double MaxRating
        {
            get
            {
                double MaxRating = 0;
                foreach (Magazine magazine in magazines)
                {
                    if (magazine.Rating > MaxRating)
                    {
                        MaxRating = magazine.Rating;
                    }
                }
                return MaxRating;
            }
        }

        public IEnumerable<Magazine> GetMonthlyMagazine
        {
            get
            {
                foreach (Magazine magazine in magazines)
                {
                    if (magazine.Frequency == Frequency.Monthly)
                    {
                        yield return magazine;
                    }

                }
            }
        }

        public List<Magazine> RatingGroup(double value)
        {
            List<Magazine> magazinesWithRating = new List<Magazine>();
            foreach (Magazine magazine in magazines)
            {
                if (magazine.Rating >= value)
                {
                    magazinesWithRating.Add(magazine);
                }
            }
            return magazinesWithRating;
        }


        public string NameOfCollection { get; set; }

        public bool Replace(int index, Magazine magazine)
        {
            if (index < 0 || index >= magazines.Count)
            {
                return false;
            }
            magazines[index] = magazine;
            OnMagazineReplaced(NameOfCollection, "Replaced magazine", index);
            return true;
        }

        public Magazine this[int index]
        {
            get
            {
                if (index < 0 || index >= magazines.Count)
                {
                    throw new Exception("Index out of range");
                }
                return magazines[index];
            }
            set
            {
                if (index < 0 || index >= magazines.Count)
                {
                    throw new Exception("Index out of range");
                }
                OnMagazineReplaced(NameOfCollection, "Set magazine", index);
                magazines[index] = value;
            }
        }

        public event MagazineListHandler MagazineAdded;
        public event MagazineListHandler MagazineReplaced;

        protected virtual void OnMagazineAdded(string nameOfChangedCollection, string typeOfChangedEllement, int numberOfChangedEllement)
        {
            if (MagazineAdded != null)
            {
                MagazineListHandlerEventArgs args = new MagazineListHandlerEventArgs(nameOfChangedCollection, typeOfChangedEllement, numberOfChangedEllement);
                MagazineAdded(this, args);
            }
        }

        protected virtual void OnMagazineReplaced(string nameOfChangedCollection, string typeOfChangedEllement, int numberOfChangedEllement)
        {
            if (MagazineReplaced != null)
            {
                MagazineListHandlerEventArgs args = new MagazineListHandlerEventArgs(nameOfChangedCollection, typeOfChangedEllement, numberOfChangedEllement);
                MagazineReplaced(this, args);
            }
        }
    }

    public class MagazineCollection<TKey>
    {
        private Dictionary<TKey, Magazine> magazines;

        public string NameOfCollection { get; set; }
        public void AddDefaults()
        {
            magazines = new Dictionary<TKey, Magazine>();
            magazines.Add((TKey)(object)"Forbes", new Magazine("Forbes", Frequency.Monthly, new DateTime(2020, 1, 1), 1000));
            magazines.Add((TKey)(object)"Time", new Magazine("Time", Frequency.Weekly, new DateTime(2020, 1, 1), 500));
            magazines[(TKey)(object)"Forbes"].PropertyChanged += OnPropertyChanged;
            magazines[(TKey)(object)"Time"].PropertyChanged += OnPropertyChanged;
            OnMagazinesChanged(NameOfCollection, Update.Add, " ", (TKey)(object)"Forbes");
            OnMagazinesChanged(NameOfCollection, Update.Add, " ", (TKey)(object)"Time");
        }

        public void AddMagazines(params Magazine[] newMagazines)
        {
            if (magazines == null)
            {
                magazines = new Dictionary<TKey, Magazine>();
            }
            foreach (Magazine magazine in newMagazines)
            {
                magazines.Add((TKey)(object)magazine.Name, magazine);
                magazines[(TKey)(object)magazine.Name].PropertyChanged += OnPropertyChanged;
                OnMagazinesChanged(NameOfCollection, Update.Add, " ", (TKey)(object)magazine.Name);
            }
        }

        public override string ToString()
        {
            string result = "";
            foreach (KeyValuePair<TKey, Magazine> magazine in magazines)
            {
                result += magazine.Value.ToString() + "\n";
            }
            return result;
        }

        public Magazine this[TKey key]
        {
            get
            {
                if (!magazines.ContainsKey(key))
                {
                    throw new Exception("Key not found");
                }
                return magazines[key];
            }
            set
            {
                if (!magazines.ContainsKey(key))
                {
                    throw new Exception("Key not found");
                }
                magazines[key] = value;
                magazines[key].PropertyChanged += OnPropertyChanged;
                OnMagazinesChanged(NameOfCollection, Update.Replace, " ", key);
            }
        }

        public bool Replace(Magazine oldMagazine, Magazine newMagazine)
        {
            if (! magazines.ContainsValue(oldMagazine)) return false;

            foreach (KeyValuePair<TKey, Magazine> magazine in magazines)
            {
                if (magazine.Value == oldMagazine)
                {
                    magazine.Value.PropertyChanged -= OnPropertyChanged;
                    magazines.Remove(magazine.Key);
                    magazines.Add((TKey)(object)newMagazine.Name, newMagazine);
                    newMagazine.PropertyChanged += OnPropertyChanged;
                    break;
                }
            }
            OnMagazinesChanged(NameOfCollection, Update.Replace, " ", (TKey)(object)newMagazine.Name);
            return true;
        }

        public event MagazinesChangedHandler<TKey> MagazinesChanged;

        protected void OnMagazinesChanged(string nameOfCollection, Update typeOfUpdate, string? sourceOfUpdate, TKey keyOfElement)
        {
            if (MagazinesChanged != null)
            {
                MagazinesChangedEventArgs<TKey> args = new MagazinesChangedEventArgs<TKey>(nameOfCollection, typeOfUpdate, sourceOfUpdate, keyOfElement);
                MagazinesChanged(this, args);
            }
        }

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            foreach (KeyValuePair<TKey, Magazine> magazine in magazines)
            {
                if (magazine.Value == sender)
                {
                    OnMagazinesChanged(NameOfCollection, Update.Property, args.PropertyName , magazine.Key);
                    break;
                }
            }
        }
    }
}
