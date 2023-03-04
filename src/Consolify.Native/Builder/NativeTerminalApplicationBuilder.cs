using Consolify.Base;
using Consolify.Base.Builder;
using Consolify.Base.Extensions;
using Consolify.Core;
using System.CommandLine;

namespace Consolify.Native.Builder
{
    public sealed class NativeTerminalApplicationBuilder : BasicApplicationBuilder<NativeTerminalApplication, ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler>>
    {
        public NativeTerminalApplicationBuilder(Command? rootCommand) : base(rootCommand) { }
        public NativeTerminalApplicationBuilder(string rootCommandDescription) : this(new RootCommand(rootCommandDescription)) { }
        public NativeTerminalApplicationBuilder() : this(rootCommand: null) { }

        public override NativeTerminalApplication Build() =>
            this.BuildApplication(
                RootCommand,
                ParserBuilder,
                (parser, config) => new NativeTerminalApplication(parser, config),
                GetConfiguration ?? (() => new TerminalApplicationConfiguration() as ITerminalApplicationConfiguration<IConsole, IDragAndDropHandler>),
                RootCommandConfigurer,
                ParserBuilderConfigurer,
                PluginLoader);
    }
}
