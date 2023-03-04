using Consolify.Base.Extensions;
using Consolify.Base.Resources;
using Consolify.Core;
using Consolify.Core.Plugin;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace Consolify.Base
{
    public abstract class BasicApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration, TPlugin> : IApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration>, IModular<TPlugin>
        where TConsole : IConsole
        where TDragAndDropHandler : IDragAndDropHandler
        where TApplicationConfiguration : IApplicationConfiguration<TConsole, TDragAndDropHandler>
        where TPlugin : IPlugin
    {
        private Dictionary<string, TPlugin>? _plugins;
        private Parser _parser;
        public TApplicationConfiguration Configuration { get; init; }
        protected Parser Parser => _parser;
        protected IDictionary<string, TPlugin> InternalPlugins { get => _plugins ??= new Dictionary<string, TPlugin>(StringComparer.OrdinalIgnoreCase); }

        public IReadOnlyDictionary<string, TPlugin> Plugins { get => (Dictionary<string, TPlugin>)InternalPlugins; }

        protected BasicApplication(Parser parser, TApplicationConfiguration configuration)
        {
            _parser = parser;
            Configuration = configuration;
        }

        protected BasicApplication(Command rootCommand, TApplicationConfiguration configuration) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), configuration) { }
        protected BasicApplication(string rootCommandDescription, TApplicationConfiguration configuration) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), configuration) { }
        protected BasicApplication(TApplicationConfiguration configuration) : this(new CommandLineBuilder().UseDefaults().Build(), configuration) { }

        public virtual bool Load(TPlugin plugin)
        {
            Command pluginCommand = plugin.Command;

            if (Parser.Configuration.RootCommand.HasSubcommandAlias(pluginCommand.Name) || InternalPlugins.TryGetValue(plugin.Name, out _))
            {
                return false;
            }

            return Parser.Configuration.RootCommand.TryAddCommand(pluginCommand) && InternalPlugins.TryAdd(plugin.Name, plugin);
        }

        public virtual bool Unload(string pluginName)
        {
            if (InternalPlugins.TryGetValue(pluginName, out TPlugin? result) && result is TPlugin plugin)
            {
                return Parser.Configuration.RootCommand.RemoveCommand(plugin.Command.Name) && InternalPlugins.Remove(pluginName);
            }

            return false;
        }

        public virtual int Run(ReadOnlySpan<char> commandLine)
        {
            try
            {
                string[] arguments = this.GetArguments(commandLine);

                if (Configuration.DragAndDropHandler != null && arguments.Length > 0 && Configuration.DragAndDropHandler.CanHandle(Parser, arguments))
                {
                    return Configuration.DragAndDropHandler.Handle(
                        Parser,
                        arguments.Length == 2 ?
                        arguments[1] :
                        arguments[0]);
                }

                return Parser.Invoke(arguments, Configuration.Console);
            }
            catch (Exception)
            {
                Console.WriteLine(Strings.UnexpectedErrorOccured);
                return (int)ExitCode.UnexpectedErrorOccured;
            }
        }

        public virtual Task<int> RunAsync(ReadOnlySpan<char> commandLine)
        {
            try
            {
                string[] arguments = this.GetArguments(commandLine);

                if (Configuration.DragAndDropHandler != null && arguments.Length > 0 && Configuration.DragAndDropHandler.CanHandle(Parser, arguments))
                {
                    return Configuration.DragAndDropHandler.HandleAsync(
                        Parser,
                        arguments.Length == 2 ?
                        arguments[1] :
                        arguments[0]);
                }

                return Parser.InvokeAsync(arguments, Configuration.Console);
            }
            catch (Exception)
            {
                Console.WriteLine(Strings.UnexpectedErrorOccured);
                return Task.FromResult((int)ExitCode.UnexpectedErrorOccured);
            }
        }
    }
}
