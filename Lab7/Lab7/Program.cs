using Lab7;

internal class Program
{
    private static void Main(string[] args)
    {
        MagazineCollection collectionOne = new MagazineCollection();
        collectionOne.NameOfCollection = "Collection One";

        MagazineCollection collectionTwo = new MagazineCollection();
        collectionTwo.NameOfCollection = "Collection Two";

        Listener listenerOne = new Listener();
        Listener listenerTwo = new Listener();
        
        listenerOne.Subscribe(collectionOne);
        listenerTwo.Subscribe(collectionOne);
        listenerTwo.Subscribe(collectionTwo);
        
        collectionOne.AddDefaults();
        collectionTwo.AddMagazines(
            new Magazine("Forbes", Frequency.Monthly, new DateTime(2020, 1, 1), 1000),
            new Magazine("Time", Frequency.Weekly, new DateTime(2020, 1, 1), 500),
            new Magazine("National Geographic", Frequency.Monthly, new DateTime(2020, 1, 1), 1000));

        collectionOne.Replace(0, new Magazine("New Forbes", Frequency.Monthly, new DateTime(2020, 1, 1), 1000));
        collectionTwo.Replace(2, new Magazine("New Forbes", Frequency.Monthly, new DateTime(2020, 1, 1), 1000));

        try
        {
            collectionOne[2] = new Magazine("New Times", Frequency.Monthly, new DateTime(2020, 1, 1), 1000);
            collectionTwo[2] = new Magazine("New Times", Frequency.Monthly, new DateTime(2020, 1, 1), 1000);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        try
        {
            collectionOne[1] = new Magazine("New Times", Frequency.Monthly, new DateTime(2020, 1, 1), 1000);
            collectionTwo[1] = new Magazine("New Times", Frequency.Monthly, new DateTime(2020, 1, 1), 1000);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("\nListener one: \n" + listenerOne.ToString());
        Console.WriteLine("\nListener two: \n" + listenerTwo.ToString());
    }
}