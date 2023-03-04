namespace Consolify.Core.Plugin
{
    public interface IVersioned<TVersion>
        where TVersion : IVersion
    {
        TVersion Version { get; }
    }
}
