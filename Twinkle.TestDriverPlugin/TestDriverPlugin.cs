namespace Twinkle.TestDriverPlugin;

using Twinkle.API;

public class TestDriverPlugin : DriverPlugin<TestDriverPluginView>
{
    public override string Name => "Test Driver";
    public string SomeData => "TestData";
}