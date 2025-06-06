﻿
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Lab9
{
    public enum Update 
    { 
        Add,
        Replace,
        Property
    }

    [Serializable]
    [DataContract]
    [XmlType("Edition")]
    public class Edition: IComparable, IComparer<Edition>, INotifyPropertyChanged
    {
        //protected string name;
        //protected System.DateTime releaseDate;
        //protected int circulation;

        [DataMember]
        [XmlElement("Name")]
        public string Name{get;set;}

        [DataMember]
        [XmlElement("ReleaseDate")]
        public System.DateTime ReleaseDate{get;set;}

        [DataMember]
        [XmlElement("Circulation")]
        public int Circulation {get;set;}

        public Edition()
        {
            Name = "N/A";
            ReleaseDate = new DateTime();
            Circulation = 0;
        }

        public Edition(string nameOfEdition, System.DateTime releaseDate, int circulation)
        {
            Name = nameOfEdition;
            ReleaseDate = releaseDate;
            Circulation = circulation;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Edition edition = (Edition)obj;
            return edition.Name == Name && edition.ReleaseDate == ReleaseDate && edition.Circulation == Circulation;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + ReleaseDate.GetHashCode() + Circulation.GetHashCode();
        }

        public override string ToString()
        {
            return "Name of edition: " + Name + ", Release date: " + ReleaseDate + ", Circulation: " + Circulation;
        }

        public static bool operator ==(Edition edition1, Edition edition2)
        {
            if (ReferenceEquals(edition1, edition2)) return true;
            if ((object)edition1 == null || (object)edition2 == null) return false;
            return edition1.Name == edition2.Name && edition1.ReleaseDate == edition2.ReleaseDate && edition1.Circulation == edition2.Circulation;

        }
        public static bool operator !=(Edition edition1, Edition edition2)
        {
            if ((object)edition1 == null || (object)edition2 == null) return true;
            return edition1.Name != edition2.Name || edition1.ReleaseDate != edition2.ReleaseDate || edition1.Circulation != edition2.Circulation;
        }

        // Lab 6

        public int CompareTo(object? obj) 
        {
            if (obj == null) return 1;
            Edition edition = (Edition)obj;
            if(edition == null) throw new ArgumentException("Object is not an Edition.");
            return Name.CompareTo(edition.Name);
        }

        public int Compare(Edition edition1, Edition edition2)
        {
            if (edition1 == null || edition2 == null) throw new ArgumentException("One or both objects are not Editions.");
            return edition1.ReleaseDate.CompareTo(edition2.ReleaseDate);
        }

        // Lab 8
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
        public class EditionComparer : IComparer<Edition>
        {
            public int Compare(Edition edition1, Edition edition2)
            {
                if (edition1 == null || edition2 == null) throw new ArgumentException("One or both objects are not Editions.");
                return edition1.Circulation.CompareTo(edition2.Circulation);
            }
        }
    }