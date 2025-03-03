
using System.Globalization;
using Task1;

// Вариант 2

namespace Task2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Magazine magazine = new Magazine();
            string magazineShortInfo = magazine.ToShortString();
            Console.WriteLine(magazineShortInfo);
            
            Console.WriteLine("Weekly: " + magazine[Frequency.Weekly]);
            Console.WriteLine("Monthly: " + magazine[Frequency.Monthly]);
            Console.WriteLine("Yearly: " + magazine[Frequency.Yearly]);

            magazine.Name = "Forbes";
            magazine.Frequency = Frequency.Monthly;
            magazine.ReleaseDate = new System.DateTime(2020, 1, 1);
            magazine.Circulation = 1000;
            Article[] articles = { new Article(new Person("Steve", "Jobs", System.DateTime.Now), "How to become a billionaire", 10.0), new Article(new Person("Steve", "Jobs", System.DateTime.Now), "How to become a millionaire", 5.0) };
            magazine.Articles = articles;
            
            magazine.AddArticles(new Article( new Person("Steve", "Jobs", System.DateTime.Now), "How to become a billionaire", 10.0), new Article(new Person("Steve", "Jobs", System.DateTime.Now), "How to become a millionaire", 5.0));
            
            Console.WriteLine( "\n" + magazine);

            int start = 0;
            int end = 0;
            int[] timeOfExecution = { 0, 0, 0 };

            Console.WriteLine("Enter the number of rows and columns (separated with: space, comma, semicolon ): ");
            Console.Write("> ");
            string input = Console.ReadLine();
            char[] separators = { ' ', ',', ';' };
            string[] sizes = input.Split(separators);
            int rows = int.Parse(sizes[0]);
            int columns = int.Parse(sizes[1]);

            Article[] oneDemArticleArray = new Article[rows * columns];
            for (int i = 0; i < (rows * columns); i++)
            {
                oneDemArticleArray[i] = new Article();
            }
            Article[,] twoDemArticleArray = new Article[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    twoDemArticleArray[i, j] = new Article();
                }
            }
            Article[][] steppedArticleArray = new Article[rows][];
            for (int i = 0; i < rows; i++)
            {
                steppedArticleArray[i] = new Article[columns];
                for (int j = 0; j < columns; j++)
                {
                    steppedArticleArray[i][j] = new Article();
                }
            }

            start = Environment.TickCount;
            for (int i = 0; i < (rows * columns); i++)
            {
                oneDemArticleArray[i].Rating = 10.0;
            }
            end = Environment.TickCount;
            timeOfExecution[0] = end - start;

            start = Environment.TickCount;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    twoDemArticleArray[i, j].Rating = 10.0;
                }
            }
            end = Environment.TickCount;
            timeOfExecution[1] = end - start;

            start = Environment.TickCount;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    steppedArticleArray[i][j].Rating = 10.0;
                }
            }
            end = Environment.TickCount;
            timeOfExecution[2] = end - start;

            Console.WriteLine("Count of elements: " + rows * columns + " (count of rows: " + rows + " , count of columns: " + columns + ')');
            Console.WriteLine("One-dimensional array: " + timeOfExecution[0]);
            Console.WriteLine("Two-dimensional array: " + timeOfExecution[1]);
            Console.WriteLine("Stepped array: " + timeOfExecution[2]);



        }
    }
}