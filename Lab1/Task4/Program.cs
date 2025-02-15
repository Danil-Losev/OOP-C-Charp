internal class Program
{
    private static void Main(string[] args)
    {
        int[] myArray = { 1, 2, 3, -1, 5, 6, 11, 8, 9, 10 };
        
        Console.WriteLine("Out method:");
        int outMax, outMin, outMid;
        outMaxMinMid(myArray, out outMax, out outMin, out outMid);
        Console.WriteLine("Max out: " + outMax + '\n' + "Min out: " + outMin + '\n' + "Mid out: " + outMid);

        Console.WriteLine("\nVar method:");
        var MaxMinMidTuple = varMaxMinMid(myArray);
        Console.WriteLine("Max var: " + MaxMinMidTuple.Item1 + '\n' + "Min var: " + MaxMinMidTuple.Item2 + '\n' + "Mid var: " + MaxMinMidTuple.Item3);
    }
    private static void outMaxMinMid(int[] inputArray, out int maxElement, out int minElement, out int midElement)
    {
        int[] tempArray = inputArray.Where(x => x>0).ToArray();
        maxElement = tempArray.Max();
        minElement = tempArray.Min();
        midElement = Convert.ToInt32(tempArray.Average());
    }
    private static (int, int, int) varMaxMinMid(int[] inputArray)
    {
        int[] tempArray = inputArray.Where(x => x > 0).ToArray();
        var outputVar = (tempArray.Max(), tempArray.Min(), Convert.ToInt32(tempArray.Average()));
        return outputVar;
    }
}