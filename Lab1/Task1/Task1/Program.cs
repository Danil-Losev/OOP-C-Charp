
// Variant 6
internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Write the array of numbers: ");
        string stringOfNumbers = Console.ReadLine();

        stringOfNumbers = stringOfNumbers.TrimEnd('\n', '\r');

        int[] arrayOfNumbers = stringOfNumbers.Split(' ').Select(int.Parse).ToArray();
        int evenNumbers = 0;
        foreach (int number in arrayOfNumbers)
        {
            if (number % 2 == 0)
            {
                evenNumbers++;
            }
        }
        Console.Write("Your array: ");
        foreach (int number in arrayOfNumbers)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine("\nCount of even numbers is: " + evenNumbers);
    }
}