using Consolify.Core.Plugin;
using System.Runtime.InteropServices;

namespace Consolify.Base.Plugin
{
    [StructLayout(LayoutKind.Auto)]
    public readonly struct PluginVersion : IVersion
    {
        private readonly int _major;
        private readonly int _minor;
        public readonly int Major
        {
            get => _major;
            init => _major = value < 0 ? 1 : value;
        }
        public readonly int Minor
        {
            get => _minor;
            init => _minor = value < 0 ? 0 : value;
        }

        public PluginVersion(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }

        public PluginVersion(int major) : this(major, 0) { }
        public PluginVersion() : this(1, 0) { }

        public override string ToString() => $"{Major}.{Minor}";
    }
}
