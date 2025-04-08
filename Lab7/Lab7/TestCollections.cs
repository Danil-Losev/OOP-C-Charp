using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    internal class TestCollections
    {
        private List<Edition> editions;
        private List<string> strings;
        private Dictionary<Edition, Magazine> dictionary;
        private Dictionary<string, Magazine> dictionaryString;

        static Magazine MagazineGenerator(int count)
        {
            List<Article> articles = new List<Article>(count);
            List<Person> editors = new List<Person>(count);
            for (int i = 0; i < count || i < 5; i++)
            {
                articles.Add(new Article(new Person("Danil " + i, "Losev " + i, new DateTime(2005, 10, 16)), "Article #" + i, 10 * i));
                editors.Add(new Person("Danil " + i, "Losev " + i, new DateTime(2005, 10, 16)));
            }
            return new Magazine("Magazine #" + count, Frequency.Monthly, new DateTime(2005, 10, 16), 10 * count, articles, editors);
        }

        public TestCollections(int count)
        {
            editions = new List<Edition>(count);
            strings = new List<string>(count);
            dictionary = new Dictionary<Edition, Magazine>(count);
            dictionaryString = new Dictionary<string, Magazine>(count);
            for (int i = 0; i < count; i++)
            {
                Magazine magazine = MagazineGenerator(i);
                editions.Add(magazine.Edition);
                strings.Add(editions[i].ToString());
                dictionary.Add(magazine.Edition, magazine);
                dictionaryString.Add(editions[i].ToString(), magazine);
            }

        }

        public enum Positions
        {
            Start,
            Middle,
            End,
            OutOfRange
        }

        public void SearchTime(Positions position)
        {

            int elementPosition = 0;
            Edition editionToFind = new Edition();
            string stringToFind = new string("");
            Magazine magazineToFind = new Magazine();
            Magazine magazineStringToFind = new Magazine();
            
            if (position != Positions.OutOfRange)
            {

                switch (position)
                {
                    case Positions.Start:
                        elementPosition = 0;
                        break;
                    case Positions.Middle:
                        elementPosition = editions.Count / 2;
                        break;
                    case Positions.End:
                        elementPosition = editions.Count - 1;
                        break;
                }
                editionToFind = editions[elementPosition];
                stringToFind = strings[elementPosition];
                magazineToFind = dictionary[editionToFind];
                magazineStringToFind = dictionaryString[stringToFind];
            }



            int start = 0;

            int[] searchTime = new int[6];

            start = Environment.TickCount;
            bool foundEdition = editions.Contains(editionToFind);
            searchTime[0] = Environment.TickCount - start;

            start = Environment.TickCount;
            bool foundString = strings.Contains(stringToFind);
            searchTime[1] = Environment.TickCount - start;

            start = Environment.TickCount;
            bool foundMagazine = dictionary.ContainsValue(magazineToFind);
            searchTime[2] = Environment.TickCount - start;

            start = Environment.TickCount;
            bool foundKeyMagazine = dictionary.ContainsKey(editionToFind);
            searchTime[3] = Environment.TickCount - start;

            start = Environment.TickCount;
            bool foundStringMagazine = dictionaryString.ContainsValue(magazineStringToFind);
            searchTime[4] = Environment.TickCount - start;

            start = Environment.TickCount;
            bool foundKeyStringMagazine = dictionaryString.ContainsKey(stringToFind);
            searchTime[5] = Environment.TickCount - start;

            if (foundEdition)
            {
                Console.WriteLine("\n Search time for Edition: " + searchTime[0]);
                Console.WriteLine(" Found Edition" + editions[elementPosition].ToString());
            }
            Console.Write(' ');
            if (foundString)
            {
                Console.WriteLine("\n Search time for String: " + searchTime[1]);
                Console.WriteLine(" Found String" + strings[elementPosition]);
            }
            Console.Write(' ');
            if (foundMagazine)
            {
                Console.WriteLine("\n Search time for Magazine: " + searchTime[2]);
                Console.WriteLine(" Found Magazine" + dictionary[editionToFind].ToShortString());
            }
            Console.Write(' ');
            if (foundKeyMagazine)
            {
                Console.WriteLine("\n Search time for Key Magazine: " + searchTime[3]);
                Console.WriteLine(" Found Key Magazine" + dictionary[editionToFind].ToShortString());
            }
            Console.Write(' ');
            if (foundStringMagazine)
            {
                Console.WriteLine("\n Search time for String Magazine: " + searchTime[4]);
                Console.WriteLine(" Found String Magazine" + dictionaryString[stringToFind].ToShortString());
            }
            Console.Write(' ');
            if (foundKeyStringMagazine)
            {
                Console.WriteLine("\n Search time for Key String Magazine: " + searchTime[5]);
                Console.WriteLine(" Found Key String Magazine" + dictionaryString[stringToFind].ToShortString());
            }
            else
            {
                Console.WriteLine("Element not found");
            }

        }


    }
}

