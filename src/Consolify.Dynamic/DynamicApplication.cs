using Consolify.Base;
using Consolify.Core;
using Consolify.Core.Plugin;
using Consolify.Base.Dynamic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace Consolify.Dynamic
{
    public sealed class DynamicApplication : BasicDynamicApplication<IConsole, IDragAndDropHandler, IApplicationConfiguration<IConsole, IDragAndDropHandler>, IPlugin>
    {
        public DynamicApplication(Parser parser, IApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : base(parser, configuration) { }
        public DynamicApplication(Command rootCommand, IApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), configuration) { }
        public DynamicApplication(string rootCommandDescription, IApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), configuration) { }
        public DynamicApplication(IApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(new CommandLineBuilder().UseDefaults().Build(), configuration) { }
        public DynamicApplication(CommandLineBuilder parserBuilder, IApplicationConfiguration<IConsole, IDragAndDropHandler> configuration) : this(parserBuilder.Build(), configuration) { }
        public DynamicApplication(Parser parser) : this(parser, new ApplicationConfiguration()) { }
        public DynamicApplication(Command rootCommand) : this(new CommandLineBuilder(rootCommand).UseDefaults().Build(), new ApplicationConfiguration()) { }
        public DynamicApplication(string rootCommandDescription) : this(new CommandLineBuilder(new RootCommand(rootCommandDescription)).UseDefaults().Build(), new ApplicationConfiguration()) { }
        public DynamicApplication() : this(new CommandLineBuilder().UseDefaults().Build(), new ApplicationConfiguration()) { }
    }
}
