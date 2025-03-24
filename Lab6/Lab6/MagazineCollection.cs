using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class MagazineCollection
    {
        private List<Magazine> magazines;
        public void AddDefaults()
        {
            magazines = new List<Magazine>(){new Magazine("Forbes", Frequency.Monthly, new DateTime(2020, 1, 1), 1000),
              new Magazine("Time", Frequency.Weekly, new DateTime(2020, 1, 1), 500) };
        }
        public void AddMagazines(params Magazine [] newMagazines)
        {
            if (magazines == null)
            {
                magazines = new List<Magazine>();
            }
            magazines.AddRange(newMagazines);
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
    }
}
