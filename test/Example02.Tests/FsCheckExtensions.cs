using FsCheck;

namespace Example02.Tests;

public static class FsCheckExtensions
{
    public static void QuickCheckThrowOnFailure(this Property property, int maxNbrOfTests)
    {
        var configuration = Configuration.QuickThrowOnFailure;
        configuration.MaxNbOfTest = maxNbrOfTests;
        property.Check(configuration);
    }
}