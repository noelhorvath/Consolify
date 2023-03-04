using Consolify.Core;
using Consolify.Core.Plugin;
using Consolify.Base.Dynamic;
using System.CommandLine;
using Consolify.Base;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace Consolify.Dynamic
{
    public sealed class DynamicTerminalApplication : BasicDynamicTerminalApplication<IConsole, IDragAndDropHandler, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler>, IPlugin>
    {
        public DynamicTerminalApplication(Parser parser, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : base(parser, configuration) { }
        public DynamicTerminalApplication(Command rootCommand, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), configuration) { }
        public DynamicTerminalApplication(string rootCommandDescription, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), configuration) { }
        public DynamicTerminalApplication(ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder().UseDefaults().Build(), configuration) { }
        public DynamicTerminalApplication(CommandLineBuilder parserBuilder, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(parserBuilder.Build(), configuration) { }
        public DynamicTerminalApplication(Parser parser) : this(parser, new TerminalApplicationConfiguration()) { }
        public DynamicTerminalApplication(Command rootCommand) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), new TerminalApplicationConfiguration()) { }
        public DynamicTerminalApplication(string rootCommandDescription) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), new TerminalApplicationConfiguration()) { }
        public DynamicTerminalApplication() : this(new CommandLineBuilder().UseDefaults().Build(), new TerminalApplicationConfiguration()) { }
    }
}
