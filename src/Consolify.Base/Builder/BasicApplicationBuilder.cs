using Consolify.Core;
using Consolify.Core.Builder;
using Consolify.Core.Plugin;
using System.CommandLine.Builder;

namespace Consolify.Base.Builder
{
    public abstract class BasicApplicationBuilder<TApplication, TApplicationConfiguration> : IApplicationBuilder<IConsole, IDragAndDropHandler, TApplicationConfiguration, TApplication, IPlugin, IPluginLoader<IPlugin>, Command, CommandLineBuilder, BasicApplicationBuilder<TApplication, TApplicationConfiguration>>
        where TApplicationConfiguration : IApplicationConfiguration<IConsole, IDragAndDropHandler>
        where TApplication : IApplication<IConsole, IDragAndDropHandler, TApplicationConfiguration>
    {
        private CommandLineBuilder? _parserBuilder;
        private Command? _rootCommand;

        protected Func<TApplicationConfiguration>? GetConfiguration { get; set; }
        protected Action<Command>? RootCommandConfigurer { get; set; }
        protected Action<CommandLineBuilder>? ParserBuilderConfigurer { get; set; }
        protected CommandLineBuilder ParserBuilder { get => _parserBuilder ??= new CommandLineBuilder(RootCommand); }
        protected Action<IPluginLoader<IPlugin>>? PluginLoader { get; set; }
        protected Command RootCommand { get => _rootCommand ??= new RootCommand(); }

        protected BasicApplicationBuilder(Command? rootCommand)
        {
            _rootCommand = rootCommand;
        }

        protected BasicApplicationBuilder(string rootCommandDescription) : this(new RootCommand(rootCommandDescription)) { }
        protected BasicApplicationBuilder() : this(rootCommand: null) { }

        public abstract TApplication Build();

        public BasicApplicationBuilder<TApplication, TApplicationConfiguration> AddPlugins(Action<IPluginLoader<IPlugin>> pluginLoader)
        {
            PluginLoader = pluginLoader;
            return this;
        }

        public BasicApplicationBuilder<TApplication, TApplicationConfiguration> UseConfiguration(Func<TApplicationConfiguration> applicationConfigurer)
        {
            GetConfiguration = applicationConfigurer;
            return this;
        }

        public BasicApplicationBuilder<TApplication, TApplicationConfiguration> ConfigureParserBuilder(Action<CommandLineBuilder> parserBuilderConfigurer)
        {
            ParserBuilderConfigurer = parserBuilderConfigurer;
            return this;
        }

        public BasicApplicationBuilder<TApplication, TApplicationConfiguration> ConfigureRootCommand(Action<Command> rootCommandConfigurer)
        {
            RootCommandConfigurer = rootCommandConfigurer;
            return this;
        }
    }
}
