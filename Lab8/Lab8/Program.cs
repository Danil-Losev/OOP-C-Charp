namespace Lab8
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MagazineCollection<string> collectionOne = new MagazineCollection<string>();
            collectionOne.NameOfCollection = "Collection One";
            MagazineCollection<string> collectionTwo = new MagazineCollection<string>();
            collectionTwo.NameOfCollection = "Collection Two";

            Listener<string> listenerOne = new Listener<string>();
            listenerOne.Subscribe(collectionOne);
            listenerOne.Subscribe(collectionTwo);

            Magazine magazineOne = new Magazine("New old Forbes", Frequency.Monthly, new DateTime(2001, 1, 1), 1000);
            Magazine magazineTwo = new Magazine("New old Time", Frequency.Monthly, new DateTime(2001, 1, 1), 1000);

            collectionOne.AddDefaults();
            collectionTwo.AddDefaults();
            collectionOne.AddMagazines(magazineOne);
            collectionTwo.AddMagazines(magazineTwo);

            magazineOne.Circulation = 2000;
            magazineTwo.Circulation = 2000;
            magazineOne.ReleaseDate = new DateTime(2002, 1, 1);
            magazineTwo.ReleaseDate = new DateTime(2002, 1, 1);

            collectionTwo.Replace(magazineTwo, magazineOne);

            magazineTwo.Circulation = 3000;
            magazineOne.ReleaseDate = new DateTime(2003, 1, 1);

            Console.WriteLine("Listener info:\n" + listenerOne.ToString());
        }
    }
}