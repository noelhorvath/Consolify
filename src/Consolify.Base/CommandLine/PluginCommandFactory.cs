using Consolify.Core.CommandLine;

namespace Consolify.Base.CommandLine
{
    public static class PluginCommandFactory
    {
        public static PluginCommand<TCommandConfiguration> Create<TCommandConfiguration>(TCommandConfiguration configuration) where TCommandConfiguration : IMinimalCommandConfiguration =>
            new PluginCommand<TCommandConfiguration>(configuration);
    }
}
