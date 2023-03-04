namespace Consolify.Core.Plugin
{
    public interface IVersionedPlugin<TVersion> : IPlugin, IVersioned<TVersion>
        where TVersion : IVersion
    {

    }
}
