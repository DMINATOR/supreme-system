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

    [Fact]
    public void TestVersion()
    {
        var engine = new Engine();
        var version = engine.GetVersion();
        Assert.Equal("1.0.0", version);
    }
}
