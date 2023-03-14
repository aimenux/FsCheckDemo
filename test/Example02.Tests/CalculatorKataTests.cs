using FsCheck;

namespace Example02.Tests;

public class CalculatorKataTests
{
    private const int MaxNbrOfTests = 1000;
    
    [Fact]
    public void Given_Positive_Numbers_Separated_By_Delimiter_Then_Returns_Sum()
    {
        Prop.ForAll(DataGenerator.GeneratePositiveNumbers(), parameter =>
        {
            var values = parameter.values;
            var delimiter = parameter.delimiter;
            var input = string.Join(delimiter, values);
            var expectedResult = values.Where(x => x <= CalculatorKata.MaximumNumber).Sum();
            var result = CalculatorKata.Compute(delimiter, input);
            return result == expectedResult;
        }).QuickCheckThrowOnFailure(MaxNbrOfTests);
    }
    
    [Fact]
    public void Given_Bigger_Numbers_Separated_By_Delimiter_Then_Returns_Zero()
    {
        Prop.ForAll(DataGenerator.GenerateBiggerNumbers(), parameter =>
        {
            var values = parameter.values;
            var delimiter = parameter.delimiter;
            var input = string.Join(delimiter, values);
            var result = CalculatorKata.Compute(delimiter, input);
            return result == 0;
        }).QuickCheckThrowOnFailure(MaxNbrOfTests);
    }
    
    [Fact]
    public void Given_Negative_Numbers_Separated_By_Delimiter_Then_Throw_Exception()
    {
        Prop.ForAll(DataGenerator.GenerateNegativeNumbers(), parameter =>
        {
            var values = parameter.values;
            var delimiter = parameter.delimiter;
            var input = string.Join(delimiter, values);
            Assert.Throws<NegativeNumbersException>(() => CalculatorKata.Compute(delimiter, input));
        }).QuickCheckThrowOnFailure(MaxNbrOfTests);
    }
    
    private static class DataGenerator
    {
        public static Arbitrary<(int[] values, string delimiter)> GenerateBiggerNumbers()
        {
            var input = from values in Gen.ArrayOf(Gen.Constant(10 * CalculatorKata.MaximumNumber))
                from delimiter in Arb.Generate<NonEmptyString>().Where(IsValidDelimiter)
                select (values, delimiter.Get);
            return input.ToArbitrary();
        }
        
        public static Arbitrary<(int[] values, string delimiter)> GeneratePositiveNumbers()
        {
            var input = from values in Arb.Generate<PositiveInt[]>().Where(x => x.Any())
                from delimiter in Arb.Generate<NonEmptyString>().Where(IsValidDelimiter)
                select (values.Select(x => x.Get).ToArray(), delimiter.Get);
            return input.ToArbitrary();
        }
        
        public static Arbitrary<(int[] values, string delimiter)> GenerateNegativeNumbers()
        {
            var input = from values in Arb.Generate<NegativeInt[]>().Where(x => x.Any())
                from delimiter in Arb.Generate<NonEmptyString>().Where(IsValidDelimiter)
                select (values.Select(x => x.Get).ToArray(), delimiter.Get);
            return input.ToArbitrary();
        }

        private static bool IsValidDelimiter(NonEmptyString input)
        {
            return !input.Item.IsInteger() && input.Item != "-";
        }
    }
}