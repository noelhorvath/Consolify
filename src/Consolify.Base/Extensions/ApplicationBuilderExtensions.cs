using Consolify.Core;
using Consolify.Core.Builder;
using Consolify.Core.Plugin;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace Consolify.Base.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static TApplication BuildApplication<TApplicationConfiguration, TApplication, TApplicationBuilder>(
            this IApplicationBuilder<IConsole, IDragAndDropHandler, TApplicationConfiguration, TApplication, IPlugin, IPluginLoader<IPlugin>, Command, CommandLineBuilder, TApplicationBuilder> builder,
            Command rootCommand,
            CommandLineBuilder parserBuilder,
            Func<Parser, TApplicationConfiguration, TApplication> appConstructor,
            Func<TApplicationConfiguration> getConfiguration,
            Action<Command>? rootCommandConfigurer,
            Action<CommandLineBuilder>? parserBuilderConfigurer,
            Action<IPluginLoader<IPlugin>>? pluginLoader)
                where TApplicationConfiguration : IApplicationConfiguration<IConsole, IDragAndDropHandler>
                where TApplication : IApplication<IConsole, IDragAndDropHandler, TApplicationConfiguration>, IPluginLoader<IPlugin>
                where TApplicationBuilder : IApplicationBuilder<IConsole, IDragAndDropHandler, TApplicationConfiguration, TApplication, IPlugin, IPluginLoader<IPlugin>, Command, CommandLineBuilder, TApplicationBuilder>
        {
            TApplicationConfiguration applicationConfiguration = getConfiguration();

            if (rootCommandConfigurer != null)
            {
                rootCommandConfigurer(rootCommand);
            }

            if (parserBuilderConfigurer != null)
            {
                parserBuilderConfigurer(parserBuilder);
            }

            Parser parser = parserBuilder.Build();
            TApplication application = appConstructor(parser, applicationConfiguration);

            if (pluginLoader != null)
            {
                pluginLoader(application);
            }

            return application;
        }
    }
}
