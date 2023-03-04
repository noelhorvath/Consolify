using Consolify.Core.Plugin;
using System.CommandLine.Builder;

namespace Consolify.Core.Builder
{
    public interface IApplicationBuilder<TConsole, TDragAndDropHandler, TApplicationConfiguration, TApplication, TPlugin, TPluginContext, TCommand, TParserBuilder, TApplicationBuilder> : IBuilder<TApplication>
        where TConsole : IConsole
        where TDragAndDropHandler : IDragAndDropHandler
        where TApplicationConfiguration : IApplicationConfiguration<TConsole, TDragAndDropHandler>
        where TApplication : IApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration>
        where TPlugin : IPlugin
        where TPluginContext : IPluginLoader<TPlugin>
        where TCommand : Command
        where TParserBuilder : CommandLineBuilder
        where TApplicationBuilder : IApplicationBuilder<TConsole, TDragAndDropHandler, TApplicationConfiguration, TApplication, TPlugin, TPluginContext, TCommand, TParserBuilder, TApplicationBuilder>
    {
        public TApplicationBuilder AddPlugins(Action<TPluginContext> pluginLoader);
        public TApplicationBuilder ConfigureRootCommand(Action<TCommand> rootCommandConfigurer);
        public TApplicationBuilder ConfigureParserBuilder(Action<TParserBuilder> parserBuilderConfigurer);
        public TApplicationBuilder UseConfiguration(Func<TApplicationConfiguration> getApplicationConfiguration);
    }
}
