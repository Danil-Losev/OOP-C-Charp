
namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
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

            Person[] oneDemPersonArray = new Person[rows * columns];
            for (int i = 0; i < (rows * columns); i++)
            {
                oneDemPersonArray[i] = new Person();
            }

            Person[,] twoDemPersonArray = new Person[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    twoDemPersonArray[i, j] = new Person();
                }
            }

            Person[][] steppedPersonArray = new Person[rows][];
            for (int i = 0; i < rows; i++)
            {
                steppedPersonArray[i] = new Person[columns];
                for (int j = 0; j < columns; j++)
                {
                    steppedPersonArray[i][j] = new Person();
                }
            }

            start = Environment.TickCount;
            for (int i = 0; i < (rows * columns); i++)
            {
                oneDemPersonArray[i].BirthDate = new DateTime(2000, 1, 1);
            }
            end = Environment.TickCount;
            timeOfExecution[0] = end - start;

            start = Environment.TickCount;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    twoDemPersonArray[i, j].BirthDate = new DateTime(2000, 1, 1);
                }
            }
            end = Environment.TickCount;
            timeOfExecution[1] = end - start;

            start = Environment.TickCount;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    steppedPersonArray[i][j].BirthDate = new DateTime(2000, 1, 1);
                }
            }
            end = Environment.TickCount;
            timeOfExecution[2] = end - start;

            Console.WriteLine("Count of elements: " + rows * columns + " (count of rows: " + rows + " , count of columns: " + columns + ')');
            Console.WriteLine("Timw of work with one dimensional array: " + timeOfExecution[0] + " ms");
            Console.WriteLine("Timw of work with two dimensional array: " + timeOfExecution[1] + " ms");
            Console.WriteLine("Timw of work with stepped array: " + timeOfExecution[2] +  "ms");
        }
    }
}