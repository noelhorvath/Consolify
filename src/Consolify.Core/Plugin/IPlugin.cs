namespace Consolify.Core.Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        Command Command { get; }
    }
}
