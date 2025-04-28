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

            Times.BinarySerealization("Times.dat");
            Console.WriteLine("Magazine after serialization:\n" + Times.ToString());
            
            Magazine TimesDeserialisation = new Magazine();
            TimesDeserialisation.BinaryDeserealization("Times.dat");
            Console.WriteLine("\nMagazine after deserialization:\n" + TimesDeserialisation.ToString());
      
            Console.ReadLine();
            Console.Clear();

            Times.XmlSerialization("Times.txt");
            Console.WriteLine("Magazine after xml serialization:\n" + Times.ToString());
            Magazine TimesXmlDeserialization = new Magazine();
            TimesXmlDeserialization.XmlDeserealization("Times.txt");
            Console.WriteLine("\nMagazine after xml deserialization:\n" + TimesXmlDeserialization.ToString());
            
            Console.ReadLine();
            Console.Clear();

            Times.DataContractSerialization("DataTimes.txt");
            Console.WriteLine("Magazine after data contract serialization:\n" + Times.ToString());
            Magazine TimesDataContractDeserialization = new Magazine();
            TimesDataContractDeserialization.DataContractDeserialization("DataTimes.txt");
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
    }
}