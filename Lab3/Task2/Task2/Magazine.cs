using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;


namespace Task2
{
    internal class Magazine
    {
        private string name;
        private Frequency frequency;
        private System.DateTime releaseDate;
        private int circulation;
        private Article[] articles;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Frequency Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        public System.DateTime ReleaseDate
        {
            get { return releaseDate; }
            set { releaseDate = value; }
        }

        public int Circulation
        {
            get { return circulation; }
            set { circulation = value; }
        }

        public Article[] Articles
        {
            get { return articles; }
            set { articles = value; }
        }


        public Magazine()
        {
            Name = "N/A";
            Frequency = Frequency.Monthly;
            ReleaseDate = DateTime.Now;
            Circulation = 0;
            Articles = new Article[0];
        }

        public Magazine(string name, Frequency frequency, System.DateTime releaseDate, int circulation)
        {
            Name = name;
            Frequency = frequency;
            ReleaseDate = releaseDate;
            Circulation = circulation;
            Articles = new Article[0];
        }

        public double Rating
        {
            get
            {
                double rating = 0;
                foreach (Article article in articles)
                {
                    rating += article.Rating;
                }
                if (articles.Length == 0)
                {
                    return 0;
                }
                return rating / articles.Length;
            }
        }

        public bool this[Frequency frequency]
        {
            get
            {
                if (this.frequency == frequency)
                {
                    return true;
                }
                return false;
            }
        }

        public void AddArticles(params Article[] articles)
        {
            if (this.articles == null)
            {
                this.articles = articles;
            }
            else
            {
                List<Article> addArticles = new List<Article>(this.articles);
                addArticles.AddRange(articles);
                this.articles = addArticles.ToArray();
            }
        }

        public override string ToString()
        {
            string articlesList = "\n";
            foreach (Article article in Articles)
            {
                articlesList += article.ToString() + "\n";
            }
            return "Name: " + Name + ", Frequency: " + Frequency + ", Release Date: " + ReleaseDate + ", Circulation: " + Circulation +  ", Articles:" + articlesList;
        }

        public virtual string ToShortString()
        {
            return "Name: " + name + ", Frequency: " + frequency + ", Release Date: " + releaseDate + ", Circulation: " + circulation + ", Rating: " + Rating;
        }
    }
}
