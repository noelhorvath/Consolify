using System.Reflection;

namespace Consolify.Base.Dynamic
{
    public class PluginNotFoundException : Exception
    {
        public Assembly Assembly { get; init; }

        public PluginNotFoundException(string message, Assembly assembly, Exception? innerException) : base(message, innerException)
        {
            Assembly = assembly;
        }

        public PluginNotFoundException(string message, Assembly assembly) : this(message, assembly, innerException: null) { }
    }
}
