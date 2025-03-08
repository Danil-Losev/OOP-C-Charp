

using System.Collections;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Edition firstEdition = new Edition("Edition", new DateTime(2025, 01, 01), 1000);
            Edition secondEdition = new Edition("Edition", new DateTime(2025, 01, 01), 1000);

            Console.WriteLine("Comparing the reference of to objects: " + ReferenceEquals(firstEdition, secondEdition));
            Console.WriteLine("Comparing the values of two objects: " + firstEdition.Equals(secondEdition));
            Console.WriteLine("Hash code of the first object: " + firstEdition.GetHashCode());
            Console.WriteLine("Hash code of the second object: " + secondEdition.GetHashCode());

            try { firstEdition.Circulation = -1; }
            catch (Exception exception) { Console.WriteLine(exception.Message); }

            Magazine firstMagazine = new Magazine("Times", Frequency.Monthly, new DateTime(2025, 01, 01), 1000);
            firstMagazine.AddArticles(new Article(new Person("Danil", "Losev", new DateTime(2025, 01, 01)), "First article", 100),
                new Article(new Person("Danil", "Losev", new DateTime(2025, 01, 01)), "Second article", 200));
            firstMagazine.AddEditors(new Person("Danil", "Losev", new DateTime(2025, 01, 01)),
                new Person("Arthyom", "Losev", new DateTime(2025, 01, 01)),
                new Person("Steve", "Jobes", new DateTime(2025, 01, 01)));

            Console.WriteLine("\n\nMagazine info:\n" + firstMagazine);

            Console.WriteLine("\nEdition property of Magazine: " + firstMagazine.Edition);

            Magazine secondMagazine = (Magazine)firstMagazine.DeepCopy();

            firstMagazine.Name = "New Times";
            firstMagazine.Frequency = Frequency.Weekly;
            Console.WriteLine("\n\nPrinting changed sourse:\n");
            Console.WriteLine(firstMagazine);

            Console.WriteLine("\nPrinting copy of sourse before changing:\n");
            Console.WriteLine(secondMagazine);

            firstMagazine.AddArticles(new Article(new Person("Danil", "Losev", new DateTime(2025, 01, 01)), "Third article", 300),
                new Article(new Person("Arthyom", "Losev", new DateTime(2025, 01, 01)), "Third article", 400),
                new Article(new Person("Bill", "Gates", new DateTime(2025, 01, 01)), "Fifth article", 500));

            Console.WriteLine("\n\nArticles with rating more than 100");
            foreach (Article article in firstMagazine.GetArticlesWithMoreRating(100))
            {
                Console.WriteLine("    " + article);
            }

            Console.WriteLine("\nArticles with title: \"Third article\"");
            foreach (Article article in firstMagazine.GetArticlesWithSameTitle("Third article"))
            {
                Console.WriteLine("    " + article);
            }

            Console.WriteLine("\nArticles with authors who are not a editors");
            foreach (Article article in firstMagazine)
            {
                Console.WriteLine(article);
            }

            Console.WriteLine("\nArticles with authors who are editors");
            foreach (Article article in firstMagazine.GetArticlesOfEditors())
            {
                Console.WriteLine(article);
            }

            Console.WriteLine("\nEdotors without articles");
            foreach (Person editor in firstMagazine.GetEditorsWithoutArticles())
            {
                Console.WriteLine(editor);
            }
        }
    }
}