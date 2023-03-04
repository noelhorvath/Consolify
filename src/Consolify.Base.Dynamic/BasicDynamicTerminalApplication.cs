using Consolify.Base.Dynamic.Extensions;
using Consolify.Core;
using Consolify.Core.Plugin;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace Consolify.Base.Dynamic
{
    public abstract class BasicDynamicTerminalApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration, TPlugin> : BasicTerminalApplication<TConsole, TDragAndDropHandler, TApplicationConfiguration, TPlugin>, IDynamicModular<TPlugin>
        where TConsole : IConsole
        where TDragAndDropHandler : IDragAndDropHandler
        where TApplicationConfiguration : ITerminalApplicationConfiguration<TConsole, TDragAndDropHandler>
        where TPlugin : IPlugin
    {
        protected BasicDynamicTerminalApplication(Parser parser, TApplicationConfiguration configuration) : base(parser, configuration) { }
        protected BasicDynamicTerminalApplication(Command rootCommand, TApplicationConfiguration configuration) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), configuration) { }
        protected BasicDynamicTerminalApplication(string rootCommandDescription, TApplicationConfiguration configuration) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), configuration) { }
        protected BasicDynamicTerminalApplication(TApplicationConfiguration configuration) : this(new CommandLineBuilder().UseDefaults().Build(), configuration) { }

        public virtual bool LoadFromFile(string assemblyFilePath) => this.LoadPluginFromAssemblyFile(assemblyFilePath);
    }
}
