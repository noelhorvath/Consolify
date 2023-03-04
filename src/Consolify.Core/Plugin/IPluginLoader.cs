namespace Consolify.Core.Plugin
{
    public interface IPluginLoader<TPlugin> where TPlugin : IPlugin
    {
        bool Load(TPlugin plugin);
    }
}
