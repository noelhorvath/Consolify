using Consolify.Core.Plugin;
using System.Diagnostics.CodeAnalysis;

namespace Consolify.Base.Plugin
{
    public abstract class BasicPlugin : IVersionedPlugin<PluginVersion>
    {
        protected string _name;
        public virtual string Name
        {
            get => _name;

            [MemberNotNull(nameof(_name))]
            private set
            {
                ReadOnlySpan<char> span = value.AsSpan();
                _name = span.IsEmpty || span.IsWhiteSpace() ? nameof(BasicPlugin) : value;
            }
        }

        public virtual PluginVersion Version { get; private set; }
        public abstract Command Command { get; }

        protected BasicPlugin(string name, int majorVersion, int minorVersion) : this(name, new PluginVersion(majorVersion, minorVersion)) { }

        protected BasicPlugin(string name, PluginVersion version)
        {
            Name = name;
            Version = version;
        }

        protected BasicPlugin(string name) : this(name, new PluginVersion()) { }

        public override string ToString() => $"{Name} {Version}";
    }
}
