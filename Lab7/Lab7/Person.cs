using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class Person
    {
        private string name;
        private string surname;
        private System.DateTime birthDate;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public System.DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public int BirthYear
        {
            get { return birthDate.Year; }
            set { birthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
        }

        public Person()
        {
            name = "N/A";
            surname = "N/A";
            birthDate = System.DateTime.Now;
        }

        public Person(string name, string surname, System.DateTime birthDate)
        {
            this.name = name;
            this.surname = surname;
            this.birthDate = birthDate;
        }

        public override string ToString()
        {
            return "Name: " + Name + ", Surname: " + Surname + ", Year of birth: " + BirthDate + " ";
        }

        virtual public string ToShortString()
        {
            return "Name: " + Name + ", Surname: " + Surname + " ";
        }


        // Lab 5

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Person person = (Person)obj;
            return (Name == person.Name && Surname == person.Surname && BirthDate == person.BirthDate);
        }

        public static bool operator ==(Person person1, Person person2)
        {
            if (ReferenceEquals(person1, person2)) return true;
            if ((object)person1 == null || (object)person2 == null) return false;
            return person1.Name == person2.Name && person1.Surname == person2.Surname && person1.BirthDate == person2.BirthDate;
        }
        public static bool operator !=(Person person1, Person person2)
        {
            if ((object)person1 == null || (object)person2 == null) return false;
            return person1.Name != person2.Name || person1.Surname != person2.Surname || person1.BirthDate != person2.BirthDate;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Surname.GetHashCode() + BirthDate.GetHashCode();
        }

        virtual public object DeepCopy()
        {
            return new Person(Name, Surname, BirthDate);
        }

        // Lab 6

    }
}
