using Consolify.Core.CommandLine;
using System.CommandLine.Invocation;

namespace Consolify.Base.CommandLine
{
    public abstract class BasicMinimalCommandConfiguration<TCommandConfiguration> : IMinimalCommandConfiguration
        where TCommandConfiguration : IMinimalCommandConfiguration, new()
    {
        public static TCommandConfiguration? _instance;
        public static TCommandConfiguration Instance { get => _instance ??= new TCommandConfiguration(); }
        public static TCommandConfiguration GetInstance() => Instance;
        public virtual IReadOnlyCollection<string> Aliases { get; } = Array.Empty<string>();
        public abstract ICommandHandler? Handler { get; }
        public abstract string Name { get; }
        public virtual string Description { get; } = string.Empty;
        public virtual bool IsHidden { get; }
        public virtual bool TreatUnmatchedTokensAsErrors { get; }

        protected BasicMinimalCommandConfiguration() { }
    }
}
