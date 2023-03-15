using FsCheck;
using FsCheck.Xunit;

namespace Example01.Tests;

public class FizzBuzzKataTests 
{
    private const int MaximumNumber = 1000;
    
    [Property(Arbitrary = new[] { typeof(DivisibleByThreeNotByFiveDataGenerator) }, MaxTest = MaximumNumber)]
    public Property Given_Number_Divisible_By_Three_But_Not_Five_Then_Returns_Fizz(int number)
    {
        return (FizzBuzzKata.FizzBuzz(number) == "Fizz").ToProperty();
    }
    
    [Property(Arbitrary = new[] { typeof(DivisibleByFiveNotByThreeDataGenerator) }, MaxTest = MaximumNumber)]
    public Property Given_Number_Divisible_By_Five_But_Not_Three_Then_Returns_Buzz(int number)
    {
        return (FizzBuzzKata.FizzBuzz(number) == "Buzz").ToProperty();
    }
    
    [Property(Arbitrary = new[] { typeof(DivisibleByThreeAndFiveDataGenerator) }, MaxTest = MaximumNumber)]
    public Property Given_Number_Divisible_By_Three_And_Five_Then_Returns_FizzBuzz(int number)
    {
        return (FizzBuzzKata.FizzBuzz(number) == "FizzBuzz").ToProperty();
    }
    
    [Property(Arbitrary = new[] { typeof(NotDivisibleByThreeAndFiveDataGenerator) }, MaxTest = MaximumNumber)]
    public Property Given_Number_Not_Divisible_By_Three_And_Five_Then_Returns_Number(int number)
    {
        return (FizzBuzzKata.FizzBuzz(number) == number.ToString()).ToProperty();
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