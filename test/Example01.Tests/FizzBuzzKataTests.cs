using FsCheck;
using FsCheck.Xunit;

namespace Example01.Tests;

public class FizzBuzzKataTests 
{
    private const int MaximumNumber = 1000;
    
    [Property(Arbitrary = new[] { typeof(DivisibleByThreeNotByFiveDataGenerator) }, MaxTest = MaximumNumber)]
    public Property Given_Number_Divisible_By_Three_But_Not_Five_Then_Returns_Fizz(int number)
    {
        var property = () => FizzBuzzKata.FizzBuzz(number) == "Fizz";
        return property.ToProperty();
    }
    
    [Property(Arbitrary = new[] { typeof(DivisibleByFiveNotByThreeDataGenerator) }, MaxTest = MaximumNumber)]
    public Property Given_Number_Divisible_By_Five_But_Not_Three_Then_Returns_Buzz(int number)
    {
        var property = () => FizzBuzzKata.FizzBuzz(number) == "Buzz";
        return property.ToProperty();
    }
    
    [Property(Arbitrary = new[] { typeof(DivisibleByThreeAndFiveDataGenerator) }, MaxTest = MaximumNumber)]
    public Property Given_Number_Divisible_By_Three_And_Five_Then_Returns_FizzBuzz(int number)
    {
        var property = () => FizzBuzzKata.FizzBuzz(number) == "FizzBuzz";
        return property.ToProperty();
    }
    
    [Property(Arbitrary = new[] { typeof(NotDivisibleByThreeAndFiveDataGenerator) }, MaxTest = MaximumNumber)]
    public Property Given_Number_Not_Divisible_By_Three_And_Five_Then_Returns_Number(int number)
    {
        var property = () => FizzBuzzKata.FizzBuzz(number) == number.ToString();
        return property.ToProperty();
    }

    private static class DivisibleByThreeNotByFiveDataGenerator
    {
        public static Arbitrary<int> GenerateNumber()
        {
            return Arb.Default.Int32().Filter(x => x % 3 == 0 && x % 5 != 0);
        }
    }
    
    private static class DivisibleByFiveNotByThreeDataGenerator
    {
        public static Arbitrary<int> GenerateNumber()
        {
            return Arb.Default.Int32().Filter(x => x % 3 != 0 && x % 5 == 0);
        }
    }
    
    private static class DivisibleByThreeAndFiveDataGenerator
    {
        public static Arbitrary<int> GenerateNumber()
        {
            return Arb.Default.Int32().Filter(x => x % 3 == 0 && x % 5 == 0);
        }
    }
    
    private static class NotDivisibleByThreeAndFiveDataGenerator
    {
        public static Arbitrary<int> GenerateNumber()
        {
            return Arb.Default.Int32().Filter(x => x % 3 != 0 && x % 5 != 0);
        }
    }
}