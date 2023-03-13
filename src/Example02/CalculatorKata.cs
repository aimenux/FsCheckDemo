namespace Example02;

public static class CalculatorKata
{
    public const int MaximumNumber = 1000;
    
    public static int Compute(string delimiter, string input)
    {
        var numbers = input
            .Split(delimiter)
            .Where(x => x.IsInteger())
            .Select(int.Parse)
            .ToList();

        if (numbers.Any(x => x < 0))
        {
            throw new NegativeNumbersException();
        }

        return numbers
            .Where(x => x <= MaximumNumber)
            .Sum();
    }
}

public class NegativeNumbersException : Exception
{
    public NegativeNumbersException() : base("Negative numbers are not supported")
    {
    }
}

public static class Extensions
{
    public static bool IsInteger(this string input) => int.TryParse(input, out var _);
}