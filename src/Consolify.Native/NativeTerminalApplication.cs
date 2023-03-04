using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using Consolify.Base;
using Consolify.Core;
using Consolify.Core.Plugin;

namespace Consolify.Native
{
    public sealed class NativeTerminalApplication : BasicTerminalApplication<IConsole, IDragAndDropHandler, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler>, IPlugin>
    {
        public NativeTerminalApplication(Parser parser, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : base(parser, configuration) { }
        public NativeTerminalApplication(Command rootCommand, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), configuration) { }
        public NativeTerminalApplication(string rootCommandDescription, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), configuration) { }
        public NativeTerminalApplication(ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder().UseDefaults().Build(), configuration) { }
        public NativeTerminalApplication(Parser parser) : this(parser, new TerminalApplicationConfiguration()) { }
        public NativeTerminalApplication(Command rootCommand) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), new TerminalApplicationConfiguration()) { }
        public NativeTerminalApplication(string rootCommandDescription) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), new TerminalApplicationConfiguration()) { }
        public NativeTerminalApplication() : this(new CommandLineBuilder().UseDefaults().Build(), new TerminalApplicationConfiguration()) { }
    }
}
