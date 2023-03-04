using Consolify.Core.Plugin;

namespace Consolify.Core
{
    public interface IModular<TPlugin> : IPluginLoader<TPlugin>, IPluginUnloader
        where TPlugin : IPlugin
    {
        IReadOnlyDictionary<string, TPlugin> Plugins { get; }
    }
}
