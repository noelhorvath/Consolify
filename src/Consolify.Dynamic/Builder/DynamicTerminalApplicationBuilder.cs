using Consolify.Base.Extensions;
using Consolify.Base;
using Consolify.Core;
using System.CommandLine;
using Consolify.Base.Builder;

namespace Consolify.Dynamic.Builder
{
    public sealed class DynamicTerminalApplicationBuilder : BasicApplicationBuilder<DynamicTerminalApplication, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler>>
    {
        public DynamicTerminalApplicationBuilder(Command? rootCommand) : base(rootCommand) { }
        public DynamicTerminalApplicationBuilder(string rootCommandDescription) : this(new RootCommand(rootCommandDescription)) { }
        public DynamicTerminalApplicationBuilder() : this(rootCommand: null) { }

        public override DynamicTerminalApplication Build() =>
            this.BuildApplication(
                RootCommand,
                ParserBuilder,
                (parser, config) => new DynamicTerminalApplication(parser, config),
                GetConfiguration ?? (() => new TerminalApplicationConfiguration() as ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler>),
                RootCommandConfigurer,
                ParserBuilderConfigurer,
                PluginLoader);
    }
}
