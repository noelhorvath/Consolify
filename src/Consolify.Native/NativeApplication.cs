using Consolify.Base;
using Consolify.Core;
using Consolify.Core.Plugin;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace Consolify.Native
{
    public sealed class NativeApplication : BasicApplication<IConsole, IDragAndDropHandler, IApplicationConfiguration<IConsole, IDragAndDropHandler>, IPlugin>
    {
        public NativeApplication(Parser parser, IApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : base(parser, configuration) { }
        public NativeApplication(Command rootCommand, IApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), configuration) { }
        public NativeApplication(string rootCommandDescription, IApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), configuration) { }
        public NativeApplication(IApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder().UseDefaults().Build(), configuration) { }
        public NativeApplication(Parser parser) : this(parser, new ApplicationConfiguration()) { }
        public NativeApplication(Command rootCommand) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), new ApplicationConfiguration()) { }
        public NativeApplication(string rootCommandDescription) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), new ApplicationConfiguration()) { }
        public NativeApplication() : this(new CommandLineBuilder().UseDefaults().Build(), new ApplicationConfiguration()) { }
    }
}
