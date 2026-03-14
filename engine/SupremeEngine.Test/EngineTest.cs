namespace SupremeEngine.Test;

using Xunit.Abstractions;
using SupremeEngine;

public class EngineTest
{
    [Fact]
    public void Test1()
    {
        var engine = new Engine();
        Assert.NotNull(engine);
    }
}
