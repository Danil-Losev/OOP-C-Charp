using Lab6;

internal class Program
{
    private static void Main(string[] args)
    {
        MagazineCollection magazineCollection = new MagazineCollection();
        magazineCollection.AddDefaults();
        magazineCollection.AddMagazines(new Magazine("Kaktus", Frequency.Monthly, new DateTime(2021, 1, 2), 100),
            new Magazine("Sputnik", Frequency.Weekly, new DateTime(2021, 1, 3), 2000),
            new Magazine("News", Frequency.Yearly, new DateTime(2021, 1, 4), 5000));

        for (int i = 0; i < magazineCollection.CollectionLenth; i++)
        {
            magazineCollection[i].AddArticles(new Article(new Person("Danil #" + i , "Losev #" + i, new DateTime(2005, 10, 16)), "Article #" + i, i % 5));
            magazineCollection[i].AddEditors(new Person("Danil #" + i, "Losev #" + i, new DateTime(2005, 10, 16)));
        }
        Console.WriteLine("Information about magazine collection\n" + magazineCollection.ToString());


        Console.WriteLine("Different sorts of collection\n");
        magazineCollection.SortByName();
        Console.WriteLine("Sort by name\n" + magazineCollection.ToShortString());
        magazineCollection.SortByCirculation();
        Console.WriteLine("Sort by circulation\n" + magazineCollection.ToShortString());
        magazineCollection.SortByReleaseDate();
        Console.WriteLine("Sort by release date\n" + magazineCollection.ToShortString());


        Console.WriteLine("Maximum rating: " + magazineCollection.MaxRating);

        Console.WriteLine("\nMagazines with frequency: monthly");
        foreach(Magazine magazine in magazineCollection.GetMonthlyMagazine)
        {
            Console.WriteLine(magazine.ToShortString());
        }

        List<Magazine> magazinesWhithratingMore0 = magazineCollection.RatingGroup(0);
        List<Magazine> magazinesWhithratingMore3 = magazineCollection.RatingGroup(3);
        List<Magazine> magazinesWhithratingMore5 = magazineCollection.RatingGroup(5);

        Console.WriteLine("\nMagazines with rating >= 0");
        foreach (Magazine magazine in magazinesWhithratingMore0)
        {
            Console.WriteLine(" "+magazine.ToShortString());
        }
        Console.WriteLine("\nMagazines with rating >= 3");
        foreach (Magazine magazine in magazinesWhithratingMore3)
        {
            Console.WriteLine(" " + magazine.ToShortString());
        }
        Console.WriteLine("\nMagazines with rating >= 5");
        foreach (Magazine magazine in magazinesWhithratingMore5)
        {
            Console.WriteLine(" " + magazine.ToShortString());
        }


        TestCollections testEditionAndMagazine = new TestCollections(2000);

        Console.WriteLine("\nSearching first element");
        testEditionAndMagazine.SearchTime(TestCollections.Positions.Start);

        Console.WriteLine("\nSearching middle element");
        testEditionAndMagazine.SearchTime(TestCollections.Positions.Middle);

        Console.WriteLine("\nSearching last element");
        testEditionAndMagazine.SearchTime(TestCollections.Positions.End);

        Console.WriteLine("\nSearching out of range element");
        testEditionAndMagazine.SearchTime(TestCollections.Positions.OutOfRange);
    }
}