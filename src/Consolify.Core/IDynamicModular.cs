using Consolify.Core.Plugin;

namespace Consolify.Core
{
    public interface IDynamicModular<TPlugin> : IModular<TPlugin> where TPlugin : IPlugin
    {
        bool LoadFromFile(string assemblyFilePath);
    }
}
