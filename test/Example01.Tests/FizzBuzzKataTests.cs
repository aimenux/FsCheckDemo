using FsCheck;
using FsCheck.Xunit;

namespace Example01.Tests;

public class FizzBuzzKataTests 
{
    [Property]
    public Property Given_Number_Divisible_By_Three_But_Not_Five_Then_Returns_Fizz(int number)
    {
        var property = () => FizzBuzzKata.FizzBuzz(number) == "Fizz";
        return property.When(number % 3 == 0 && number % 5 != 0);
    }
    
    [Property]
    public Property Given_Number_Divisible_By_Five_But_Not_Three_Then_Returns_Buzz(int number)
    {
        var property = () => FizzBuzzKata.FizzBuzz(number) == "Buzz";
        return property.When(number % 5 == 0 && number % 3 != 0);
    }
    
    [Property(Arbitrary = new[] { typeof(DataGenerator) })]
    public Property Given_Number_Divisible_By_Three_And_By_Five_Then_Returns_FizzBuzz(int number)
    {
        var property = () => FizzBuzzKata.FizzBuzz(number) == "FizzBuzz";
        return property.ToProperty();
    }
    
    [Property]
    public Property Given_Number_Not_Divisible_By_Three_And_Five_Then_Returns_Number(int number)
    {
        var property = () => FizzBuzzKata.FizzBuzz(number) == number.ToString();
        return property.When(number % 3 != 0 && number % 5 != 0);
    }
    
    private static class DataGenerator
    {
        public static Arbitrary<int> GenerateNumber()
        {
            return Arb.Default.Int32().Filter(x => x % 3 == 0 && x % 5 == 0);
        }
    }
}