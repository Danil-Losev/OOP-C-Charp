using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;


namespace Task2
{
    public enum Frequency
    {
        Weekly,
        Monthly,
        Yearly
    }

    internal class Article
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
            return "Author: ( " + Author + "), Title: " + Title + ", Rating: " + Rating ;
        }

    }
}
