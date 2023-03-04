using System.Reflection;
using System.Runtime.Loader;

namespace Consolify.Base.Dynamic
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver _resolver;

        public PluginLoadContext(string pluginPath) : base(isCollectible: true)
        {
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }

        protected override Assembly? Load(AssemblyName name)
        {
            string? assemblyPath = _resolver.ResolveAssemblyToPath(name);
            return assemblyPath != null ? LoadFromAssemblyPath(assemblyPath) : null;
        }

        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            string? assemblyPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            return assemblyPath != null ? LoadUnmanagedDllFromPath(assemblyPath) : nint.Zero;
        }
    }
}
