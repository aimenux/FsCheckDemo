using FsCheck;

namespace Example02.Tests;

public class CalculatorKataTests
{
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
        }).QuickCheckThrowOnFailure();
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
        }).QuickCheckThrowOnFailure();
    }
    
    [Fact]
    public void Given_Positive_Numbers_Separated_By_Delimiter_Then_Throw_Exception()
    {
        Prop.ForAll(DataGenerator.GenerateNegativeNumbers(), parameter =>
        {
            var values = parameter.values;
            var delimiter = parameter.delimiter;
            var input = string.Join(delimiter, values);
            Assert.Throws<NegativeNumbersException>(() => CalculatorKata.Compute(delimiter, input));
        }).QuickCheckThrowOnFailure();
    }
    
    private static class DataGenerator
    {
        public static Arbitrary<(int[] values, string delimiter)> GenerateBiggerNumbers()
        {
            var input = from values in Gen.ArrayOf(Gen.Constant(10 * CalculatorKata.MaximumNumber))
                from delimiter in Arb.Generate<NonEmptyString>().Where(x => !x.Item.IsInteger())
                select (values, delimiter.Get);
            return input.ToArbitrary();
        }
        
        public static Arbitrary<(int[] values, string delimiter)> GeneratePositiveNumbers()
        {
            var input = from values in Arb.Generate<PositiveInt[]>().Where(x => x.Any())
                from delimiter in Arb.Generate<NonEmptyString>().Where(x => !x.Item.IsInteger())
                select (values.Select(x => x.Get).ToArray(), delimiter.Get);
            return input.ToArbitrary();
        }
        
        public static Arbitrary<(int[] values, string delimiter)> GenerateNegativeNumbers()
        {
            var input = from values in Arb.Generate<NegativeInt[]>().Where(x => x.Any())
                from delimiter in Arb.Generate<NonEmptyString>().Where(x => !x.Item.IsInteger())
                select (values.Select(x => x.Get).ToArray(), delimiter.Get);
            return input.ToArbitrary();
        }
    }
}