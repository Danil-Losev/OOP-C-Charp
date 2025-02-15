internal class Program
{
    private static void Main(string[] args)
    {
        int[] myArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] result = removeElementsFromArray(myArray, [ 1, 3, 5, 7, 9 ]);
        Console.WriteLine("Your output array is " + String.Join(" ", result));
        Console.ReadLine();
    }

    private static int[] removeElementsFromArray(int[] inputArray, int[] elements)
    {
        List<int> outputArray = new List<int>();

        foreach (int el in inputArray)
        {
            if (!elements.Contains(el))
            {
                outputArray.Add(el);
            }
        }

        return outputArray.ToArray();
    }
}
