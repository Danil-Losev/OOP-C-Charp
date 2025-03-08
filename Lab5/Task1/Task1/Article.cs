using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public enum Frequency
    {
        Weekly,
        Monthly,
        Yearly
    }

    internal class Article: IRateAndCopy
    {
        public Person Author { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        public Article()
        {
            Author = new Person(); // Assuming Person has a parameterless constructor
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

        double IRateAndCopy.Rating
        {
            get { return Rating; }
        }

    }
}