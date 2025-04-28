
using System.Collections;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Lab9
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Lab9")]
    [Serializable]
    [XmlType("Magazine")]
    public class Magazine : Edition, IRateAndCopy, IEnumerable
    {
        private Frequency frequency;
        private List<Article> articles;
        private List<Person> editors;

        [XmlAttribute]
        [DataMember]
        public Frequency Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        [DataMember]
        [XmlArray("Articles")]
        [XmlArrayItem("Article")]
        public List<Article> Articles
        {
            get { return articles; }
            set { articles = value; }
        }

        [DataMember]
        [XmlArray("Editors")]
        [XmlArrayItem("Editor")]
        public List<Person> Editors
        {
            get { return editors; }
            set { editors = value; }
        }

        [XmlIgnore]
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
            ReleaseDate = new DateTime();
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
        [XmlIgnore]
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
            return "Name: " + Name + ", Frequency: " + Frequency + ", Release Date: " + ReleaseDate.ToShortDateString() + ", Circulation: " + Circulation + ", \nArticles:" + articlesList + "Editors:" + editorsList;
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


        object IRateAndCopy.DeepCopy()
        {
            return DeepCopy();
        }
        [XmlIgnore]
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

        // Lab 9
        public Magazine DeepCopy()
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

        public bool Save(string fileName)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName);
                writer.WriteLine(Name);
                writer.WriteLine(Frequency);
                writer.WriteLine(ReleaseDate);
                writer.WriteLine(Circulation);
                foreach (Article article in Articles)
                {
                    writer.WriteLine(article.ToString());
                }
                foreach (Person person in Editors)
                {
                    writer.WriteLine(person.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving magazine: " + ex.Message);
                return false;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public bool Load(string fileName)
        {
            Magazine oldMagazine = DeepCopy();
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(fileName);
                Name = reader.ReadLine();
                Frequency = (Frequency)Enum.Parse(Frequency.GetType(), reader.ReadLine());
                ReleaseDate = DateTime.Parse(reader.ReadLine());
                Circulation = int.Parse(reader.ReadLine());
                Articles.Clear();
                Editors.Clear();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Author:"))
                    {
                        string[] partsOfline = line.Split(new string[] { "Author: ( Name: ", ", Surname: ", ", Year of birth: ", " ), Title: ", ", Rating: " }, StringSplitOptions.None);
                        Person author = new Person(partsOfline[1], partsOfline[2], DateTime.Parse(partsOfline[3]));
                        Articles.Add(new Article(author, partsOfline[4], double.Parse(partsOfline[5])));
                    }
                    else
                    {
                        string[] partsOfline = line.Split(new string[] { "Name: ", ", Surname: ", ", Year of birth: " }, StringSplitOptions.None);
                        Person editor = new Person(partsOfline[1], partsOfline[2], DateTime.Parse(partsOfline[3]));
                        Editors.Add(editor);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading magazine: " + ex.Message);
                Name = oldMagazine.Name;
                Frequency = oldMagazine.Frequency;
                ReleaseDate = oldMagazine.ReleaseDate;
                Circulation = oldMagazine.Circulation;

                Articles.Clear();
                Editors.Clear();

                foreach (Article article in oldMagazine.Articles)
                {
                    Articles.Add((Article)article.DeepCopy());
                }

                foreach (Person person in oldMagazine.Editors)
                {
                    Editors.Add((Person)person.DeepCopy());
                }

                return false;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }


        public static bool Save(string fileName, Magazine magazine)
        {
            StreamWriter writer = default;
            try
            {
                writer = new StreamWriter(fileName);
                writer.WriteLine(magazine.Name);
                writer.WriteLine(magazine.Frequency);
                writer.WriteLine(magazine.ReleaseDate);
                writer.WriteLine(magazine.Circulation);
                foreach (Article article in magazine.Articles)
                {
                    writer.WriteLine(article.ToString());
                }
                foreach (Person person in magazine.Editors)
                {
                    writer.WriteLine(person.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving magazine: " + ex.Message);
                return false;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public static bool Load(string fileName, ref Magazine magazine)
        {
            Magazine oldMagazine = (Magazine)magazine.DeepCopy();
            magazine = new Magazine();
            StreamReader reader = default;
            try
            {
                reader = new StreamReader(fileName);
                magazine.Name = reader.ReadLine();
                magazine.Frequency = (Frequency)Enum.Parse(magazine.Frequency.GetType(), reader.ReadLine());
                magazine.ReleaseDate = DateTime.Parse(reader.ReadLine());
                magazine.Circulation = int.Parse(reader.ReadLine());
                magazine.Articles.Clear();
                magazine.Editors.Clear();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Author:"))
                    {
                        string[] partsOfline = line.Split(new string[] { "Author: ( Name: ", ", Surname: ", ", Year of birth: ", " ), Title: ", ", Rating: " }, StringSplitOptions.None);
                        Person author = new Person(partsOfline[1], partsOfline[2], DateTime.Parse(partsOfline[3]));
                        magazine.Articles.Add(new Article(author, partsOfline[4], double.Parse(partsOfline[5])));
                    }
                    else
                    {
                        string[] partsOfline = line.Split(new string[] { "Name: ", ", Surname: ", ", Year of birth: " }, StringSplitOptions.None);
                        Person editor = new Person(partsOfline[1], partsOfline[2], DateTime.Parse(partsOfline[3]));
                        magazine.Editors.Add(editor);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading magazine: " + ex.Message);
                magazine = (Magazine)oldMagazine.DeepCopy();
                return false;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        public bool AddFromConsole()
        {
            Console.WriteLine("Now you are adding a new article to the list");
            Console.WriteLine("Enter the article`s info in a raw ");
            Console.WriteLine("The style: Title of article, Rating of article, Name of creator, Surname of creator, Birth date (yyyy.mm.dd) ");
            Console.WriteLine("The feilds must be devided by \" \", \", \", \",\"");
            Console.Write("> ");
            string input = Console.ReadLine();
            string[] partsOfInput = input.Split(new string[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (partsOfInput.Length != 5)
            {
                Console.WriteLine("Invalid input. Please try again.");
                return false;
            }
            Article article = null;
            try
            {
                string title = partsOfInput[0];
                double rating = double.Parse(partsOfInput[1]);
                string name = partsOfInput[2];
                string surname = partsOfInput[3];
                DateTime birthDate = DateTime.Parse(partsOfInput[4]);
                Person author = new Person(name, surname, birthDate);
                article = new Article(author, title, rating);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding article: " + ex.Message);
                return false;
            }
            if (Articles == null)
            {
                Console.WriteLine("Error adding article");
                return false;
            }
            Articles.Add(article);
            Console.WriteLine("Article added successfully.");
            return true;
        }

        public bool BinarySerealization(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Create);
#pragma warning disable SYSLIB0011 // Тип или член устарел
                formatter.Serialize(stream, this);
#pragma warning restore SYSLIB0011 // Тип или член устарел
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error serializing magazine: " + ex.Message);
                return false;
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }

        public bool BinaryDeserealization(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
#pragma warning disable SYSLIB0011 // Тип или член устарел
                Magazine magazine = (Magazine)formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // Тип или член устарел
                Name = magazine.Name;
                Frequency = magazine.Frequency;
                ReleaseDate = magazine.ReleaseDate;
                Circulation = magazine.Circulation;
                Articles.Clear();
                Editors.Clear();
                foreach (Article article in magazine.Articles)
                {
                    Articles.Add((Article)article.DeepCopy());
                }
                foreach (Person person in magazine.Editors)
                {
                    Editors.Add((Person)person.DeepCopy());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening file: " + ex.Message);
                return false;
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }


        public void Add(object item)
        {
            if (item is Article article)
            {
                Articles.Add(article);
            }
            else if (item is Person editor)
            {
                Editors.Add(editor);
            }
            else
            {
                throw new ArgumentException("Only objects of type Article or Person can be added to the Magazine.");
            }
        }

        public bool XmlSerialization(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Magazine));
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                xmlSerializer.Serialize(stream, this);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error serializing magazine: " + ex.Message);
                return false;
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }


        public bool XmlDeserealization(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Magazine));
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Open);
                Magazine magazine = (Magazine)xmlSerializer.Deserialize(stream);
                Name = magazine.Name;
                Frequency = magazine.Frequency;
                ReleaseDate = magazine.ReleaseDate;
                Circulation = magazine.Circulation;
                Articles.Clear();
                Editors.Clear();
                foreach (Article article in magazine.Articles)
                {
                    Articles.Add((Article)article.DeepCopy());
                }
                foreach (Person person in magazine.Editors)
                {
                    Editors.Add((Person)person.DeepCopy());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error serializing magazine: " + ex.Message);
                return false;
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }

        public bool DataContractSerialization(string fileName)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Magazine));
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                serializer.WriteObject(stream, this);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error serializing magazine: " + ex.Message);
                return false;
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }

        public bool DataContractDeserialization(string fileName)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Magazine));
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Open);
                Magazine magazine = (Magazine)serializer.ReadObject(stream);
                Name = magazine.Name;
                Frequency = magazine.Frequency;
                ReleaseDate = magazine.ReleaseDate;
                Circulation = magazine.Circulation;
                Articles.Clear();
                Editors.Clear();
                foreach (Article article in magazine.Articles)
                {
                    Articles.Add((Article)article.DeepCopy());
                }
                foreach (Person person in magazine.Editors)
                {
                    Editors.Add((Person)person.DeepCopy());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error serializing magazine: " + ex.Message);
                return false;
            }
            finally
            {
                if (stream != null) stream.Close();
            }
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