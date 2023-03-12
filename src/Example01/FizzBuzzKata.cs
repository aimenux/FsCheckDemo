namespace Example01;

public static class FizzBuzzKata
{
    public static string FizzBuzz(int number)
    {
        if (IsFizz(number) && IsBuzz(number)) return "FizzBuzz";
        if (IsFizz(number)) return "Fizz";
        if (IsBuzz(number)) return "Buzz";
        return number.ToString();
    }

    private static bool IsFizz(int number) => number % 3 == 0;
    
    private static bool IsBuzz(int number) => number % 5 == 0;
}