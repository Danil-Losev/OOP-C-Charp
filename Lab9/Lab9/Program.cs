using System.Xml.Serialization;

namespace Lab9
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Person Danil = new Person("Danil", "Losev", new DateTime(2005, 10, 16));

            Magazine Times = new Magazine("Times", Frequency.Weekly, new DateTime(2000, 1, 1), 1000,
                new List<Article>
                {
                    new Article(Danil, "Top 10 programming languages", 5),
                    new Article(Danil, "Top 5 programming languages", 5),
                },
                new List<Person> { Danil });
            Magazine TimesCopy = Times.DeepCopy();

            Console.WriteLine("Times:\n" + Times.ToString());
            Console.WriteLine("TimesCopy:\n" + TimesCopy.ToString());

            Console.ReadLine();

            string answer = "yes";
            while (answer == "yes")
            {
                Console.Clear();
                Console.WriteLine("Enter the file name");
                Console.Write("> ");
                string fileName = Console.ReadLine();
                FileInfo file = null;
                try
                {
                    file = new FileInfo(fileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    break;
                }

                if (!file.Exists)
                {
                    try
                    {
                        Console.WriteLine("File \"" + fileName + "\" doesn`t exists ");
                        Console.WriteLine("Creating file \"" + fileName + "\"");
                        FileStream createdFile = new FileStream(fileName, FileMode.Create);
                        createdFile.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error creating file: " + ex.Message);
                    }
                }
                else
                {
                    Times.Load(fileName);
                }

                Console.WriteLine("\nMagazine:\n" + Times.ToString());

                Times.AddFromConsole();
                Times.Save(fileName);
                Console.WriteLine("\nMagazine after adding:\n" + Times.ToString());

                Magazine.Load(fileName, ref Times);
                Times.AddFromConsole();
                Magazine.Save(fileName, Times);
                Console.WriteLine("\nMagazine after adding:\n" + Times.ToString());

                Console.WriteLine("Do you want to try again ? (yes/no)");
                Console.Write("> ");
                answer = Console.ReadLine();
            }
            Console.Clear();

            Times.BinarySerealization("DatTimes.dat");
            Console.WriteLine("Magazine after serialization:\n" + Times.ToString());

            Magazine TimesDeserialisation = new Magazine();
            TimesDeserialisation.BinaryDeserealization("DatTimes.dat");
            Console.WriteLine("\nMagazine after deserialization:\n" + TimesDeserialisation.ToString());

            Console.ReadLine();
            Console.Clear();

            Times.XmlSerialization("XmlTimes.xml");
            Console.WriteLine("Magazine after xml serialization:\n" + Times.ToString());
            Magazine TimesXmlDeserialization = new Magazine();
            TimesXmlDeserialization.XmlDeserealization("XmlTimes.xml");
            Console.WriteLine("\nMagazine after xml deserialization:\n" + TimesXmlDeserialization.ToString());

            Console.ReadLine();
            Console.Clear();

            Times.DataContractSerialization("DataTimes.xml");
            Console.WriteLine("Magazine after data contract serialization:\n" + Times.ToString());
            Magazine TimesDataContractDeserialization = new Magazine();
            TimesDataContractDeserialization.DataContractDeserialization("DataTimes.xml");
            Console.WriteLine("\nMagazine after data contract deserialization:\n" + TimesDataContractDeserialization.ToString());

            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Recursive directory check");
            RecursiveDirectoryChecker(Directory.GetCurrentDirectory());

        }

        public static void RecursiveDirectoryChecker(string path, int level = 0)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                for (int i = 0; i < level; i++)
                {
                    Console.Write("  ");
                }
                Console.WriteLine("File: " + file.Name);
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo directory in dirs)
            {
                for (int i = 0; i < level; i++)
                {
                    Console.Write("  ");
                }
                Console.WriteLine("Dir: " + directory.Name);
                RecursiveDirectoryChecker(directory.FullName, level + 1);
            }
        }

        public static void PersonSerialize()
        {
            FileStream fs = new FileStream("Person.xml", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(Person));
            Person person = new Person("Danil", "Losev", new DateTime(2005, 10, 16));
            xs.Serialize(fs, person);
        }

        public static void ArticleSerialize()
        {
            FileStream fs = new FileStream("Article.xml", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(Article));
            Article article = new Article(new Person("Danil", "Losev", new DateTime(2005, 10, 16)), "Top 10 programming languages", 5);
            xs.Serialize(fs, article);
        }

        public static void EditionSerialize()
        {
            FileStream fs = new FileStream("Edition.xml", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(Edition));
            Edition edition = new Edition("Times", new DateTime(2000, 1, 1), 1000);
            xs.Serialize(fs, edition);
        }

        public static void MagazineSerialize()
        {
            FileStream fs = new FileStream("Magazine.xml", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(Magazine));
            Magazine magazine = new Magazine("Times", Frequency.Weekly, new DateTime(2000, 1, 1), 1000,
                new List<Article>
                {
                    new Article(new Person("Danil", "Losev", new DateTime(2005, 10, 16)), "Top 10 programming languages", 5),
                    new Article(new Person("Danil", "Losev", new DateTime(2005, 10, 16)), "Top 5 programming languages", 5),
                },
                new List<Person> { new Person("Danil", "Losev", new DateTime(2005, 10, 16)) });
            xs.Serialize(fs, magazine);
        }

        public static void ArticleListSerialize()
        {
            try
            {
                FileStream fs = new FileStream("ArticleList.xml", FileMode.Create);
                XmlSerializer xs = new XmlSerializer(typeof(List<Article>));
                List<Article> articles = new List<Article>
            {
                new Article(new Person("Danil", "Losev", new DateTime(2005, 10, 16)), "Top 10 programming languages", 5),
                new Article(new Person("Danil", "Losev", new DateTime(2005, 10, 16)), "Top 5 programming languages", 5)
            };
                xs.Serialize(fs, articles);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}