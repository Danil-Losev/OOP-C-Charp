using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab9
{
    public enum Frequency
    {
        Weekly,
        Monthly,
        Yearly
    }

    [DataContract]
    [Serializable]
    [XmlType("Article")]
    public class Article : IRateAndCopy
    {
        [DataMember] [XmlElement("Author")] public Person Author { get; set; }
        [DataMember] [XmlElement] public string Title { get; set; }
        [DataMember] [XmlElement] public double Rating { get; set; }

        public Article()
        {
            Author = new Person(); 
            Title = "N/A";
            Rating = 0;
        }
        public Article(Person author, string title, double rating)
        {
            Author = author;
            Title = title;
            Rating = rating;
        }

        public override string ToString()
        {
            return "Author: ( " + Author + "), Title: " + Title + ", Rating: " + Rating;
        }

        virtual public object DeepCopy()
        {
            return new Article(Author, Title, Rating);
        }


        object IRateAndCopy.DeepCopy()
        {
            return DeepCopy();
        }

        [XmlIgnore]
        double IRateAndCopy.Rating
        {
            get { return Rating; }
        }

    }
}