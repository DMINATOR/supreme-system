namespace SupremeEngine.Test;

using Xunit.Abstractions;
using SupremeEngine;

public class EngingeTest
{
    [Fact]
    public void Test1()
    {
        var engine = new Engine();
        Assert.NotNull(engine);
    }
}
