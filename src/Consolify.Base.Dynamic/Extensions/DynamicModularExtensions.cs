using Consolify.Base.Dynamic.Resources;
using Consolify.Core;
using Consolify.Core.Plugin;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Consolify.Base.Dynamic.Extensions
{
    internal static class DynamicModularExtensions
    {
        internal static bool LoadPluginFromAssemblyFile<TPlugin>(this IDynamicModular<TPlugin> app, string assemblyFilePath)
            where TPlugin : IPlugin
        {
            Assembly loadedAssembly;
            PluginLoadContext pluginContext = new(assemblyFilePath);
            loadedAssembly = pluginContext.LoadFromAssemblyPath(assemblyFilePath);
            var (pluginType, constructorInfo) = loadedAssembly.GetTypes().AsSpan().FirstOrDefault<Type, ConstructorInfo?>(GetPluginInfo<TPlugin>, defaultValue: (null, null));

            if (pluginType == null || constructorInfo == null)
            {
                throw new PluginNotFoundException(string.Format(CultureInfo.CurrentCulture, CompositeFormat.Parse(Strings.PluginNotFound), loadedAssembly.FullName), loadedAssembly);
            }

            TPlugin plugin = (TPlugin)constructorInfo.Invoke(obj: null, parameters: null)!;
            pluginContext.Unload();
            return app.Load(plugin);
        }

        private static (bool IsPluginType, ConstructorInfo? Constructor) GetPluginInfo<TPlugin>(Type type)
            where TPlugin : IPlugin
        {
            Type basePluginType = typeof(TPlugin);
            bool IsPluginType = basePluginType.IsInterface ? basePluginType.IsAssignableFrom(type) : type.IsSubclassOf(basePluginType);
            return IsPluginType ? (IsPluginType, basePluginType.GetConstructor(Type.EmptyTypes)) : (IsPluginType, null);
        }
    }
}
