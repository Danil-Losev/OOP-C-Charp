
using System.Collections;

namespace Lab6
{
    internal class Magazine : Edition, IRateAndCopy, IEnumerable
    {
        private Frequency frequency;
        private List<Article> articles;
        private List<Person> editors;

        public Frequency Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        public List<Article> Articles
        {
            get { return articles; }
            set { articles = value; }
        }
        public List<Person> Editors
        {
            get { return editors; }
            set { editors = value; }
        }

        public Edition Edition
        {
            get
            {
                return new Edition(Name, ReleaseDate, Circulation);
            }
            set
            {
                Name = value.Name;
                ReleaseDate = value.ReleaseDate;
                Circulation = value.Circulation;
            }
        }

        public Magazine()
        {
            Name = "N/A";
            Frequency = Frequency.Monthly;
            ReleaseDate = DateTime.Now;
            Circulation = 0;
            Articles = new List<Article>();
            Editors = new List<Person>();
        }

        public Magazine(string name, Frequency frequency, System.DateTime releaseDate, int circulation, List<Article> articles = null, List<Person> editors = null)
        {
            Name = name;
            Frequency = frequency;
            ReleaseDate = releaseDate;
            Circulation = circulation;
            if (articles == null)
            {
                Articles = new List<Article>();
            }
            else
            {
                Articles = articles;
            }
            if (editors == null)
            {
                Editors = new List<Person>();
            }
            else
            {
                Editors = editors;
            }
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
                if (articles.Count == 0)
                {
                    return 0;
                }
                return rating / articles.Count;
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
            if (Articles == null)
            {
                Articles = new List<Article>();
                Articles.AddRange(articles);
            }
            else
            {
                Articles.AddRange(articles);
            }
        }

        public void AddEditors(params Person[] editors)
        {
            if (Editors == null)
            {
                Editors = new List<Person>();
                Editors.AddRange(editors);
            }
            else
            {
                Editors.AddRange(editors);
            }
        }

        public override string ToString()
        {
            string articlesList = "\n";
            foreach (Article article in Articles)
            {
                articlesList += "   " + article.ToString() + "\n";
            }
            string editorsList = "\n";
            foreach (Person person in Editors)
            {
                editorsList += "    " + person.ToString() + "\n";
            }
            return "Name: " + Name + ", Frequency: " + Frequency + ", Release Date: " + ReleaseDate + ", Circulation: " + Circulation + ", \nArticles:" + articlesList + "Editors:" + editorsList;
        }

        public virtual string ToShortString()
        {
            return "Name: " + name + ", Frequency: " + frequency + ", Release Date: " + releaseDate + ", Circulation: " + circulation + ", Rating: " + Rating;
        }

        // Lab 5

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Magazine magazine = (Magazine)obj;
            return (Name == magazine.Name && Frequency == magazine.Frequency && ReleaseDate == magazine.ReleaseDate && Circulation == magazine.Circulation && Editors == magazine.Editors && Articles == magazine.Articles);
        }

        public static bool operator ==(Magazine magazine1, Magazine magazine2)
        {

            if (ReferenceEquals(magazine1, magazine2)) return true;
            if ((object)magazine1 == null || (object)magazine2 == null) return false;
            return magazine1.Name == magazine2.Name && magazine1.Frequency == magazine2.Frequency && magazine1.ReleaseDate == magazine2.ReleaseDate && magazine1.Circulation == magazine2.Circulation && magazine1.Editors == magazine2.Editors && magazine1.Articles == magazine2.Articles;
        }

        public static bool operator !=(Magazine magazine1, Magazine magazine2)
        {
            if ((object)magazine1 == null || (object)magazine2 == null) return true;
            return magazine1.Name != magazine2.Name || magazine1.Frequency != magazine2.Frequency || magazine1.ReleaseDate != magazine2.ReleaseDate || magazine1.Circulation != magazine2.Circulation || magazine1.Editors != magazine2.Editors || magazine1.Articles != magazine2.Articles;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Frequency.GetHashCode() + ReleaseDate.GetHashCode() + Circulation.GetHashCode() + Articles.GetHashCode() + Editors.GetHashCode();
        }

        public object DeepCopy()
        {
            List<Article> articlesCopy = new List<Article>();
            foreach (Article article in Articles)
            {
                articlesCopy.Add((Article)article.DeepCopy());
            }
            List<Person> editorsCopy = new List<Person>();
            foreach (Person person in Editors)
            {
                editorsCopy.Add((Person)person.DeepCopy());
            }
            return new Magazine(Name, Frequency, ReleaseDate, Circulation, articlesCopy, editorsCopy);
        }

        object IRateAndCopy.DeepCopy()
        {
            return DeepCopy();
        }

        double IRateAndCopy.Rating
        {
            get { return Rating; }
        }

        public IEnumerable<Article> GetArticlesWithMoreRating(double rating)
        {
            foreach (Article article in Articles)
            {
                if (article.Rating > rating)
                {
                    yield return article;
                }
            }
        }

        public IEnumerable<Article> GetArticlesWithSameTitle(string title)
        {
            foreach (Article article in Articles)
            {
                if (article.Title == title)
                {
                    yield return article;
                }
            }
        }

        public IEnumerable<Article> GetArticlesOfEditors()
        {
            foreach (Article article in Articles)
            {
                if (Editors.Contains(article.Author))
                {
                    yield return article;
                }
            }
        }

        public IEnumerable<Person> GetEditorsWithoutArticles()
        {
            foreach (Person editor in Editors)
            {

                bool personHasArticle = false;
                foreach (Article article in Articles)
                {
                    if (editor == article.Author)
                    {
                        personHasArticle = true;
                        break;
                    }
                }
                if (!personHasArticle)
                {
                    yield return editor;
                }
            }
        }



        public IEnumerator GetEnumerator()
        {
            return new EnumeratorForMagazine(this);
        }



    }

    file class EnumeratorForMagazine : IEnumerator
    {
        private Magazine thisMagazine;
        private int index = -1;

        public Magazine ThisMagazine
        {
            get { return thisMagazine; }
            set { thisMagazine = value; }
        }
        public EnumeratorForMagazine(Magazine magazine)
        {
            ThisMagazine = magazine;
        }

        public bool MoveNext()
        {
            while (++index < ThisMagazine.Articles.Count)
            {
                Article curArticle = (Article)ThisMagazine.Articles[index];
                if (!thisMagazine.Editors.Contains(curArticle.Author))
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset() { index = -1; }
        public object Current
        {
            get
            {
                if (index < 0 || index >= ThisMagazine.Articles.Count)
                {
                    return new Article();
                }
                return ThisMagazine.Articles[index];
            }

        }
    }
}
