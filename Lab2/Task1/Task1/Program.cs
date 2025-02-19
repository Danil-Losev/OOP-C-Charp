internal class Program
{
    private static void Main(string[] args)
    {
        string name = "Данил";
        int result = ConvertStringToInt(name);
        Console.WriteLine("Res: " + result);
    }
    private static int ConvertStringToInt(string inputString)
    {
        char[] inputCharArray = inputString.ToCharArray();
        int result = 0;
        List<char> usedChars = new List<char>();
        foreach (char ch in inputCharArray)
        {
            if (!usedChars.Contains(ch))
            {
                result += (int)Math.Pow((int)ch, getCountOfLetters(inputCharArray, ch));
                usedChars.Add(ch);
            }
        }
        return result % 8;
    }
    private static int getCountOfLetters(char[] inputCharArray, char ch)
    {
        int count = 0;
        foreach (char c in inputCharArray)
        {
            if (c == ch)
            {
                count++;
            }
        }
        return count;
    }
}
