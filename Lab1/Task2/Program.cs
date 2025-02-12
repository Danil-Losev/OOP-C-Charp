internal class Program
{
    private static void Main(string[] args)
    {
        int countOfElementes = 0;

        Console.Write("Enter the count of elements: ");
        countOfElementes = int.Parse(Console.ReadLine());

        int[] arrayOfNumbers = new int[countOfElementes];

        for (int i = arrayOfNumbers.GetLowerBound(0); i <= arrayOfNumbers.GetUpperBound(0); i++)
        {
            Console.Write("    Enter the element " + (i + 1) + ": ");
            arrayOfNumbers[i] = int.Parse(Console.ReadLine());
        }

        int[] descendingOrderSortedArray = arrayOfNumbers.OrderDescending().ToArray();

        Array taskArray = Array.CreateInstance(typeof(int), 10);

        for (int i = taskArray.GetLowerBound(0); i <= taskArray.GetUpperBound(0); i++)
        {
            taskArray.SetValue(descendingOrderSortedArray[i] + arrayOfNumbers[i], i);
        }

        Console.Write("Array after adding elements: ");
        foreach (int number in taskArray)
        {
            Console.Write(number + " ");
        }
    }
}