using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab9
{
    [DataContract]
    [Serializable]
    [XmlType("Person")]
    public class Person
    {
        //private string name;
        //private string surname;
        //private System.DateTime birthDate;

        [DataMember]
        [XmlElement("Name")]
        public string Name { get; set; }

        [DataMember]
        [XmlElement("Surname")]
        public string Surname { get; set; }

        [DataMember]
        [XmlElement("BirthDate")]
        public System.DateTime BirthDate{get;set;}

        [XmlIgnore]
        public int BirthYear
        {
            get { return BirthDate.Year; }
            set { BirthDate = new DateTime(value, BirthDate.Month, BirthDate.Day); }
        }

        public Person()
        {
            Name = "N/A";
            Surname = "N/A";
            BirthDate = new DateTime();
        }

        public Person(string name, string surname, System.DateTime birthDate)
        {
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
        }

        public override string ToString()
        {
            return "Name: " + Name + ", Surname: " + Surname + ", Year of birth: " + BirthDate.ToShortDateString() + " ";
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
